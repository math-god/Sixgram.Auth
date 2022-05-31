using System;
using System.Security.Claims;

namespace Sixgram.Auth.Core.User
{
    public interface IUserIdentityService
    {
        public Guid GetCurrentUserId();
        public string GetClaim(string token, string claimType);
    }
}