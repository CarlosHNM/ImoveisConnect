using ImoveisConnect.API.Core.Helpers;
using ImoveisConnect.API.FilterAttributes;
using ImoveisConnect.Application.DTOS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace ImoveisConnect.API.Controllers.Core
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiAuthorizationAttribute]
    public class BaseApiController : ControllerBase
    {
        protected AccountLoggedDTO GetUsuarioLogado(string secret)
        {
            try
            {
                StringValues bearerAuthToken;
                Request.Headers.TryGetValue("Authorization", out bearerAuthToken);
                string token = bearerAuthToken.Count > 0 && !string.IsNullOrWhiteSpace(bearerAuthToken[0])
                    ? bearerAuthToken[0].Split(' ')[1]
                    : null;

                var principal = TokenHelper.GetPrincipal(secret, token);

                string login = "";
                var froLogin = principal.Claims.Where(c => c.Type == "Login").Select(x => x.Value).FirstOrDefault();
                if (froLogin != null)
                {
                    login = froLogin;
                }

                int usuarioId = 0;
                var froUsuarioId = principal.Claims.Where(c => c.Type == "UsuarioId").Select(x => x.Value).FirstOrDefault();
                if (froUsuarioId != null)
                {
                    int.TryParse(froUsuarioId, out usuarioId);
                }


                int perfilId = 0;
                var froPerfilId = principal.Claims.Where(c => c.Type == "PerfilId").Select(x => x.Value).FirstOrDefault();
                if (froUsuarioId != null)
                {
                    int.TryParse(froPerfilId, out perfilId);
                }

                return new AccountLoggedDTO(usuarioId, perfilId, login);
            }
            catch (Exception ex)
            {
                return new AccountLoggedDTO(0, 0, string.Empty);
            }
        }

    }
}
