﻿#pragma checksum "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CA0C120D6F0987919427CAAA82322D4E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace WpfApplication1 {
    
    
    /// <summary>
    /// WarningDelProductBindsWindow
    /// </summary>
    public partial class WarningDelProductBindsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelWarning;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDelBinds;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonNotDelBinds;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxWarningSettings;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Pharmacy DataBase;component/warningwindows/warningdelproductbindswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.labelWarning = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.buttonDelBinds = ((System.Windows.Controls.Button)(target));
            
            #line 7 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
            this.buttonDelBinds.Click += new System.Windows.RoutedEventHandler(this.buttonDelBinds_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonNotDelBinds = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
            this.buttonNotDelBinds.Click += new System.Windows.RoutedEventHandler(this.buttonNotDelBinds_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.checkBoxWarningSettings = ((System.Windows.Controls.CheckBox)(target));
            
            #line 9 "..\..\..\WarningWindows\WarningDelProductBindsWindow.xaml"
            this.checkBoxWarningSettings.Click += new System.Windows.RoutedEventHandler(this.checkBoxWarningSettings_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

