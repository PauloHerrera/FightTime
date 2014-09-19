using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FightTime.Filters
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class FightTimeAuthorizeAtribute : AuthorizeAttribute
    {
        public string Haver;

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var teste = "32";
            return true;
            //var b = myMembership.Instance.Member().IsLoggedIn;
            ////Is user logged in?
            //if (b)
            //    //If user is logged in and we need a custom check:
            //    if (ResourceKey != null && OperationKey != null)
            //        return ecMembership.Instance.Member().ActivePermissions.Where(x => x.operation == OperationKey && x.resource == ResourceKey).Count() > 0;
            ////Returns true or false, meaning allow or deny. False will call HandleUnauthorizedRequest above
            //return b;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var teste = filterContext.HttpContext.User.Identity;

            var teste2 = Haver;

            if (HttpContext.Current.Session["User"] == null)
                base.HandleUnauthorizedRequest(filterContext);

            //var teste = "32";
        }



    }
}