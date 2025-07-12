using FormBuilder.Data;
using FormBuilder.Interface;
using FormBuilder.Interfaces;
using FormBuilder.Models;
using FormBuilder.Service;
using FormBuilder.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using static Dropbox.Api.TeamLog.EventCategory;

var builder = WebApplication.CreateBuilder(args);

// 🌍 Localization setup

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
builder.Services.AddScoped<ISalesForceService, SalesForceService>();
builder.Services.AddScoped<ISupportTicketService, SupportTicketService>();

var dropboxAccessToken = builder.Configuration["Dropbox:AccessToken"];

builder.Services.AddScoped<IDropboxService>(sp => new DropboxService(dropboxAccessToken));


builder.Services.AddScoped<ISupportTicketService, SupportTicketService>();



builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();
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

if (AppDomain.CurrentDomain.FriendlyName != "ef")
{
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

        var admins = new[]
        {
            new { Email = "admin@example.com", Password = "Admin@123" },
            new { Email = "bughasteam123@gmail.com", Password = "BughaAdmin@123" }
        };

        foreach (var entry in admins)
        {
            var existingUser = await userManager.FindByEmailAsync(entry.Email);
            if (existingUser == null)
            {
                var newAdmin = new User
                {
                    UserName = entry.Email,
                    Email = entry.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(newAdmin, entry.Password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
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
app.UseSession();
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
