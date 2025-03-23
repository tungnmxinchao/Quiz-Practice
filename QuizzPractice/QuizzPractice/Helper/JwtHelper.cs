using System.Security.Claims;

namespace QuizzPractice.Helper
{
    public class JwtHelper
    {
        public static int GetUserIdFromJwt(HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return -1;
            }

            var userIdClaim = httpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim != null ? int.Parse(userIdClaim.Value) : -1;
        }
    }
}
