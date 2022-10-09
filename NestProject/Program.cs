using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NestProject.DAL;
using NestProject.Models;
using NestProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<LayoutService>();
builder.Services.AddIdentity<AppUser, IdentityRole>(con =>
{
    con.Password.RequiredLength = 8;
    con.Password.RequireNonAlphanumeric = false;
    con.User.RequireUniqueEmail = true;
    con.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<NestContext>();
builder.Services.AddDbContext<NestContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration["ConnectionStrings:default"]);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=DashBoard}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

NestProject.Utilities.Constants.Constants.RootPath = Path.Combine(app.Environment.WebRootPath, "assets");

app.Run();
