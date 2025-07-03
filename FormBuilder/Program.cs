using FormBuilder.Data;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using FormBuilder.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// 🌍 Localization setup
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton<CloudinaryService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFormService, FormService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<ITemplateStatisticsService, TemplateStatisticsService>();

var app = builder.Build();

// 🌍 Configure cultures
var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("fr"),
    new CultureInfo("es"),
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,

};
localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());

// Support ?culture=en query strings
localizationOptions.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
app.UseRequestLocalization(localizationOptions);

// Seed roles and admin user
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    string[] roles = { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    var adminEmail = "admin@example.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        var admin = new User
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var createResult = await userManager.CreateAsync(admin, "Admin@123");
        if (createResult.Succeeded)
            await userManager.AddToRoleAsync(admin, "Admin");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var path = context.Request.Path.ToString().ToLower();

    if (context.User.Identity?.IsAuthenticated == true &&
        (path == "/auth/signin" || path == "/auth/signup"))
    {
        context.Response.Redirect("/");
        return;
    }

    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
