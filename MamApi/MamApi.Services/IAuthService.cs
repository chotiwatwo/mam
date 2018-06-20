using MamApi.Models;
using MamApi.Models.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MamApi.Services
{
    public interface IAuthService
    {
        User CheckCredential(string userId, string password);

        object CreateToken(UserProfileResource userProfile, string issuer, string privateKey);

        Task<UserProfile> GetUserProfileFromToken(HttpContext httpContext);

        long SaveCurrentLogin(LoginResource userProfile);

        DateTime Logout(string userId);
    }
}
