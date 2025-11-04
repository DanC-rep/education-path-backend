using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EducationPath.Framework.Authorization;

public class TokenValidationParametersFactory
{
    public static TokenValidationParameters CreateWithLifeTime(AuthOptions authOptions) =>
        new()
        {
            ValidIssuer = authOptions.Issuer,
            ValidAudience = authOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            ClockSkew = TimeSpan.Zero
        };
    
    public static TokenValidationParameters CreateWithoutLifeTime(AuthOptions authOptions) =>
        new()
        {
            ValidIssuer = authOptions.Issuer,
            ValidAudience = authOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.Key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
}