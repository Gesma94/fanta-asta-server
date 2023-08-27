using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Threading.Tasks;

namespace FantaAstaServer.Extensions
{
    public class RuoteRequestCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var culture = "en-US";

            if (httpContext.Request.RouteValues.TryGetValue("culture", out var objectCulture) && objectCulture is string aux)
            {
                culture = aux;
            }

            return Task.FromResult(new ProviderCultureResult(culture));
        }
    }
}
