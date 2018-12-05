using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace VST.WebHelper
{
    public class VTSAuthorizerAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var role = ((ClaimsIdentity)filterContext.HttpContext.User.Identity).Claims
                                  .Where(c => c.Type == ClaimTypes.Role)
                                  .Select(c => c.Value);
                var userRepo = DependencyResolver.Current.GetService(typeof(UserRepository)) as UserRepository;
                var isAdmin = userRepo.IsAdmin(filterContext.HttpContext.User.Identity.Name);
                filterContext.Result = new RedirectResult(isAdmin ? "AdminDashboard" : "StudentDashboard");
            }
        }
    }
}