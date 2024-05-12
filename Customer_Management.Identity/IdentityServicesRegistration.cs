using Customer_Management.Application.Contracts.Identity;
using Customer_Management.Application.Models.Identity;
using Customer_Management.Identity.Models;
using Customer_Management.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Customer_Management.Identity
{
    public static class IdentityServicesRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var j = configuration.GetSection("JwtSettings");

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.Configure<JwtSettings>(j);
            services.AddDbContext<CustomerManagementIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("CustomerManagementIdentityConnectionString"),
                  b => b.MigrationsAssembly(typeof(CustomerManagementIdentityDbContext).Assembly.FullName)
                  );
            });


            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CustomerManagementIdentityDbContext>().AddDefaultTokenProviders();

             services.AddTransient<IAuthService, AuthService>();
            //services.AddTransient<IUserService, UserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                         IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                    };
                });
            return services;
        }

    }
}
