using SMP.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SMP.Domain.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using SMP.Application.IoC;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));




});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new DependencyResolver());
});
builder.Services.AddAuthentication().AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = "390900063560-mncj321ih5cjvu4mu2qd8vahe398jojs.apps.googleusercontent.com"/*builder.Configuration["Authentication:Google:ClientId"]*/;
    options.ClientSecret = "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"/*builder.Configuration["Authentication:Google:ClientSecret"]*/;


});
//.AddCookie(options =>
//{
//    options.LoginPath = "/account/google-login"; // Must be lowercase
//})

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
    options.Password.RequireLowercase = false;



}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromDays(7);
});



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllerRoute(
    name: "Default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
