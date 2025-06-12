using ApiClient.RefitInterfaces;
using Application.Entities.Auth;
using Application.Interfaces.Auth;
using Application.Interfaces.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Refit;
using System.Text;
using Microsoft.OpenApi.Models;
using Application.Interfaces.OAuth2;
using Application.Services.OAuth2;


namespace ProjetosApi.Ioc
{
    public static class IndependencyInjection
    {
        public static IServiceCollection ServiceCollection { get; private set; } = new ServiceCollection();
        public static IConfiguration? Configuration { get; private set; }
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ServiceCollection = serviceCollection;
            Configuration = configuration;
            AddSwaggerConfiguration();
            AddScopedServices();
            AddRefitSettings();
            AddOAuth2();
            serviceCollection.AddMemoryCache();
            return serviceCollection;
        }
        public static void AddSwaggerConfiguration()
        {
            ServiceCollection.AddAuthentication(options =>
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
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("A2D8u9jWwWnKlQ27@pN7XzL5bQyP9v3l2cT5y0jZf8yM1mZ2tG7uO0c3QhVwD9bA")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            ServiceCollection.AddControllers();
            ServiceCollection.AddEndpointsApiExplorer();

            ServiceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto integração", Version = "v1" });

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
                new string[] { }
            }
                });
            });
        }
        public static void AddScopedServices()
        {
            ServiceCollection.AddScoped<ICepService, CepService>();
            ServiceCollection.AddScoped<IFipeService, FipeService>();
            ServiceCollection.AddScoped<IIbgeService, IbgeService>();
            ServiceCollection.AddScoped<IAuthService, AuthService>();
        }
        public static void AddRefitSettings()
        {
            ServiceCollection.AddRefitClient<ICepRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://brasilapi.com.br");
            });/* .AddHttpMessageHandler<HttpRequestHandler>(); */
            ServiceCollection.AddRefitClient<IFipeRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://brasilapi.com.br");
            });
            ServiceCollection.AddRefitClient<IIbgeRefit>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://brasilapi.com.br");
            });
        }
        /* public static void AddToken(this IServiceCollection services)
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
        } */
        public static void AddOAuth2()
        {
            ServiceCollection.AddHttpClient();
            ServiceCollection.AddScoped<IGoogleAuthService, GoogleAuthService>();

        }
    }
}
