namespace GeometryConverter.Templates
{
    using System.Collections.Generic;
    using System.IO;
    using NVelocity;

    public class TemplateManager
    {
        private readonly Template template;

        private TemplateProvider templateProvider;

        public TemplateProvider TemplateProvider
        {
            get
            {
                if (templateProvider == null)
                {
                    templateProvider = new TemplateProvider();
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

        private TemplateManager()
        {
        }

        public TemplateManager(string templatePath, string templateName)
        {
            template = TemplateProvider.GetTemplate(templatePath, templateName);
        }

        public void MergeTemplateWithObjects(Dictionary<string, object> objects, string pathToFile)
        {
            var context = new VelocityContext();

            foreach (KeyValuePair<string, object> obj in objects)
            {
                context.Put(obj.Key, obj.Value);
            }

            var writer = new StreamWriter(pathToFile);
            template.Merge(context, writer);
            writer.Flush();
            writer.Close();
        }
    }
}