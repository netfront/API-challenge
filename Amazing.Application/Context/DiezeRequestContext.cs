using Amazing.Application.Exceptions;
using Amazing.Persistence.Enumerators;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace Amazing.Application.Context
{
    public class AmazingRequestContext
    {
        public List<Claim> Claims { get; private set; } = null;
        public bool IsUserAuthentified { get; private set; } = false;
        public string Auhtorization { get; private set; } = string.Empty;

        public AmazingRequestContext WithAuthorization(string header)
        {
            this.Auhtorization = header;
            return this;
        }

        public AmazingRequestContext WithUserDetail(List<Claim> claims)
        {
            this.Claims = claims;
            this.IsUserAuthentified = true;
            return this;
        }

        public AmazingRequestContext HasPolicy(EJwtType type)
        {
            if (this.Claims.FirstOrDefault(claim => claim.Type == "Type")?.Value != type.ToString())
                throw new AmazingException(HttpStatusCode.Unauthorized, $"Unauthorized");
            return this;
        }

        /// <summary>
        /// Check if the authorization header has a specific role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public AmazingRequestContext HasRole(EUserRole role)
        {
            if (this.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value != role.ToString())
                throw new AmazingException(HttpStatusCode.Unauthorized, $"Unauthorized");
            return this;
        }

        /// <summary>
        /// Get the User Id from the claims
        /// </summary>
        /// <returns></returns>
        public int GetUserIdFromBearer()
            => int.Parse(this.Claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value);

        /// <summary>
        /// Get a specific claim
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetClaim(string type)
            => this.Claims.FirstOrDefault(c => c.Type == type).Value;
    }
}