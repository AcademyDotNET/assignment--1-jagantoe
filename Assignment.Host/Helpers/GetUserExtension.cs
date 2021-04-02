using System.Linq;
using System.Security.Claims;

namespace MVC_Assignment.Helpers
{
    public static class ClaimsPrincipalExtension
    {
        public static string GetUser(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.First().Value;
        }
    }
}
