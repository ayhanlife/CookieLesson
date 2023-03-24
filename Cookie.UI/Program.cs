using Cookie.UI.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CookieContext>(opt =>
{
    //delegate geciyorum
    opt.UseSqlServer("Data Source=AYHAN\\AYHAN;Password=sifre123@;User ID=sa;Initial Catalog=CookieDbTest;TrustServerCertificate=True;");
    opt.LogTo(Console.WriteLine, LogLevel.Information);
});

////////////////////COOKIE/////////////////////

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
{
    opt.Cookie.Name = "CustomCookie";
    opt.Cookie.HttpOnly = true; // JS ÝLE COOKIE CEKME
    opt.Cookie.SameSite = SameSiteMode.Strict;// COKIE PAYLAÞIMA KAPT
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.ExpireTimeSpan = TimeSpan.FromDays(10);
    opt.LoginPath = new PathString("/Home/SingIn");    
    opt.LogoutPath = new PathString("/Home/Logout");    
    opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
}); ;




////////////////////COOKIE/////////////////////













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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();

app.Run();
