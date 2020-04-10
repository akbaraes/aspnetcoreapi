using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TwitterBook.Extensions
{
    public static class IdentityExtension
    {

        public static string GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null)
                return string.Empty;

            return httpContext.User.Claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}
