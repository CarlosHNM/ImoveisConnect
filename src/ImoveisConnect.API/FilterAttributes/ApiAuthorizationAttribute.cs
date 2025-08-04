using ImoveisConnect.API.Core.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using ImoveisConnect.Application;

namespace ImoveisConnect.API.FilterAttributes
{
    public class ApiAuthorizationAttribute : ActionFilterAttribute, IActionFilter
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata
            .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            if (allowAnonymous)
            {
                base.OnActionExecuting(context);
                return;
            }

            string path = context.HttpContext.Request.Path.Value.ToLower().Trim();
            if (!path.Contains("/api/account"))
            {
                StringValues referValues;

                bool isValidToken = false;
                try
                {
                    IOptions<ApplicationConfig> appConfig = context.HttpContext.RequestServices.GetService<IOptions<ApplicationConfig>>();

                    StringValues bearerAuthToken;
                    context.HttpContext.Request.Headers.TryGetValue("Authorization", out bearerAuthToken);
                    string token = bearerAuthToken.Count > 0 && !string.IsNullOrWhiteSpace(bearerAuthToken[0])
                        ? bearerAuthToken[0].Split(' ')[1]
                        : null;

                    string secret = appConfig.Value.SecurityConfig.Secret;

                    var tokenValidation = TokenHelper.IsValidToken(secret, token);
                    isValidToken = tokenValidation.Item2;

                    if (!isValidToken)
                        context.Result = new ForbidResult();
                }
                catch
                {
                    context.Result = new ForbidResult();
                }

                if (!isValidToken)
                    context.Result = new UnauthorizedResult();
            }

            base.OnActionExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }
    }
}
