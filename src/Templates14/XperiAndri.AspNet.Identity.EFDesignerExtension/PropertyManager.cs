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
        private static readonly XName aspNetIdentityXmlnsAttributeName = XNamespace.Xmlns + Constants.AspNetIdentityPrefix;
        private static readonly string entityTypeNameString = "EntityType";

        private readonly XName keyElementName;
        private readonly XName entityTypeName;
        private readonly XName entityContainerName;
        private readonly XName entitySetName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyManager"/> class.
        /// </summary>
        public PropertyManager(string schema)
        {
            keyElementName = XName.Get("Key", schema);
            entityTypeName = XName.Get(entityTypeNameString, schema);
            entityContainerName = XName.Get("EntityContainer", schema);
            entitySetName = XName.Get("EntitySet", schema);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyManager"/> class.
        /// </summary>
        public PropertyManager(XNamespace schema)
            : this(schema.NamespaceName)
        {
        }

        public string GetValue(XElement parent, string value)
        {
            return GetEntitySetsWithIdentityAttribute(parent, value).Select(es => GetEntityNameFromFullName(es.Attribute(entityTypeNameString).Value)).FirstOrDefault();
        }

        public void SetValue(XElement parent, PropertyExtensionContext context, string entityName, string identityType)
        {
            if (entityName == null)
                using (EntityDesignerChangeScope scope = context.CreateChangeScope("Set ASP.NET Identity Property"))
                {
                    RemoveIdentityAttributeOfType(parent, identityType);
                    scope.Complete();
                    return;
                }

            XElement set = GetEntitySetsByEntityName(parent, entityName).FirstOrDefault();
            if (set == null)
                throw new ArgumentException(string.Format("Entity Model does not contain any entity with name \"{0}\"." , entityName));

            var identityTypeAttribute = set.Attributes(Constants.aspNetIdentityAttributeName).FirstOrDefault();

            if (identityTypeAttribute != null)
            {
                if (identityTypeAttribute.Value == identityType)
                    return;

                DialogResult result = DialogResult.OK;
                string otherType = identityTypeAttribute.Value;
                string message = string.Format("Selected entity type is already used as a {0} identity. Would you like to set it as a {1} identity? A {0} identity will be unset.", otherType, identityType);
                string caption = string.Format("Entity is already in use as {0} identity!", otherType);
                result = MessageBox.Show(message, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel)
                    return;
                else
                    using (EntityDesignerChangeScope scope = context.CreateChangeScope("Set ASP.NET Identity Property"))
                    {
                        RemoveIdentityAttributeOfType(parent, identityType);
                        identityTypeAttribute.Value = identityType;
                        scope.Complete();
                    }
            }
            else
            {
                using (EntityDesignerChangeScope scope = context.CreateChangeScope("Set ASP.NET Identity Property"))
                {
                    if (!parent.Attributes(aspNetIdentityXmlnsAttributeName).Any())
                        parent.Add(new XAttribute(aspNetIdentityXmlnsAttributeName, Constants.AspNetIdentityNamespace));

                    identityTypeAttribute = new XAttribute(Constants.aspNetIdentityAttributeName, identityType);
                    set.Add(identityTypeAttribute);
                    scope.Complete();
                }
            }
        }

        private void RemoveIdentityAttributeOfType(XElement parent, string identityType)
        {
            foreach (var element in GetEntitySetsWithIdentityAttribute(parent, identityType))
                element.Attribute(Constants.aspNetIdentityAttributeName).Remove();
        }

        public IEnumerable<XElement> GetEntityTypes(XElement schema)
        {
            return from entityType in schema.Elements(entityTypeName)
                   where entityType.Elements(keyElementName).Any()
                   select entityType;
        }

        public IEnumerable<XElement> GetEntitySetsWithIdentityAttribute(XElement schema, string identityType)
        {
            return from entitySet in schema.Element(entityContainerName).Elements(entitySetName)
                   where entitySet.Attributes(Constants.aspNetIdentityAttributeName).Any(a => a.Value == identityType)
                   select entitySet;
        }

        public IEnumerable<XElement> GetEntitySetsByEntityName(XElement schema, string entityName)
        {
            return from entitySet in schema.Element(entityContainerName).Elements(entitySetName)
                   where entitySet.Attributes(entityTypeNameString).Any(a => GetEntityNameFromFullName(a.Value) == entityName)
                   select entitySet;
        }

        private static string GetEntityNameFromFullName(string fullName)
        {
            int lastDotIndex = fullName.LastIndexOf('.');
            return fullName.Substring(lastDotIndex + 1, fullName.Length - lastDotIndex - 1);
        }
    }
}