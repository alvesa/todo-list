using todo_list.Domain.Service;
using todo_list.Domain.Repository;
using todo_list.Infra.Repository;
using todo_list.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using todo_list.Extension;

var builder = WebApplication.CreateBuilder(args);
var configurations = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        opt.RequireHttpsMetadata = false;
        opt.SaveToken = true;
        var secret = configurations["JwtSettings:secret"];
        var key = Encoding.UTF8.GetBytes(secret);
        opt.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

// Context
builder.Services.AddDbContext<ITodoContext, TodoContext>(x => x.UseInMemoryDatabase("TodoDb"));

// Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthenticationMiddleware();
app.UseExeptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
