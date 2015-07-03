using Microsoft.Data.Entity.Design.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Xml.Linq;

namespace XperiAndri.AspNetIdentity.EFDesignerExtension
{
    internal class IdentityEntityProperties
    {
        private readonly PropertyManager propertyManager;
        private readonly XElement parent;
        private readonly PropertyExtensionContext context;

        public IdentityEntityProperties(XElement parent, PropertyExtensionContext context)
        {
            this.propertyManager = new PropertyManager(parent.GetDefaultNamespace());
            this.context = context;
            this.parent = parent;
            this.parent.Changed += parentChanged;
        }

        void parentChanged(object sender, XObjectChangeEventArgs e)
        {
            var change = e.ObjectChange;
        }

        [DisplayName("User Entity Type")]
        [Description("An entity type which describes ASP.NET identity user.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public XElement UserEntity
        {
            get { return getValue(Constants.aspNetIdentityUserName); }
            set { setValue(Constants.aspNetIdentityUserName, value); }
        }

        [DisplayName("Role Entity Type")]
        [Description("An entity type which describes ASP.NET identity role.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public XElement RoleEntity
        {
            get { return getValue(Constants.aspNetIdentityUserRoleName); }
            set { setValue(Constants.aspNetIdentityUserRoleName, value); }
        }

        [DisplayName("User Login Entity Type")]
        [Description("An entity type which describes ASP.NET identity user login.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public XElement UserLoginEntity
        {
            get { return getValue(Constants.aspNetIdentityUserRoleName); }
            set { setValue(Constants.aspNetIdentityUserLoginName, value); }
        }

        [DisplayName("User Claim Entity Type")]
        [Description("An entity type which describes ASP.NET identity user claim.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public XElement UserClaimEntity
        {
            get { return getValue(Constants.aspNetIdentityUserClaimName); }
            set { setValue(Constants.aspNetIdentityUserClaimName, value); }
        }

        protected XElement getValue(XName name)
        {
            return propertyManager.GetValue(parent, name);
        }

        protected void setValue(XName name, XElement value)
        {
            propertyManager.SetValue(parent, context, name, value);
        }

    }
}