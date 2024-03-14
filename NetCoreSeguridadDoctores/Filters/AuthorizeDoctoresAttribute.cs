using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreSeguridadDoctores.Filters
{
    public class AuthorizeDoctoresAttribute: AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            
            var user = context.HttpContext.User;
            if (user.Identity.IsAuthenticated == false)
            {
                //ENVIAMOS A LA VISTA LOGIN
                context.Result = this.GetRoute("Managed", "Login");
            }
            else
            {
                if (user.IsInRole("DIAGNOSTICO") == false &&
                    user.IsInRole("Carfiología") == false)
                {
                    context.Result = this.GetRoute("Managed", "ErrorAcceso");
                }
            }
        }

        private RedirectToRouteResult GetRoute
            (string controller, string action)
        {
            RouteValueDictionary ruta =
                new RouteValueDictionary(
                    new { controller = controller, action = action });
            RedirectToRouteResult result =
                new RedirectToRouteResult(ruta);
            return result;
        }

    }
}
