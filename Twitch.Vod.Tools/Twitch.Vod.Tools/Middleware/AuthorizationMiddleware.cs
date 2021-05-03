using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Twitch.Vod.Tools.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString().Split(" ")[1];
                if (string.IsNullOrEmpty(token))
                {
                    context.Response.StatusCode = 403;
                }
                using var httpClient = new HttpClient
                {
                    DefaultRequestHeaders =
                    {
                        Authorization = new AuthenticationHeaderValue("Bearer", token)
                    }
                };
                var response = await httpClient.GetAsync(new Uri("https://id.twitch.tv/oauth2/validate"));
                if (!response.IsSuccessStatusCode)
                {
                    context.Response.StatusCode = 403;
                }
                else
                {
                    context.User.AddIdentity(new ClaimsIdentity());
                }
            }
            await _next(context);
        }
    }

    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
