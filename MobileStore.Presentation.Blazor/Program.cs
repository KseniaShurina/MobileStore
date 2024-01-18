using Microsoft.AspNetCore.Authentication.Cookies;
using MobileStore.Core.Configurations;
using MobileStore.Presentation.Blazor.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCoreDependencies(builder.Configuration);

builder.Services.AddMvc();
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Mud blazor
builder.Services.AddMudServices();
builder.Services.AddTransient<NotificationService>();


// To set up Authentication by Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/Account/Login");
    });
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

//This method instructs the server to use certain authentication strategies when processing requests
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();
app.MapDefaultControllerRoute();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
