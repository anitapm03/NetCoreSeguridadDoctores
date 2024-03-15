using Microsoft.AspNetCore.Authorization;

namespace NetCoreSeguridadDoctores.Policies
{
    public class OverSalarioRequirement :
        AuthorizationHandler<OverSalarioRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync
            (AuthorizationHandlerContext context, 
            OverSalarioRequirement requirement)
        {
            //podemos preguntar si existe el claim
            if (context.User.HasClaim(x=> x.Type == "Salario") ==false)
            {
                context.Fail();
            }
            else
            {
                string data = context.User.FindFirst("Salario").Value;
                int salario = int.Parse(data);
                if(salario >= 200000){
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
