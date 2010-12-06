using System.Reflection;
using System.Runtime.InteropServices;
using Autodesk.AutoCAD.Runtime;
using Fds2AcadPlugin;

[assembly: AssemblyTitle("PlugIn")]
[assembly: ComVisible(false)]
[assembly: Guid("b4b8a41f-ac62-4797-a6a4-234695a91533")]


[assembly: CommandClass(typeof(EntryPoint))]
[assembly: ExtensionApplication(typeof(EntryPoint))]
