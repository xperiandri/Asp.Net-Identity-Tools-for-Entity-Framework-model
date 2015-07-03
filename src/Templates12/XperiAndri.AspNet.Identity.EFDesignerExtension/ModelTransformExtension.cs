using Microsoft.Data.Entity.Design.Extensibility;
using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Xml.Linq;

namespace XperiAndri.AspNetIdentity.EFDesignerExtension
{
    [Export(typeof(IModelTransformExtension))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    internal class ModelTransformExtension : IModelTransformExtension
    {
        /// <summary>
        /// Called after the .edmx document has been loaded, but before the contents are displayed in the
        /// Entity Designer.
        /// </summary>
        /// <param name="context"></param>
        void IModelTransformExtension.OnAfterModelLoaded(ModelTransformExtensionContext context)
        {
            // context.OriginalDocument = The incoming .edmx document.
            //                            This file cannot be modified.
            //
            // context.CurrentDocument = Make changes to this document using OriginalDocument as a reference.
            //                           This document will be loaded by the Entity Designer.

            XElement root = context.CurrentDocument.Root;
            string edmxNamespace = root.Name.NamespaceName;
            XElement runtime = root.Element(XName.Get("Runtime", edmxNamespace));
            XElement conceptualModels = runtime.Element(XName.Get("ConceptualModels", edmxNamespace));
            XElement schema = conceptualModels.Elements().FirstOrDefault();
            XNamespace schemaNamespace = schema.GetDefaultNamespace();
        }

        /// <summary>
        /// Called immediately before the .edmx document is saved.
        /// </summary>
        /// <param name="context"></param>
        void IModelTransformExtension.OnBeforeModelSaved(ModelTransformExtensionContext context)
        {
            // context.OriginalDocument = The .edmx document as created by the Entity Designer.
            //
            // context.CurrentDocument = Make changes to this document using OriginalDocument as a reference.
            //                           This should be the valid .edmx document to be saved.

            //context.CurrentDocument.Root.Elements(prop)
        }
    }
}