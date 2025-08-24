using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using StudentCourseManagement;
using StudentCourseManagement.AppDbContextModels;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Services.Batch;
using StudentCourseManagement.Services.Course;
using System.Globalization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources"; // Folder where resource files will be stored
});

builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages()
    .AddViewLocalization();

var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/login/loginpage";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });



// Add FluentValidation services.
builder.Services.AddValidatorsFromAssemblyContaining<CourseRequestValidator>();
builder.Services.AddFluentValidationAutoValidation(); // Add this line to enable automatic validation.

// Add your custom services.
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<CourseRepo>();
builder.Services.AddScoped<BatchRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var supportedCultures = new[] { "en-US","my-MM" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());

// Use the localization middleware
app.UseRequestLocalization(localizationOptions);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();