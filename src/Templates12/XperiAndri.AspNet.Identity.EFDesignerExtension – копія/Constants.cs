using System;
using System.Xml.Linq;

namespace XperiAndri.AspNetIdentity.EFDesignerExtension
{
    internal class Constants
    {
        internal const string IdentityCategory = "ASP.NET Identity";

        internal static readonly string aspNetIdentityNamespace = "http://schemas.xperiandri.com/AspNetIdentity";
        internal static XName aspNetIdentityUserName = XName.Get("AspNetIdentityUser", aspNetIdentityNamespace);
        internal static XName aspNetIdentityUserRoleName = XName.Get("AspNetIdentityUserRole", aspNetIdentityNamespace);
        internal static XName aspNetIdentityUserLoginName = XName.Get("AspNetIdentityUserLogin", aspNetIdentityNamespace);
        internal static XName aspNetIdentityUserClaimName = XName.Get("AspNetIdentityUserClaim", aspNetIdentityNamespace);
    }
}
