namespace GeometryConverter.BLL.Templates
{
    using NVelocity;
    using NVelocity.App;

    public class TemplateProvider
    {
        #region Methods

        public Template GetTemplate(string templatePath, string templateName)
        {
            //if (!File.Exists(string.Concat(templatePath, templateName)))
            //    throw new InvalidOperationException(String.Format("Template with name {0} and path {1} was not found.", templateName, templatePath));

            var engine = new VelocityEngine();
            // var props = new ExtendedProperties();
            // props.AddProperty(NVelocity.Runtime.RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templatePath);
            // engine.Init(props);
            engine.Init();

            return engine.GetTemplate(templateName);
        }

        #endregion
    }
}