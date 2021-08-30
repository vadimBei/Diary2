using Common.Authentication.Identity.Common.Models;
using Common.Authentication.Identity.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Common.Authentication.JWT.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public UserModel User { get; private set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            User = new UserModel();

            User.Id = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id)))
            {
                User.ProfileId = 
                    Convert.ToInt64(httpContextAccessor.HttpContext?.User?
                                                                    .FindFirstValue(JwtClaimTypes.Id));
            }

            User.Roles = new List<string>();

            var userRoles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                                                                  .Select(c => c.Value)
                                                                  .ToList();

            if (userRoles != null)
            {
                User.Roles = userRoles;
            }
        }
    }
}
