using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CtrlBox.Domain.Security
{
    public static class PolicyTypes
    {
        public const string Create = "default.policy.create";
        public const string Read = "default.policy.read";
        public const string Update = "default.policy.update";
        public const string Delete = "default.policy.delete";
        public const string Index = "default.policy.index";

        public const string NAME = "default.policy";

        public static class DeliveryPolicy
        {
            public const string ExecuteDelivery = "delivery.policy.execute";
        }

        public static IDictionary<object, string> IdentityClaims
        {
            get
            {
                var defaultPolicies = new Dictionary<object, string>();
                foreach (var permission in Enum.GetNames(typeof(CRUD)))
                {
                    var result = (CRUD)Enum.Parse(typeof(CRUD), permission);
                    defaultPolicies.Add(result, $"{NAME}.{permission.ToLower()}");
                }

                defaultPolicies.Add(DeliveryPolicy.ExecuteDelivery, DeliveryPolicy.ExecuteDelivery);
                return defaultPolicies;
            }
        }

        public static IList<Claim> ListAllClaims
        {
            get
            {
                var claims = new List<Claim>();
                foreach (var permission in Enum.GetNames(typeof(CRUD)))
                {
                    var result = (CRUD)Enum.Parse(typeof(CRUD), permission);
                    claims.Add(new Claim(CustomClaimTypes.Permission, $"{NAME}.{permission.ToLower()}"));
                }

                claims.Add(new Claim(CustomClaimTypes.Permission, $"{DeliveryPolicy.ExecuteDelivery.ToString().ToLower()}"));
                return claims;
            }
        }
    }

    public enum DeliveryPolicy
    {
        ExecuteDelivery
    }

    public enum CRUD
    {
        Create,
        Read,
        Update,
        Delete,
        Index
    }
}