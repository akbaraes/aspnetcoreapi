using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TwitterBook.Authorization
{
    public class WorkForCompanyHandler : AuthorizationHandler<WorkForCompanyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WorkForCompanyRequirement requirement)
        {
            var email = context.User?.FindFirst(ClaimTypes.Email).Value ?? string.Empty;

            if(email.EndsWith(requirement.DomainName))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
