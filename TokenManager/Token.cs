using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;

namespace TokenManager
{
    public class Token
    {

        string key = "0801056087828679183689122577224206524434116185313874716265917559112757188028550139520367950825846233952754821836700847754937433356841048621704597691388771445792477781863965514274502302746445602392479965448986548978165909471900378486130041615604541031100293";
        JwtSecurityTokenHandler handler;
        SymmetricSecurityKey securityKey;


        public Token()
        {
            handler = new JwtSecurityTokenHandler();
            securityKey = new SymmetricSecurityKey(Convert.FromBase64String(key));
        }

        public string GenerateToken(ClaimsIdentity claims)
        {
            try
            {

                SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor()
                {
                    Subject = claims,
                    Expires = DateTime.Now.AddHours(1),
                    SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
                };

                JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);


                return handler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ClaimsPrincipal ValidateToken(string token)
        {

            try
            {
                JwtSecurityToken jwtToken = (JwtSecurityToken)handler.ReadToken(token);

                if (jwtToken == null)
                    return null;


                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = securityKey
                };

                SecurityToken securityToken;
                return handler.ValidateToken(token, validationParameters, out securityToken);
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }
    }
}
