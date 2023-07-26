using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace TestWebApi.Helpers;

public class Jwt
{
    private readonly IConfiguration _configuration;

    public Jwt(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string Generate(string userId, string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var secret = _configuration.GetSection("Jwt")["Secret"];
        var key = Encoding.UTF8.GetBytes(secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Email, email),
            }),
            Expires = DateTime.Now.AddMinutes(60),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public ClaimsPrincipal ValidateToken(string jwtToken)
    {
        IdentityModelEventSource.ShowPII = true;

        var validationParameters = new TokenValidationParameters();

        validationParameters.ValidateLifetime = true;

        validationParameters.ValidAudience = null;
        validationParameters.ValidIssuer = null;
        validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Secret"]));

        var principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out _);
        
        return principal;
    }
}