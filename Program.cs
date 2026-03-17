using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Helpers;
using SchoolApi.Models.Data;
using SchoolApi.Services.Implementations;
using SchoolApi.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers ──────────────────────────────────────────────────────────────
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// ── Swagger / OpenAPI ─────────────────────────────────────────────────────────
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ── Database ──────────────────────────────────────────────────────────────────
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// ── JWT Authentication ────────────────────────────────────────────────────────
var jwtKey = builder.Configuration["Jwt:Key"]!;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// ── Helpers ───────────────────────────────────────────────────────────────────
builder.Services.AddScoped<JwtHelper>();

// ── Services ──────────────────────────────────────────────────────────────────
// Core
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IClassService, ClassService>();

// People
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStaffService, StaffService>();

// Academic Structure
builder.Services.AddScoped<ISectionService, SectionService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IStudentEnrollmentService, StudentEnrollmentService>();
builder.Services.AddScoped<IStaffAssignmentService, StaffAssignmentService>();

// Attendance
builder.Services.AddScoped<IStudentAttendanceService, StudentAttendanceService>();
builder.Services.AddScoped<IStaffAttendanceService, StaffAttendanceService>();

// Auth
builder.Services.AddScoped<IAuthService, AuthService>();

// Finance & Exams
builder.Services.AddScoped<IFeeService, FeeService>();
builder.Services.AddScoped<IExamService, ExamService>();

// Notifications & Reports
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IReportService, ReportService>();

// ── Build App ─────────────────────────────────────────────────────────────────
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolApi v1");
        c.DocumentTitle = "School Management System API";
    });
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();