using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NttDataApi.Database;
using NttDataApi.Repository;
using System.Collections.Generic;
using System.Text;
using ViewModel;

namespace NttDataApi.Middlware
{
    public static class IoC
    {
        public static IServiceCollection AddDatabaseCooperativa(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CooperativaDatabase>
             (options =>
             options.UseSqlServer(configuration.GetConnectionString("Default")

             ));

            return services;
        }


        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICuentaRepository, CuentaRepository>();
            services.AddScoped<IMovimientoRepository, MovimientoRepository>();
            return services;
        }

      


        public static IServiceCollection AddSwaggerAuthorize(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Web Api", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the bearer scheme \r\n\r\n
                                    Enter 'Bearer' [space] and then your token in the text input below
                                    \r\n\r\nExample 'Bearer 1234abc'
                                    ",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
            });

            return services;
        }
    
    
        public static IServiceCollection AddAuthorizacionToken(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
            return services;
        }
    
    }
}
