using DAL;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using Utility;

namespace VST
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            if (FormsAuthentication.CookiesSupported)
            {
                if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
                {
                    IRepository<VTSUSER, long> userRepository = new UserRepository();
                    string username = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
                    // TODO : Need to set Roles based On Areas and Authorize properly.
                    var user = (userRepository as UserRepository).Get(username);
                    string rolename = user.IsAdmin ? VTSConstants.Admin : VTSConstants.User;
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(
                        new GenericIdentity(username, "Forms"), new string[] { rolename });
                }
            }
        }
    }
}
