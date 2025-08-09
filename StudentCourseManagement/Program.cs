using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentCourseManagement.AppDbContextModels;
using StudentCourseManagement.Models.Courses;
using StudentCourseManagement.Services.Batch;
using StudentCourseManagement.Services.Course;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

builder.Services.AddDbContext<AppDbContext>(option =>
option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
);

builder.Services.AddAuthentication("MyCookieAuth")
    .AddCookie("MyCookieAuth", options =>
    {
        options.LoginPath = "/login/loginpage"; 
        options.AccessDeniedPath = "/Account/AccessDenied"; // optional
    });

builder.Services.AddMvc();
builder.Services.AddValidatorsFromAssemblyContaining<CourseRequestValidator>();
builder.Services.AddControllersWithViews()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = false;
    });

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<CourseRepo>();
builder.Services.AddScoped<BatchRepo>();
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
