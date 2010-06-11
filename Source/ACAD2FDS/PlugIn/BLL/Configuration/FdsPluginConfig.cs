using Fds2AcadPlugin.BLL.Entities;
using System.Collections.Generic;
namespace Fds2AcadPlugin.BLL.Configuration
{
    /// <summary>
    /// Introduces basic plugin confuguration.
    /// </summary>
    public class FdsPluginConfig
    {
        #region Properties

        /// <summary>
        /// Defines path to FDS executable.
        /// </summary>
        public string PathToFds { get; set; }

        /// <summary>
        /// Defines path to SmokeView executable.
        /// </summary>
        public string PathToSmokeView { get; set; }

        /// <summary>
        /// Defines if it is necessary to use custom element size for complex solids.
        /// </summary>
        public bool UseCustomElementSize { get; set; }

        /// <summary>
        /// Defines size of element. So, we have only one dimension and element will be a cube.
        /// </summary>
        public int ElementSize { get; set; }

        /// <summary>
        /// Specifies if it necessaty to define custom devices density.
        /// </summary>
        public bool DefineCustomDevicesDensity { get; set; }

        /// <summary>
        /// Density for devices.
        /// </summary>
        public int DevicesDensity { get; set; }

        /// <summary>
        /// Collection of devices, SLCFs and BNDFs.
        /// </summary>
        public List<InfoCollector> InfoCollectors { get; set; }

        #endregion
    }
}