using Microsoft.EntityFrameworkCore;
using SchoolApi.Models.Data;
using SchoolApi.Services.Implementations;
using SchoolApi.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

// Services
builder.Services.AddScoped<IAcademicYearService, AcademicYearService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IClassService, ClassService>();

var app = builder.Build();

// Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();