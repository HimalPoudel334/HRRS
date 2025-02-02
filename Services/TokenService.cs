using HRRS.Dto.User;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HRRS.Services;


public class TokenService(IConfiguration configuration)
{
    public static TokenValidationParameters GetTokenValidationParameter(IConfiguration configuration) =>
        new()
        {
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:Issuer"],
            IssuerSigningKey = GetSecurityKey(configuration)
        };

    public string GenerateJwt(LoggedInUser user)
    {
        var securityKey = GetSecurityKey(configuration);

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        int expireMin = int.Parse(configuration["JWT:ExpiresInMinute"]!);

        Claim[] claims = [
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.UserRole)
            ];

        var token = new JwtSecurityToken(
            issuer: configuration["JWT:Issuer"],
            audience: "*",
            claims: claims,
            signingCredentials: credentials,
            expires: DateTime.Now.AddMinutes(expireMin)
            );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    private static SymmetricSecurityKey GetSecurityKey(IConfiguration configuration)
    {
        var secreteKey = configuration["JWT:SecreteKey"];
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secreteKey!));
        return securityKey;
    }
}
