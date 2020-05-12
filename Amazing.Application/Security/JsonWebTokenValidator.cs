using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Amazing.Application.Security
{
    public interface IJsonWebTokenValidator
    {
        List<Claim> ValidateAndDecode(string jwt);
    }

    public class JsonWebTokenValidator : IJsonWebTokenValidator
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JsonWebTokenValidator(string key, string issuer, string audience)
        {
            this._key = key;
            this._audience = audience;
            this._issuer = issuer;
        }

        /// <summary>
        /// Validate and decode a JWT
        /// </summary>
        /// <param name="jwt"></param>
        /// <returns></returns>
        public List<Claim> ValidateAndDecode(string jwt)
        {
            if (string.IsNullOrEmpty(jwt)) return null;

            jwt = jwt.Replace("bearer", string.Empty).TrimStart().TrimEnd();

            var validationParameters = new TokenValidationParameters
            {
                IssuerSigningKeys = new[] { new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this._key)) },
                RequireSignedTokens = true,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ValidIssuer = this._issuer,
                ValidAudience = this._audience
            };

            try
            {
                var claimsPrincipal = new JwtSecurityTokenHandler()
                    .ValidateToken(jwt, validationParameters, out var rawValidatedToken);

                return claimsPrincipal.Claims.ToList();
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                // TODO: Log it or display an error.
                throw new Exception($"Token failed validation: {stvex.Message}");
            }
            catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                // TODO: Log it or display an error.
                throw new Exception($"Token was invalid: {argex.Message}");
            }
        }
    }
}