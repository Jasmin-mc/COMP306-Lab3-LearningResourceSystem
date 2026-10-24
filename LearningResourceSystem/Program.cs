using LearningResourceSystem.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//MVC Controllers and Views
builder.Services.AddControllersWithViews();

//AWS SQL Server connection
var connectionString =
    builder.Configuration.GetConnectionString("AuthConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'AuthConnection' was not found.");

var sqlBuilder = new SqlConnectionStringBuilder(connectionString)
{
    Encrypt = true,
    TrustServerCertificate = true
};



builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ResourcesDbContext>(options =>
    options.UseSqlServer(connectionString));

// Account management and cookie authentication.
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;

        options.Lockout.DefaultLockoutTimeSpan =
            TimeSpan.FromMinutes(5);

        options.Lockout.MaxFailedAccessAttempts = 5;

        
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";

    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
