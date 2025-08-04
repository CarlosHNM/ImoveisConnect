using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ImoveisConnect.API.Core.Helpers
{
    public static class TokenHelper
    {
        private static string _generateSuperSecret(string secret)
        {
            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("Secret não pode ser nulo ou vazio");

            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(secret));
            return Convert.ToBase64String(hmac.Key);
        }

        public static ClaimsPrincipal GetPrincipal(string secret, string token)
        {
            if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(token))
                return null;

            try
            {
                var superSecret = _generateSuperSecret(secret);
                var tokenHandler = new JwtSecurityTokenHandler();

                // Validação preliminar do token
                if (!tokenHandler.CanReadToken(token))
                    return null;

                var symmetricKey = Convert.FromBase64String(superSecret);
                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                    ClockSkew = TimeSpan.Zero // Remove tolerância de tempo
                };

                return tokenHandler.ValidateToken(token, validationParameters, out _);
            }
            catch (SecurityTokenException stEx)
            {
                // Log específico para erros de token (opcional)
                return null;
            }
            catch (Exception ex)
            {
                // Log genérico (opcional)
                return null;
            }
        }

        public static Tuple<ClaimsPrincipal, bool> IsValidToken(string secret, string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return new Tuple<ClaimsPrincipal, bool>(null, false);

            var principal = GetPrincipal(secret, token);
            var isValid = principal?.Identity?.IsAuthenticated == true;

            return new Tuple<ClaimsPrincipal, bool>(principal, isValid);
        }

        public static bool IsTokenNotExpired(string expirationExp)
        {
            if (string.IsNullOrWhiteSpace(expirationExp))
                return false;

            if (!long.TryParse(expirationExp, out var tokenTicks))
                return false;

            try
            {
                var tokenDate = DateTimeOffset.FromUnixTimeSeconds(tokenTicks).UtcDateTime;
                return tokenDate >= DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }

        public static string GenerateToken(string secret, int expires, string login,
            List<string> perfis, Dictionary<string, string> outrosDados = null)
        {
            if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(login))
                throw new ArgumentException("Parâmetros essenciais não fornecidos");

            var tokenHandler = new JwtSecurityTokenHandler();
            var superSecret = _generateSuperSecret(secret);
            var symmetricKey = Convert.FromBase64String(superSecret);

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim(ClaimTypes.Name, login));

            perfis?.ForEach(perfil => identity.AddClaim(new Claim(ClaimTypes.Role, perfil)));

            outrosDados?.ToList().ForEach(dado =>
                identity.AddClaim(new Claim(dado.Key, dado.Value)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddHours(expires),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public static string GenerateTemporaryToken(string secret, int seconds = 5)
        {
            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("Secret não pode ser nulo");

            var tokenHandler = new JwtSecurityTokenHandler();
            var superSecret = _generateSuperSecret(secret);
            var symmetricKey = Convert.FromBase64String(superSecret);

            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("tipo", "questionario"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddSeconds(seconds),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
