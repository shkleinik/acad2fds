using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Surface = MaterialManager.BLL.Surface;

namespace Fds2AcadPlugin.BLL
{
    public class FdsStartInfo
    {
        // public string Arguments { get; set; }

        public int CalculationTime { get; set; }

        public string DocumentName { get; set; }

        public string PathToFds { get; set; }

        public IList<Entity> SelectedSolids { get; set; }

        public IList<Surface> UsedSurfaces { get; set; }

        public string WorkingDirectory { get; set; }
    }
}