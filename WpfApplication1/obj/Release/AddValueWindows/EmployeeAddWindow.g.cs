﻿#pragma checksum "..\..\..\AddValueWindows\EmployeeAddWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "3E21A4495C449ABDEF8C84036343B3D0"
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
    /// EmployeeAddWindow
    /// </summary>
    public partial class EmployeeAddWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxName;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxTel;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxPos;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxContract;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSave;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonBack;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxEmployeeINN;
        
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
            System.Uri resourceLocater = new System.Uri("/Pharmacy DataBase;component/addvaluewindows/employeeaddwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
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
            this.textBoxName = ((System.Windows.Controls.TextBox)(target));
            
            #line 7 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
            this.textBoxName.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBox_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textBoxTel = ((System.Windows.Controls.TextBox)(target));
            
            #line 8 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
            this.textBoxTel.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBox_KeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.comboBoxPos = ((System.Windows.Controls.ComboBox)(target));
            
            #line 9 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
            this.comboBoxPos.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBoxPos_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.textBoxContract = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
            this.textBoxContract.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBox_KeyUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.buttonSave = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
            this.buttonSave.Click += new System.Windows.RoutedEventHandler(this.buttonSave_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.buttonBack = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
            this.buttonBack.Click += new System.Windows.RoutedEventHandler(this.buttonBack_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textBoxEmployeeINN = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\AddValueWindows\EmployeeAddWindow.xaml"
            this.textBoxEmployeeINN.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBox_KeyUp);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

