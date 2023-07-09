using Database;
using Microsoft.EntityFrameworkCore;
using System;
using Config;
using Microsoft.AspNetCore.Identity;
using Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Enable Swagger/OpenAPI documentation generation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Configure the database context with the connection string.
builder.Services.AddDbContext<AppDbContext>(dbOptions =>
    dbOptions.UseSqlServer(builder.Configuration.GetConnectionString("SignUpDb"))
);

// Configure Identity with options for password requirements and storage.
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    // Password requirements
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add and configure repositories.
builder.Services.AddAndConfigureRepositories();

// Add and configure services.
builder.Services.AddAndConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI for API documentation.
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Enable CORS for specific origins, methods, and headers.
app.UseCors(options => options.WithOrigins("https://localhost:7141/").AllowAnyMethod().AllowAnyHeader());

// Map controllers for handling API endpoints.
app.MapControllers();

app.Run();
