using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjectAPI.Endpoints.Security
{
    public class TokenPost
    {
        public static string Template => "/Token";
        public static string[] Methods => new[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        [AllowAnonymous]
        public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
            if (user == null)
                return Results.BadRequest("Usuário não encontrado");

            var isPasswordValid = userManager.CheckPasswordAsync(user, loginRequest.Password).Result;
            if (!isPasswordValid)
                return Results.BadRequest("Senha inválida");

            var claims = userManager.GetClaimsAsync(user).Result;
            var subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, loginRequest.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),

                });
                subject.AddClaims(claims);

            var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer =configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Results.Ok(new
            {
                Token = tokenHandler.WriteToken(token)
            });
        }
    }
}
