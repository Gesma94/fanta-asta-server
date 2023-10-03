using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace FantaAstaServer.Extensions
{
    public static class HttpContextExts
    {
        public static int GetUserIdFromCookieClaim(this HttpContext httpContext)
        {
            var stringId = httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (stringId != null && int.TryParse(stringId, out int userId))
            {
                return userId;
            }

            throw new InvalidOperationException($"id '{stringId}' is not a valid id");
        }
    }
}
