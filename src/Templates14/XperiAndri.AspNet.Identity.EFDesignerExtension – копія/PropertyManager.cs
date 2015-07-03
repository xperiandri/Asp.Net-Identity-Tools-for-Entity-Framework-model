using Microsoft.Data.Entity.Design.Extensibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace XperiAndri.AspNetIdentity.EFDesignerExtension
{
    internal class PropertyManager
    {
        private static readonly XName[] propertyNames = new XName[]
        {
            Constants.aspNetIdentityUserName,
            Constants.aspNetIdentityUserLoginName,
            Constants.aspNetIdentityUserRoleName,
            Constants.aspNetIdentityUserClaimName
        };

        private readonly XName keyElementName;
        private readonly XName entityTypeName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyManager"/> class.
        /// </summary>
        public PropertyManager(string schema)
        {
            keyElementName = XName.Get("Key", schema);
            entityTypeName = XName.Get("EntityType", schema);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyManager"/> class.
        /// </summary>
        public PropertyManager(XNamespace schema) : this(schema.NamespaceName)
        {
        }

        public XElement GetValue(XElement parent, XName name)
        {
            return GetEntityTypesWithIdentityElement(parent, name).FirstOrDefault();
        }

        public void SetValue(XElement parent, PropertyExtensionContext context, XName name, XElement value)
        {
            if (value == null)
            {
                foreach (var element in GetEntityTypesWithIdentityElement(parent, name))
                    element.Element(name).Remove();
                return;
            }

            var otherElements = (from element in value.Elements()
                              let otherName = element.Name
                              where propertyNames.Contains(otherName) && otherName != name
                              select element).ToList();
            if (otherElements.Any())
            {
                DialogResult result = DialogResult.OK;
                if (GetEntityTypesWithIdentityElement(parent, otherName).Count() == 1)
                {
                    string nameString = name.LocalName.Substring(name.LocalName.Length - 5, 4);
                    string otherNameString = otherName.LocalName.Substring(otherName.LocalName.Length - 5, 4);
                    string message = string.Format("Selected entity type is already used as a {0} identity. Would you like to set it as a {1} identity? A {0} identity will be unset.", otherName, name);
                    string caption = string.Format("Entity is already in use as {0} identity!", otherName);
                    result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                if (result == DialogResult.Cancel)
                    return;
                else
                    value.Element(otherName).Remove();
            }

            using (EntityDesignerChangeScope scope = context.CreateChangeScope("Set ASP.NET Identity Property"))
            {
                foreach (var element in GetEntityTypesWithIdentityElement(parent, name))
                {
                    if (element != value)
                        element.Element(name).Remove();
                }

                if (value.Elements(name).Count() < 1)
                    value.FirstNode.AddBeforeSelf(new XElement(name));

                // Commit the changes.
                scope.Complete();
            }
        }

        public IEnumerable<XElement> GetEntityTypes(XElement schema)
        {
            return from entityType in schema.Elements(entityTypeName)
                   where entityType.Elements(keyElementName).Any()
                   select entityType;
        }

        public IEnumerable<XElement> GetEntityTypesWithIdentityElement(XElement schema, XName identitElementName)
        {
            return from entityType in schema.Elements(entityTypeName)
                   where entityType.Elements(identitElementName).Any()
                   select entityType;
        }
    }
}