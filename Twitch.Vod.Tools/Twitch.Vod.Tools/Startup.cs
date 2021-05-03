using System;
using System.Collections.Generic;
using System.Net.Http;
using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Twitch.Vod.Services.Configuration;
using Twitch.Vod.Services.Implementations;
using Twitch.Vod.Services.Interfaces;

namespace Twitch.Vod.Tools
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .SetBasePath("/opt/appsettings/twitch-vod-tools")
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Environment = env;
        }
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            var section = Configuration.GetSection("TwitchSettings").Get<TwitchConfigurationSection>();
            var vodService = new TwitchVodService(section);
            var userService = new TwitchUserService(section);
            services.AddSingleton<ITwitchUserService>(userService);
            services.AddSingleton<ITwitchVodService>(vodService);
            services.Configure<TwitchConfigurationSection>(Configuration.GetSection("TwitchSettings"));

            services.AddCors(options =>
            {
                options.AddPolicy("Twitch", config =>
                {
                    config.WithOrigins("https://id.twitch.tv/*", "https://twitch.tv/*").AllowAnyMethod();
                });
                if (Environment.IsDevelopment())
                {
                   options.AddPolicy("Development", config =>
                   {
                       config.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                   });
                }
            });

            services.AddControllers();
            services.AddAuthentication(config =>
            {
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.Configuration.Issuer = "https://twitch.tv";
                //config.Configuration.
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerValidator = IssuerValidator,
                };
            });
            //services.AddAuthentication(config =>
            //    {
            //        config.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        config.DefaultChallengeScheme = TwitchAuthenticationDefaults.AuthenticationScheme;
            //        config.DefaultAuthenticateScheme = TwitchAuthenticationDefaults.AuthenticationScheme;
            //    })
            //    .AddCookie(options =>
            //    {
            //        options.LoginPath = "/api/authentication/token";
            //        options.LogoutPath = "/api/authentication/logout";
            //        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //        options.Cookie.SameSite = SameSiteMode.None;
            //    })
            //    .AddTwitch(options =>
            //    {
            //        //options.CallbackPath = "/authenticated";
            //        options.ClientId = section.ClientId;
            //        options.ClientSecret = section.ClientSecret;
            //        options.Scope.Add("user:read:email");
            //        options.Scope.Add("clips:edit");
            //        options.Scope.Add("channel:manage:videos");
            //    });
            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = "wwwroot/twitch-vod-tools/dist";
            });
        }

        private string IssuerValidator(string issuer, SecurityToken securitytoken, TokenValidationParameters validationparameters)
        {
            using var httpClient = new HttpClient {BaseAddress = new Uri("https://id.twitch.tv/oauth2/validate")};

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("Twitch");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot/twitch-vod-tools";
                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080/");
                }
            });
        }
    }
}
