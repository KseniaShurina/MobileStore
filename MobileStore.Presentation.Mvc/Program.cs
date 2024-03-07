using Microsoft.AspNetCore.Authentication.Cookies;
using MobileStore.Core.Configurations;
using MobileStore.Presentation.Mvc.Configurations;
using MobileStore.Presentation.Mvc.Helpers.Mapper;
using MobileStore.Presentation.Mvc.Middleware;

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

// Configuring routing middleware
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
// Method is used to add middleware components to the ASP.NET Core request processing pipeline.
// IdentityMiddleware is custom middleware component.
app.UseMiddleware<IdentityMiddleware>();

// is used to configure the default controller route directly in the Program.cs file.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();