using ImoveisConnect.API.Controllers.Core;
using ImoveisConnect.API.Core.Helpers;
using ImoveisConnect.API.Core.Response;
using ImoveisConnect.Application;
using ImoveisConnect.Application.DTOS;
using ImoveisConnect.Domain.DTOS.Response;
using ImoveisConnect.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ImoveisConnect.API.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly IAccountService _accountService;
        private readonly IOptions<ApplicationConfig> _appConfig;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAccountService accountService,
            IOptions<ApplicationConfig> applicationConfig,
            ILogger<AuthController> logger)
        {
            _accountService = accountService;
            _appConfig = applicationConfig;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] SignInDTO signIn)
        {
            try
            {
                // Validação básica do modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response<object>(ModelState.GetValidationErrors()));
                }

                // Autenticação do usuário
                var result = await _accountService.PostLoginAsync(
                    signIn,
                    _appConfig.Value.TokenExpirationHours,
                    _appConfig.Value.UsersByPass_HML);

                if (result.Success == null || result.Data == null)
                {
                    _logger.LogWarning($"Tentativa de login falhou para o usuário: {signIn.Login}");
                    return Unauthorized(result);
                }

                // Geração do token JWT
                var claims = new Dictionary<string, string>
                {
                    { ClaimTypes.NameIdentifier, result.Data.UsuarioId.ToString() },
                    { ClaimTypes.Name, result.Data.Login },
                    { ClaimTypes.Role, result.Data.DsPerfilSistema },
                    { "DsPerfil", result.Data.DsPerfil },
                    { "PerfilId", result.Data.PerfilId.ToString() },
                    { "UsuarioId", result.Data.UsuarioId.ToString() }
                };

                var token = TokenHelper.GenerateToken(
                    secret: _appConfig.Value.SecurityConfig.Secret,
                    expires: _appConfig.Value.TokenExpirationHours,
                    login: result.Data.Login,
                    perfis: new List<string> { result.Data.DsPerfilSistema },
                    outrosDados: claims
                );

                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogError("Falha ao gerar token JWT");
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        new Response<object>("Falha ao gerar token de autenticação"));
                }

                result.Data.SetToken(token);
                _logger.LogInformation($"Login bem-sucedido para o usuário: {result.Data.Login}");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro durante o processo de login");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<object>("Ocorreu um erro durante o login"));
            }
        }

        [HttpPost("refresh-token")]
        [Authorize]
        [ProducesResponseType(typeof(Response<AuthResponseDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response<object>), StatusCodes.Status401Unauthorized)]
        public IActionResult RefreshToken()
        {
            try
            {
                var user = HttpContext.User;
                var login = user.FindFirst(ClaimTypes.Name)?.Value;
                var role = user.FindFirst(ClaimTypes.Role)?.Value;
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(login))
                    return Unauthorized();

                var claims = new Dictionary<string, string>();
                foreach (var claim in user.Claims)
                {
                    claims.TryAdd(claim.Type, claim.Value);
                }

                var newToken = TokenHelper.GenerateToken(
                    _appConfig.Value.SecurityConfig.Secret,
                    _appConfig.Value.TokenExpirationHours,
                    login,
                    new List<string> { role },
                    claims
                );

                return Ok(new Response<AuthResponseDTO>(new AuthResponseDTO
                {
                    Token = newToken,
                    Login = login,
                    DsPerfilSistema = role,
                    UsuarioId = int.Parse(userId)
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao renovar token");
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response<object>("Falha ao renovar token"));
            }
        }
    }
}
