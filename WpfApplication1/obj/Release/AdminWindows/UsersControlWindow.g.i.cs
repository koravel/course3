﻿#pragma checksum "..\..\..\AdminWindows\UsersControlWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2848B65DE97A41E51A300DDAE554CF27"
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
    /// UsersControlWindow
    /// </summary>
    public partial class UsersControlWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\AdminWindows\UsersControlWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGridUserOut;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\AdminWindows\UsersControlWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemDel;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\AdminWindows\UsersControlWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonUserAdd;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\AdminWindows\UsersControlWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonUserDelete;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\AdminWindows\UsersControlWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonUpdate;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\AdminWindows\UsersControlWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonBack;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\AdminWindows\UsersControlWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonUserEdit;
        
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
            System.Uri resourceLocater = new System.Uri("/Pharmacy DataBase;component/adminwindows/userscontrolwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AdminWindows\UsersControlWindow.xaml"
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
            this.dataGridUserOut = ((System.Windows.Controls.DataGrid)(target));
            
            #line 10 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            this.dataGridUserOut.KeyUp += new System.Windows.Input.KeyEventHandler(this.dataGridUserOut_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 18 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.buttonUserEdit_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.menuItemDel = ((System.Windows.Controls.MenuItem)(target));
            
            #line 19 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            this.menuItemDel.Click += new System.Windows.RoutedEventHandler(this.UserDelete_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 20 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.UserAdd_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 21 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.buttonUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.buttonUserAdd = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            this.buttonUserAdd.Click += new System.Windows.RoutedEventHandler(this.UserAdd_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.buttonUserDelete = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            this.buttonUserDelete.Click += new System.Windows.RoutedEventHandler(this.UserDelete_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.buttonUpdate = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            this.buttonUpdate.Click += new System.Windows.RoutedEventHandler(this.buttonUpdate_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.buttonBack = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            this.buttonBack.Click += new System.Windows.RoutedEventHandler(this.buttonBack_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.buttonUserEdit = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\AdminWindows\UsersControlWindow.xaml"
            this.buttonUserEdit.Click += new System.Windows.RoutedEventHandler(this.buttonUserEdit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

