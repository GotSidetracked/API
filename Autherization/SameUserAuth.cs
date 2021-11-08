using KTU_API.Autherization.Model;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTU_API.Autherization
{
    public class SameUserAuth : AuthorizationHandler<SameUserRequirement, IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameUserRequirement requirement, IUserOwnedResource resource)
        {
            if (context.User.FindFirst(CustomClaims.UserID).Value == resource.UserID || 
                context.User.IsInRole(UserRoles.Admin))
            {
                context.Succeed(requirement);
            }


            return Task.CompletedTask;
        }
    }

    public record SameUserRequirement : IAuthorizationRequirement;
}
