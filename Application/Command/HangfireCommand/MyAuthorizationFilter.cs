using Application.Wrapper;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using System.Text;

namespace Application.Command.HangfireCommand
{
    [ExcludeFromCodeCoverage]
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {

        public static string name { get; set; } = string.Empty;
        public static string pass { get; set; } = string.Empty;


        public bool Authorize([Hangfire.Annotations.NotNull] DashboardContext context)
        {
            HttpContext httpcontext = context.GetHttpContext();
            StringValues stringValues = httpcontext.Request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(stringValues))
            {
                SetChallengeResponse(httpcontext);
                return false;
            }

            AuthenticationHeaderValue authValues = AuthenticationHeaderValue.Parse(stringValues);
            Extract_Authentication_Tokens(authValues);
            var userManager = httpcontext.RequestServices.GetService<IUserWrapper>();
            var user = userManager.FindByNameAsy(name).GetAwaiter().GetResult();

            if (user == null)
            {
                SetChallengeResponse(httpcontext);
                return false;
            }

            if (userManager.GetRolesAsy(user).GetAwaiter().GetResult().First().Equals("Admin"))
            {
                if (userManager.CheckPWAsync(user, pass).GetAwaiter().GetResult())
                {
                    return true;
                }
            }
            SetChallengeResponse(httpcontext);
            return false;
        }

        private void SetChallengeResponse(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Append("WWW-Authenticate", "Basic realm=\"Hangfire Dashboard\"");
            httpContext.Response.WriteAsync("Authentication is required.");
        }
        private static void Extract_Authentication_Tokens(AuthenticationHeaderValue authValues)
        {
            string @string = Encoding.UTF8.GetString(Convert.FromBase64String(authValues.Parameter));
            string[] tokens = @string.Split(':');
            name = tokens[0];
            pass = tokens[1];
            return;
        }
    }
}
