using Microsoft.Data.Entity.Design.Extensibility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Runtime.CompilerServices;
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

        [DisplayName("User Entity")]
        [Description("An entity type which describes ASP.NET identity user.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public string UserEntity
        {
            get { return getValue(IdentityTypes.User); }
            set { setValue(IdentityTypes.User, value); }
        }

        [DisplayName("Role Entity")]
        [Description("An entity type which describes ASP.NET identity user role.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public string UserRoleEntity
        {
            get { return getValue(IdentityTypes.Role); }
            set { setValue(IdentityTypes.Role, value); }
        }

        [DisplayName("Login Entity")]
        [Description("An entity type which describes ASP.NET identity user login.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public string UserLoginEntity
        {
            get { return getValue(IdentityTypes.Login); }
            set { setValue(IdentityTypes.Login, value); }
        }

        [DisplayName("Claim Entity")]
        [Description("An entity type which describes ASP.NET identity user claim.")]
        [Category(Constants.IdentityCategory)]
        [Editor(typeof(IdentityEntityPropertyEditor), typeof(UITypeEditor))]
        public string UserClaimEntity
        {
            get { return getValue(IdentityTypes.Claim); }
            set { setValue(IdentityTypes.Claim, value); }
        }

        protected string getValue(string identityType)
        {
            return propertyManager.GetValue(parent, identityType);
        }

        protected void setValue(string identityType, string entityName)
        {
            propertyManager.SetValue(parent, context, entityName, identityType);
        }

    }
}