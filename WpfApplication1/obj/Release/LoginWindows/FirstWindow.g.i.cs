﻿#pragma checksum "..\..\..\LoginWindows\FirstWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1D50AFA508256A199188EE11A924A5AC"
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
    /// FirstWindow
    /// </summary>
    public partial class FirstWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\LoginWindows\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxDirector;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\LoginWindows\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxSecurity;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\LoginWindows\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonConfirm;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\LoginWindows\FirstWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxDirectorLogin;
        
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
            System.Uri resourceLocater = new System.Uri("/Pharmacy DataBase;component/loginwindows/firstwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\LoginWindows\FirstWindow.xaml"
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
            
            #line 4 "..\..\..\LoginWindows\FirstWindow.xaml"
            ((WpfApplication1.FirstWindow)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.Window_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textBoxDirector = ((System.Windows.Controls.TextBox)(target));
            
            #line 6 "..\..\..\LoginWindows\FirstWindow.xaml"
            this.textBoxDirector.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBox_KeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.textBoxSecurity = ((System.Windows.Controls.TextBox)(target));
            
            #line 9 "..\..\..\LoginWindows\FirstWindow.xaml"
            this.textBoxSecurity.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBox_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonConfirm = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\LoginWindows\FirstWindow.xaml"
            this.buttonConfirm.Click += new System.Windows.RoutedEventHandler(this.buttonConfirm_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.textBoxDirectorLogin = ((System.Windows.Controls.TextBox)(target));
            
            #line 11 "..\..\..\LoginWindows\FirstWindow.xaml"
            this.textBoxDirectorLogin.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBox_KeyUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

