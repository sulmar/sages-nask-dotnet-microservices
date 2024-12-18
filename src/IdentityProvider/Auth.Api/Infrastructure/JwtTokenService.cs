using Auth.Api.Abstractions;
using Auth.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Api.Infrastructure;

// dotnet add package  Microsoft.IdentityModel.JsonWebTokens

public class JwtTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity identity)
    {
        var current_time = DateTime.UtcNow;
        var expiration_time = current_time.AddDays(10);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "public_key"),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(expiration_time).ToUnixTimeSeconds().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identity.Email),
            new Claim(JwtRegisteredClaimNames.Name, identity.Username),
            new Claim(JwtRegisteredClaimNames.GivenName, identity.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, identity.LastName),
        };


        string privateKey = "your_secret_key_your_secret_key_your_secret_key";
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(privateKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration_time, signingCredentials: credentials);
        var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt_token;
    }
}
