using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MVPSA_V2022.Interceptors
{
    public class RequestUserMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Obtengo el token de seguridad
            string token = context.Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            if (token == null || token == "") {
                // Si no se envio un token (por ejemplo si es un endpoint que no lo requiere), sigue
                // el flujo normal
                await _next(context);
            }

            // con el token obtengo el id de usuario
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
            string usuarioId = jwt.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

            // seteo el id de usuario como un header
            context.Request.Headers.Add("id_usuario", usuarioId);

            // ese nuevo header "id_usuario" llega al controlador
            await _next(context);
        }
    }
}
