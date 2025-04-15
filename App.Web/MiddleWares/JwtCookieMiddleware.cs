using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace App.Web.MiddleWares
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User?.Identity != null && context.User.Identity.IsAuthenticated)
            {
                await _next(context);
                return;
            }

            var token = context.Request.Cookies["JWTToken"];

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var claims = jwtToken.Claims;
                var identity = new ClaimsIdentity(claims, "jwt");
                var principal = new ClaimsPrincipal(identity);

                context.User = principal;
            }

            await _next(context);
        }

    }
}