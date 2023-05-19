using Microsoft.AspNetCore.Authentication.Cookies;
using MobileStore.Core.Configurations;
using MobileStore.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterCoreDependencies(builder.Configuration);

//services.AddRazorPages(); - AddMvc() inclued Razor yet
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

//connection configuration setting
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

// Build
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
//TODO Непонятно чо как работает. настроить обработку ошибок
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