using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ornaments.Core.Services.Implements;
using Ornaments.Core.Services.Interfaces;
using Ornaments.DataAccess.Context;
using Ornaments.DataAccess.Entities.User;
using Ornaments.DataAccess.Repository;
using System.Security.Permissions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
#region Database
builder.Services.AddDbContext<DataContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DataConnection"));
});
#endregion
#region Account
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
{
    op.Password.RequireUppercase = false;
    op.Password.RequireNonAlphanumeric = false;
    op.Password.RequireLowercase = false;
    op.Password.RequireDigit = false;
    op.Password.RequiredLength = 4;

    op.User.RequireUniqueEmail = true;

    op.Lockout.MaxFailedAccessAttempts = 6;
    op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);

}
            )
.AddEntityFrameworkStores<DataContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/AccessDenied";
    options.Cookie.Name = "Ornaments";
    options.LoginPath = "/Login";
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
});

builder.Services.Configure<SecurityStampValidatorOptions>(option =>
{
    option.ValidationInterval = TimeSpan.FromMinutes(5);
});


#endregion
#region IOC
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IMessageSender, MessageSender>();
builder.Services.AddScoped<IOrnamentService, OrnamentService>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}");
	endpoints.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
 );
});

app.Run();
