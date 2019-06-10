using CtrlBox.Domain.Security;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Linq;

namespace CtrlBox.UI.Web.Extensions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeEnum : AuthorizeAttribute
    {
        public AuthorizeEnum(params object[] roles)
        {
            if (roles.Any(r => r.GetType().BaseType != typeof(Enum)))
                throw new ArgumentException("roles");

            this.Roles = string.Join(", ", roles.Select(r => Enum.GetName(r.GetType(), r)));
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizePolicyEnum : AuthorizeAttribute
    {
        public AuthorizePolicyEnum(object policy)
        {
            if (policy.GetType().BaseType != typeof(Enum))
                throw new ArgumentException("policies");

            this.Policy = PolicyTypes.ListAllClaims[(CRUD)policy].Value;
        }
    }
}
