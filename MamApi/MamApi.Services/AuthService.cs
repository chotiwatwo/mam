using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MamApi.Models;
using MamApi.Models.Resources;
using System.Security.Cryptography;
using MamApi.Data.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace MamApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public User CheckCredential(string userName, string password)
        {
            string hashedPassword = MD5Hash(password).ToUpper();
            
            var user = _userRepo
                .FindByInclude(
                    u => u.UserId == userName && u.Password == hashedPassword,
                    i => i.Position, j => j.Department, k => k.Branch)
                .FirstOrDefault();

            //if (user != null && user.Count() > 0)
            //    return true;
            //else
            //    return false;

            return user;
        }

        private string MD5Hash(string unhashedPassword)
        {
            using (var md5 = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(unhashedPassword));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder result = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    result.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return result.ToString();
            }
        }

        public object CreateToken(UserProfileResource userProfile, string issuer, string privateKey)
        {
            var claims = new[]
                    {
                        // new Claim(JwtRegisteredClaimNames.Sub, userName),
                        new Claim(JwtRegisteredClaimNames.Sub, userProfile.UserId),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("userName", userProfile.UserName),
                        new Claim("positionId", userProfile.Position.Id),
                        new Claim("departmentId", userProfile.Department.Id),
                        new Claim("branchId", userProfile.Branch.Id)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: creds
            );

            var tokenResult = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                expiration = jwtToken.ValidTo
            };

            return tokenResult;
        }

        public async Task<string> GetBranchIdFromUserProfile(HttpContext httpContext)
        {
            string branchId = await GetClaimValueFromToken(httpContext, "branchId");

            return branchId ?? string.Empty;
        }

        private async Task<string> GetClaimValueFromToken(HttpContext httpContext, string claimType)
        {
            var accessToken = await AuthenticationHttpContextExtensions.GetTokenAsync(httpContext, "access_token");

            var claimValue = GetClaimValue(accessToken, claimType);

            return claimValue;
        }

        private string GetClaimValue(string accessToken, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();

            var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;

            return tokenS.Claims.First(claim => claim.Type == claimType).Value;
        }

    }
}
