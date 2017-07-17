using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace KendoProto1.Models
{
    public static class IdentityExtensions
    {
        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            if (ci != null)
            {
                var id = ci.FindFirst(ClaimTypes.NameIdentifier);
                if (id != null)
                {
                    return (T)Convert.ChangeType(id.Value, typeof(T), CultureInfo.InvariantCulture);
                }
            }
            return default(T);
        }

        public static string GetEmail(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;

            string email = "";
            if (ci != null)
            {
                var id = ci.FindFirst(ClaimTypes.Email);
                if (id != null)
                    email = id.Value.ToString();
            }
            return email;
        }

        public static string GetFio(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;

            string strFio = "";
            if (ci != null)
            {
                var id = ci.FindFirst("UserFio");
                if (id != null)
                    strFio = id.Value.ToString();
            }
            return strFio;
        }

        public static string GetUserRole(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            string role = "";
            if (ci != null)
            {
                var id = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
                if (id != null)
                    role = id.Value;
            }
            return role;
        }
    }
}