﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources.Localization {
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
    public class Texts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Texts() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Nca.Valdr.Tests.DTOs.Texts", typeof(Texts).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ort.
        /// </summary>
        public static string Address_City {
            get {
                return ResourceManager.GetString("Address_City", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PLZ.
        /// </summary>
        public static string Address_ZipCode {
            get {
                return ResourceManager.GetString("Address_ZipCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} muss zwischen {1} und {2} Zeichen sein..
        /// </summary>
        public static string Generic_MaximumLength {
            get {
                return ResourceManager.GetString("Generic_MaximumLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} ist obligatorisch..
        /// </summary>
        public static string Generic_RequiredField {
            get {
                return ResourceManager.GetString("Generic_RequiredField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vorname.
        /// </summary>
        public static string Person_FirstName {
            get {
                return ResourceManager.GetString("Person_FirstName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nachname.
        /// </summary>
        public static string Person_LastName {
            get {
                return ResourceManager.GetString("Person_LastName", resourceCulture);
            }
        }
    }
}
