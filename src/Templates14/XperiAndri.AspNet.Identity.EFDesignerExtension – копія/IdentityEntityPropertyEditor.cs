using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace XperiAndri.AspNetIdentity.EFDesignerExtension
{
    public class IdentityEntityPropertyEditor : ObjectSelectorEditor
    {
        private static readonly PropertyInfo wrappedItemProperty;
        private static readonly PropertyInfo xElementProperty;

        static IdentityEntityPropertyEditor()
        {
            Type efEntityModelDescriptorType = Type.GetType("Microsoft.Data.Entity.Design.UI.ViewModels.PropertyWindow.Descriptors.EFEntityModelDescriptor, Microsoft.Data.Entity.Design", true);
            wrappedItemProperty = efEntityModelDescriptorType.GetProperty("WrappedItem");
            Type conceptualEntityModelType = Type.GetType("Microsoft.Data.Entity.Design.Model.Entity.ConceptualEntityModel, Microsoft.Data.Entity.Design.Model", true);
            xElementProperty = conceptualEntityModelType.GetProperty("XObject", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        protected override void FillTreeWithData(Selector selector, ITypeDescriptorContext context, IServiceProvider provider)
        {
            base.FillTreeWithData(selector, context, provider);

            var model = xElementProperty.GetValue(wrappedItemProperty.GetValue(context.Instance)) as XElement;

            selector.AddNode("(None)", null, null);
            foreach (XElement entityType in new PropertyManager(model.GetDefaultNamespace()).GetEntityTypes(model))
            {
                selector.AddNode(entityType.Attribute("Name").Value, entityType, null);
            }
        }
    }
}