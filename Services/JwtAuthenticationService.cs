using Microsoft.IdentityModel.Tokens;
using MVPSA_V2022.clases;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVPSA_V2022.Services
{
    public class JwtAuthenticationService : IJwtAuthenticationService
    {
        private readonly string _key;

        public JwtAuthenticationService(string key) {
            _key = key;
        }

        public LoginResponseCLS getToken(int idUsuario, string codRol)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new System.Security.Claims.ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Sid, idUsuario.ToString()),
                            new Claim(ClaimTypes.Role, codRol),
                            new Claim(ClaimTypes.Expiration, 3600.ToString())
                        }
                    ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenId = tokenHandler.WriteToken(token);

            LoginResponseCLS loginResponse = new LoginResponseCLS();
            loginResponse.tokenId = tokenId;
            loginResponse.expiresAt = 3600;

            return loginResponse;
        }
    }
}
