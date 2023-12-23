

using System.Security.Claims;
using fStore.Core;
using Microsoft.AspNetCore.Authorization;

namespace fStore.WEBAPI;

public class AdminOrOwnerHandler : AuthorizationHandler<AdminOrOwnerRequirement, Order>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminOrOwnerRequirement requirement, Order order)
    {
        Console.WriteLine("Start");
        Console.WriteLine("Context", context);
        var claims = context.User;
        var userId = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var userRole = claims.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;
        Console.WriteLine("Current User Role in authorization {0}", userRole);
        if (userRole == Role.Admin.ToString())
        {
            Console.WriteLine("User is Admin");
            context.Succeed(requirement);
        }
        else if (userId == order.UserId.ToString())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;

    }
}

public class AdminOrOwnerRequirement : IAuthorizationRequirement { }



