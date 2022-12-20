using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FilmesAPI.Authorization
{
    public class MinAgeHandler : AuthorizationHandler<MinAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirement requirement)
        {
            if(!context.User.HasClaim(claim => claim.Type == ClaimTypes.DateOfBirth))
                return Task.CompletedTask;

            DateTime birthDate = Convert.ToDateTime(context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth).Value);

            int currentAge = DateTime.Today.Year - birthDate.Year;

            if (birthDate > DateTime.Today.AddYears(-currentAge))
                currentAge--;

            if (currentAge >= requirement.MinAge)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
