using Lean.Contracts.MessageModels;
using Newtonsoft.Json;

namespace Lean.Web.MiddleWares
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Policy;

    public class CustomAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            // If the authorization was forbidden and the resource had a specific requirement,
            if (authorizeResult.Forbidden
                && authorizeResult.AuthorizationFailure!.FailedRequirements
                    .OfType<Show404Requirement>().Any())
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(new ErrorMessage(RM.Common.YouShouldLoginFirst)));
                return;
            }

            // Fall back to the default implementation.
            await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }

    public class Show404Requirement : IAuthorizationRequirement
    {
    }
}