﻿#pragma checksum "..\..\..\EditValueWindows\ProductEditWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7D660E8F9515D46599FE7033C126DCE4"
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace WpfApplication1 {
    
    
    /// <summary>
    /// ProductEditWindow
    /// </summary>
    public partial class ProductEditWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxName;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxManufacturer;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxGroup;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxPack;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxMaterial;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxForm;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxInstruction;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSave;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonBack;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePickerToday;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DecimalUpDown upDownPrice;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTimeToAdd;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication1;component/editvaluewindows/producteditwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
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
            
            #line 7 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            ((WpfApplication1.ProductEditWindow)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Window_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.textBoxName = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.textBoxName.KeyUp += new System.Windows.Input.KeyEventHandler(this.textBoxName_KeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.comboBoxManufacturer = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.comboBoxManufacturer.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 17 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.comboBoxGroup = ((System.Windows.Controls.ComboBox)(target));
            
            #line 21 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.comboBoxGroup.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.comboBoxPack = ((System.Windows.Controls.ComboBox)(target));
            
            #line 69 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.comboBoxPack.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.comboBoxMaterial = ((System.Windows.Controls.ComboBox)(target));
            
            #line 81 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.comboBoxMaterial.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.comboBoxForm = ((System.Windows.Controls.ComboBox)(target));
            
            #line 96 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.comboBoxForm.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.comboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.textBoxInstruction = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.buttonSave = ((System.Windows.Controls.Button)(target));
            
            #line 108 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.buttonSave.Click += new System.Windows.RoutedEventHandler(this.buttonSave_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.buttonBack = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.buttonBack.Click += new System.Windows.RoutedEventHandler(this.buttonBack_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.datePickerToday = ((System.Windows.Controls.DatePicker)(target));
            
            #line 110 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.datePickerToday.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.datePickerToday_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 13:
            this.upDownPrice = ((Xceed.Wpf.Toolkit.DecimalUpDown)(target));
            
            #line 114 "..\..\..\EditValueWindows\ProductEditWindow.xaml"
            this.upDownPrice.KeyUp += new System.Windows.Input.KeyEventHandler(this.upDownPrice_KeyUp);
            
            #line default
            #line hidden
            return;
            case 14:
            this.labelTimeToAdd = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

