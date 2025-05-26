using LocalFarmerMarket.Core.Models;
using LocalFarmerMarket.ExtentionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LocalFarmerMarket.ActionFilters
{
    public class AdminAuthenticationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObject<Customer>("loggedAdmin") == null)
            {
                if (context.HttpContext.Session.GetObject<Customer>("loggedCustomer") != null)
                {
                    context.HttpContext.Session.SetObject<Customer>("loggedCustomer", null);

                }
                context.Result = new RedirectResult("/Home/Login");
            }
        }
    }
}
