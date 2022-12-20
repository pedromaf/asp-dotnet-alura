using Microsoft.AspNetCore.Authorization;

namespace FilmesAPI.Authorization
{
    public class MinAgeRequirement : IAuthorizationRequirement
    {
        public int MinAge { get; set; }

        public MinAgeRequirement(int minAge)
        {
            MinAge = minAge;
        }
    }
}
