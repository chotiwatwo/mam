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
        User CheckCredential(string userName, string password);

        object CreateToken(UserProfileResource userProfile, string issuer, string privateKey);

        Task<string> GetBranchIdFromUserProfile(HttpContext httpContext);
    }
}
