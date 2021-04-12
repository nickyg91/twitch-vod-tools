using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
        }

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
            services.AddControllers();
            services.AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = TwitchAuthenticationDefaults.AuthenticationScheme;
                })
                .AddTwitch(options =>
                {
                    options.ClientId = section.ClientId;
                    options.ClientSecret = section.ClientSecret;
                    options.ReturnUrlParameter = section.RedirectUrl;
                    options.Scope.Add("user:read:email");
                    options.Scope.Add("clips:edit");
                    options.Scope.Add("channel:manage:videos");
                    options.SaveTokens = true;
                });
            services.AddSpaStaticFiles(options =>
            {
                options.RootPath = "wwwroot/twitch-vod-tools/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
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
