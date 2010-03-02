namespace GeometryConverter.BLL.Templates
{
    using System;
    using NVelocity;
    using NVelocity.App;
    using Commons.Collections;

    public class FileTemplateProvider
    {
        #region Constants

        private const string TemplateExtesion = "template";

        #endregion

        #region Methods

        public Template GetTemplate(string templatePath, string templateName)
        {
            try
            {
                var engine = new VelocityEngine();
                var props = new ExtendedProperties();
                props.AddProperty(NVelocity.Runtime.RuntimeConstants.FILE_RESOURCE_LOADER_PATH, templatePath);
                engine.Init(props);

                return engine.GetTemplate(String.Format("{0}.{1}", templateName, TemplateExtesion));
            }
            catch(NullReferenceException ex)
            {
                throw new InvalidOperationException(String.Format("Template with name {0} and path {1} was not found.", templateName, templatePath));
            }
        }

        #endregion
    }
}