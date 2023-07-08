using Database;
using Microsoft.EntityFrameworkCore;
using System;
using Config;
using Microsoft.AspNetCore.Identity;
using Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// This code will create the Database in MSSQL Server.
builder.Services.AddDbContext<AppDbContext>(dbOptions =>
dbOptions.UseSqlServer(builder.Configuration.GetConnectionString("SignUpDb"))
);

//The code is passing in a lambda expression to the AddIdentity method
//that configures various options for the identity system.

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    //The options include:
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
})
//Adding Entity Framework Core stores for the identity system,
//using the specified DbContext (SignUpDb) for the data access.
.AddEntityFrameworkStores<AppDbContext>()
//Adding the default token providers for the identity system.
//Token providers are responsible for generating and validating tokens,
//such as password reset and email confirmation tokens.
.AddDefaultTokenProviders();

builder.Services.AddAndConfigureRepositories();
builder.Services.AddAndConfigureServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
