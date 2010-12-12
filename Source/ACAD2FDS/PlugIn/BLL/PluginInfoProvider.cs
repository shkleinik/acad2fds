namespace Fds2AcadPlugin.BLL
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using Common;
    using Resources;

    public static class PluginInfoProvider
    {
        #region Fields

        private static readonly string productVersion;

        private static readonly string productName;

        private static readonly string productDescription;

        private static readonly string[] authors = { "Pavel Shkleinik", "Alaksandr Kanaukou" };

        #endregion

        #region Cunstruction and finalization

        static PluginInfoProvider()
        {
            ProductLogo = Resources.Logo;

            productVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            productName = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute))).Product;

            productDescription = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyDescriptionAttribute))).Description;
        }

        #endregion

        #region Properties

        public static Bitmap ProductLogo { get; set; }

        public static string ProductVersion
        {
            get
            {
                return productVersion;
            }
        }

        public static string ProductName
        {
            get
            {
                return productName;
            }
        }

        public static string ProductDescription
        {
            get
            {
                return productDescription;
            }
        }

        public static string PathToMaterialsStore
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.MaterialsBasePath);
            }
        }

        public static string PathToSurfacesStore
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.SurfacesBasePath);
            }
        }

        public static string PathToMappingsMaterials
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.MappingsMaterialsPath);
            }
        }

        public static string PathToPluginConfig
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.ConfigName);
            }
        }

        public static string PathToFdsTemplate
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.FdsTemplateName);
            }
        }

        public static string[] Authors
        {
            get
            {
                return authors;
            }
        }

        #endregion
    }
}