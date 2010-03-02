namespace GeometryConverter.BLL.Templates
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NVelocity;

    public class TemplateManager
    {
        private FileTemplateProvider templateProvider;

        public virtual FileTemplateProvider TemplateProvider
        {
            get
            {
                if (templateProvider == null)
                {
                    templateProvider = new FileTemplateProvider();
                }
                return templateProvider;
            }
            set
            {
                if (value != null)
                {
                    templateProvider = value;
                }
            }
        }

        public string MergeTemplateWithObjects(string templatePath, string templateName, Dictionary<string, object> objects)
        {
            Template template = TemplateProvider.GetTemplate(templatePath, templateName);

            if (template != null)
            {
                VelocityContext context = new VelocityContext();

                foreach (KeyValuePair<string, object> obj in objects)
                {
                    context.Put(obj.Key, obj.Value);
                }

                StringWriter writer = new StringWriter();
                template.Merge(context, writer);
                return writer.GetStringBuilder().ToString();
            }

            throw new InvalidOperationException(String.Format("Template with name {0} and path {1} was not found", templateName, templatePath));
        }
    }
}