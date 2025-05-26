using LocalFarmerMarket.Core.Models;
using LocalFarmerMarket.ExtentionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LocalFarmerMarket.ActionFilters
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session.GetObject<Customer>("loggedCustomer") == null)
            {
                if (context.HttpContext.Session.GetObject<Customer>("loggedAdmin") != null)
                {
                    context.HttpContext.Session.SetObject<Customer>("loggedAdmin", null);

                }
                context.Result = new RedirectResult("/Home/Login");
            }

        }
    }
}
