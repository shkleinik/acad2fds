﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Acad2FdsSetupActions.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Acad2FdsSetupActions.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An instance of AutoCAD is running now. Please, terminate it and try again..
        /// </summary>
        internal static string AcadIsRunningMessage {
            get {
                return ResourceManager.GetString("AcadIsRunningMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have no supported AutoCAD version installed. The installation will be canceled..
        /// </summary>
        internal static string AcadNotInstalledMessage {
            get {
                return ResourceManager.GetString("AcadNotInstalledMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Plugin for AutoCAD for converting 3D geomentry to FDS (Fire Dynamics Simulation) format..
        /// </summary>
        internal static string FdsPluginDescription {
            get {
                return ResourceManager.GetString("FdsPluginDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Info.
        /// </summary>
        internal static string InfoCaption {
            get {
                return ResourceManager.GetString("InfoCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Warning.
        /// </summary>
        internal static string InstallPreventionWindowCaption {
            get {
                return ResourceManager.GetString("InstallPreventionWindowCaption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Setup has been cancelled by the user..
        /// </summary>
        internal static string UserSetupCancelation {
            get {
                return ResourceManager.GetString("UserSetupCancelation", resourceCulture);
            }
        }
    }
}
