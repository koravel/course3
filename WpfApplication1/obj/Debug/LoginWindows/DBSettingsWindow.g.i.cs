﻿#pragma checksum "..\..\..\LoginWindows\DBSettingsWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D8D24F75F5400379236052D63A384E0B"
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
    /// DBSettingsWindow
    /// </summary>
    public partial class DBSettingsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_Adress;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textbox_DBName;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_Loign;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_Password;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDBSettingsSave;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDBSettingsBack;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDBSettingsConCheck;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDBSettingsDefault;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication1;component/loginwindows/dbsettingswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
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
            
            #line 4 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
            ((WpfApplication1.DBSettingsWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textBox_Adress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.textbox_DBName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textBox_Loign = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.textBox_Password = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.buttonDBSettingsSave = ((System.Windows.Controls.Button)(target));
            
            #line 14 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
            this.buttonDBSettingsSave.Click += new System.Windows.RoutedEventHandler(this.SaveChanges_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.buttonDBSettingsBack = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
            this.buttonDBSettingsBack.Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.buttonDBSettingsConCheck = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
            this.buttonDBSettingsConCheck.Click += new System.Windows.RoutedEventHandler(this.CheckCon_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.buttonDBSettingsDefault = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\LoginWindows\DBSettingsWindow.xaml"
            this.buttonDBSettingsDefault.Click += new System.Windows.RoutedEventHandler(this.ToDefault_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

