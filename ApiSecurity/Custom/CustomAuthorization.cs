using Microsoft.AspNetCore.Authorization;

namespace ApiSecurity.Custom
{
    public class CustomAuthorization : AuthorizeAttribute
    {
        public CustomAuthorization(string policy) : base(policy)
        {
        }
        public CustomAuthorization(string[] policies) : base()
        {
            foreach (string policy in policies)
            {
                base.Policy = policy;
            }
        }
    }
}
