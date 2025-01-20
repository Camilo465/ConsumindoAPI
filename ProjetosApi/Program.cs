using ApiClient.RefitInterfaces;
using Application.Interfaces;
using Application.Interfaces.Auth;
using Application.Interfaces.Entities;
using Application.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Refit;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API com JWT", Version = "v1" });

    // Configura��o de seguran�a para o Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato: Bearer {seu token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<ICepService, CepService>();
builder.Services.AddScoped<IFipeService, FipeService>();
builder.Services.AddScoped<IIbgeService, IbgeService>();
builder.Services.AddScoped<IAuthService, AuthService>();



builder.Services.AddRefitClient<ICepRefit>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://brasilapi.com.br");
});
builder.Services.AddRefitClient<IFipeRefit>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://brasilapi.com.br");
});
builder.Services.AddRefitClient<IIbgeRefit>().ConfigureHttpClient(c =>
{
    c.BaseAddress = new Uri("https://brasilapi.com.br");
});

builder.Services.AddToken();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public static class ServiceExtensions
{
    public static void AddToken(this IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("A2D8u9jWwWnKlQ27@pN7XzL5bQyP9v3l2cT5y0jZf8yM1mZ2tG7uO0c3QhVwD9bA")),
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}
