using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XperiAndri.AspNet.Identity.EntityFramework.Models
{
    public partial class User : IUser<Guid>
    {
        Guid IUser<Guid>.Id
        {
            get { return UserId; }
        }
    }

    public partial class UserRole : IRole<Guid>
    {
        Guid IRole<Guid>.Id
        {
            get { return RoleId; }
        }
    }
}