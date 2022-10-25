using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsuariosAPI.Models.Entities;

namespace UsuariosAPI.Services
{
    public class TokenService
    {
        public static Token CreateToken(IdentityUser<int> user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            
            Claim[] userClaims = new Claim[]
            {
                new Claim("username", user.UserName),
                new Claim("id", user.Id.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("0j928jef09823jf9i0djf09asijfpjsnd0f1n9248un08rn3984n3hjnxoif")
                );

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                    claims: userClaims,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddHours(1)
                );

            string tokenValue = tokenHandler.WriteToken(token);

            return new Token(tokenValue);
        }
    }
}
