using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;

namespace QuakerZero
{
    /// <summary>
    /// JWT generator and handling class
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator{
        Logger logger = Logger.GetInstance();
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, JwtSettings jwtSettings){
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtSettings;
        }
        /// <summary>
        /// Generate a JWT
        /// </summary>
        /// <param name="number"></param>
        /// <param name="contractId"></param>
        /// <param name="claims"></param>
        /// <param name="expirationInMinutes"></param>
        /// <returns></returns>
        public string GenerateToken(string number, string contractId, Claim[] claims, int expirationInMinutes){
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
                );
            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimeProvider.UtcNow.AddMinutes(expirationInMinutes),
                claims: claims,
                signingCredentials: signingCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
        /// <summary>
        /// Validate JWT
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ClaimsPrincipal ValidateJwtToken(string token){
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero, // You can adjust the tolerance for expiration times
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = key
            };

            SecurityToken validatedToken;
            return tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
        }
        /// <summary>
        /// Return a string type claim (strings)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// Usage example: string claim = GetClaim("ContractId");
        public string GetClaim(string type, ClaimsPrincipal principal) { return principal.FindFirst(type)?.Value; }
        /// <summary>
        /// Return the Bearer Token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string GetBearerToken(HttpListenerContext context)
        {
            string header = context.Request.Headers["Authorization"]; //Get Authorization Header
            const string bearerPrefix = "Bearer "; //Token Type
            string bearerToken = string.Empty;
            if (header != null && header.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
            {
                bearerToken = header.Substring(bearerPrefix.Length);
            }
            return bearerToken;
        }
        //Overload
        public string GetBearerToken(string socketHeader){
            return socketHeader.Split("\r\n")[1].Replace("Authorization: Bearer ", "");
        }
    }
}
