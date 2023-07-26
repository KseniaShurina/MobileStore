using Microsoft.AspNetCore.Authentication.Cookies;
using MobileStore.Core.Configurations;
using MobileStore.Presentation.Configurations;
using MobileStore.Presentation.Helpers.Mapper;
using MobileStore.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreDependencies(builder.Configuration);
builder.Services.AddAutoMapperProfiles(o =>
{
    o.AddProfile<PresentationMapperProfile>();
});

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

//connection configuration setting
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
    });

// Build
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");

//redirects all HTTP requests to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<IdentityMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();