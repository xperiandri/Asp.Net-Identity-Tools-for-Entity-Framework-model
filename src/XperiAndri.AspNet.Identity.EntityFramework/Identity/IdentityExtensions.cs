using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

using Microsoft.AspNet.Identity;

namespace XperiAndri.AspNet.Identity.EntityFramework.Identity
{
    internal static class IdentityExtensions
    {
        public static Guid GetUserGuidId(this IIdentity identity)
        {
            return new Guid (identity.GetUserId());
        }

    }
}