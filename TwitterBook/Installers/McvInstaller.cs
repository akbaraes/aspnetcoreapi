using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterBook.Authorization;
using TwitterBook.Extensions;
using TwitterBook.Services.PostService;

namespace TwitterBook.Installers
{
    public class McvInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllersWithViews(x=> {
                x.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddRazorPages();

            JwtSettings jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true

                };
            });


            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("UpdatePost", builder =>
                {
                    builder.RequireClaim("UpdatePost", "true");
                });

                opt.AddPolicy("WorkForCompanyRequirement", builder =>
                {
                    builder.AddRequirements(new WorkForCompanyRequirement("example.com"));
                });
            });

            services.AddSingleton<IAuthorizationHandler, WorkForCompanyHandler>();


          
            services.AddScoped<IPostService, PostService>();
        }
    }
}
