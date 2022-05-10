using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Framework.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Framework.Infrastructure
{
    public class AuthHelper : IAuthHelper
    {
        #region inj

        private readonly IHttpContextAccessor _contextAccessor;

        public AuthHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        #endregion

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public string AccountRole()
        {
            return IsAuthenticated()
                ? _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value
                : null;
        }

        public long AccountId()
        {
            return IsAuthenticated()
                ? long.Parse(_contextAccessor.HttpContext.User.Claims.First(x => x.Type == "AccountId")?.Value)
                : 0;
        }

        public string AccountMobile()
        {
            return IsAuthenticated()
                ? _contextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.MobilePhone)?.Value
                : "";
        }

        public List<int> AccountPermissions()
        {
            if (!IsAuthenticated()) return new List<int>();
            var permissions = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Permissions")?.Value;
            return JsonConvert.DeserializeObject<List<int>>(permissions);
        }

        public AuthViewModel AccountInfo()
        {
            var result = new AuthViewModel();

            if (!IsAuthenticated()) return result;

            var claims = _contextAccessor.HttpContext.User.Claims.ToList();

            result.Id = long.Parse(claims.FirstOrDefault(x => x.Type == "AccountId").Value);
            result.Fullname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            result.RoleId = long.Parse(claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value);
            result.Role = Roles.GetRoleBy(result.RoleId);
            result.Username = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            result.Mobile = claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone).Value;
            result.ProfileImg = claims.FirstOrDefault(x => x.Type == "ProfileImg").Value;

            return result;
        }

        public void Signin(AuthViewModel account)
        {
            var permissions = JsonConvert.SerializeObject(account.Permissions);

            var claims = new List<Claim>
            {
                new Claim("AccountId", account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname),
                new Claim(ClaimTypes.Role, account.RoleId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, account.Username), // Or Use ClaimTypes.NameIdentifier
                new Claim(ClaimTypes.MobilePhone, account.Mobile),
                new Claim("ProfileImg", account.ProfileImg),
                new Claim("Permissions", permissions),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(30)
            };

            _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}