﻿#pragma checksum "..\..\..\Windows\TestFormChange.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "084C2A13689BBD083D9658658931BE4BDBED6EFA8F15B0E004484A42C7DC5E36"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using DashBoardClient;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace DashBoardClient {
    
    
    /// <summary>
    /// TestFormChange
    /// </summary>
    public partial class TestFormChange : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\Windows\TestFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TestID;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Windows\TestFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox MethodSelect;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\TestFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ActiveSelect;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\TestFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitTest;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\TestFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelTest;
        
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
            System.Uri resourceLocater = new System.Uri("/DashBoardClient;component/windows/testformchange.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\TestFormChange.xaml"
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
            this.TestID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.MethodSelect = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.ActiveSelect = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 4:
            this.SubmitTest = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\Windows\TestFormChange.xaml"
            this.SubmitTest.Click += new System.Windows.RoutedEventHandler(this.SendTest);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CancelTest = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Windows\TestFormChange.xaml"
            this.CancelTest.Click += new System.Windows.RoutedEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
