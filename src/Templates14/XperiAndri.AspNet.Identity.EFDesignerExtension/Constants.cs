using System;
using System.Xml.Linq;

namespace XperiAndri.AspNetIdentity.EFDesignerExtension
{
    internal class Constants
    {
        internal const string IdentityCategory = "ASP.NET Identity";

        internal static readonly string AspNetIdentityPrefix = "aspnetidentity";
        internal static readonly string AspNetIdentityNamespace = "http://schemas.xperiandri.com/AspNetIdentity";

        internal static XName aspNetIdentityAttributeName = XName.Get("AspNetIdentityType", AspNetIdentityNamespace);
    }

    internal static class IdentityTypes
    {
        internal static readonly string User = "User";
        internal static readonly string Login = "Login";
        internal static readonly string Claim = "Claim";
        internal static readonly string Role = "Role";
    }
}
