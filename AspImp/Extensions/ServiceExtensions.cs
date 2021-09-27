using Data;
using Contracts;
using LoggerService;
using Data.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Text;
using Repository.Interfaces;
using System.Net;
using Repository.Implements;
using Microsoft.AspNetCore.Mvc;

namespace AspImp.Extensions
{
  public static class ServiceExtensions
  {
    public static void ConfigureIISIntegration(this IServiceCollection services) =>
     services.Configure<IISOptions>(options => { });

    public static void ConfigureCors(this IServiceCollection services) =>
      services.AddCors(opt =>
        opt.AddPolicy("CorsPolicy", builder =>
          builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
    ));

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
      services.AddDbContext<DBContext>(opts =>
        opts.UseMySQL("server=103.159.50.133;database=nhatro;user=schedulekma;password=Schedulekma123@;"));

    public static void ConfigureLoggerService(this IServiceCollection services) =>
      services.AddScoped<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) =>
      services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureIdentity(this IServiceCollection services)
    {
      var builder = services.AddIdentityCore<User>(opt =>
      {
        opt.Password.RequireDigit = true;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequiredLength = 10;
        opt.User.RequireUniqueEmail = true;
      });

      builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
      builder.AddEntityFrameworkStores<DBContext>()
        .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
      var jwtSettings = configuration.GetSection("JwtSettings");
      var secretKey = configuration.GetValue("SecretKey", string.Empty);

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
          ValidAudience = jwtSettings.GetSection("validAudience").Value,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        });
    }

    public static void ConfigureAuth(this IServiceCollection services) =>
      services.AddScoped<IAuthenticationManager, Repository.Implements.AuthenticationManager>();

    public static void ConfigureModelError(this IServiceCollection services) =>
      services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
  }
}
