﻿#pragma checksum "..\..\..\Windows\PackFormChange.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F520F5249AC3597F767E8C849BAFD3651A3DE1D523F934B7383C44C5224ADE57"
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
    /// PackFormChange
    /// </summary>
    public partial class PackFormChange : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitTest;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelTest;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IDPack;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox TestsInPack;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TimeTest;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TimeMin;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox CountRestart;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox IPList;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NamePack;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Browser;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Windows\PackFormChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Stend;
        
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
            System.Uri resourceLocater = new System.Uri("/DashBoardClient;component/windows/packformchange.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\PackFormChange.xaml"
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
            this.SubmitTest = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\Windows\PackFormChange.xaml"
            this.SubmitTest.Click += new System.Windows.RoutedEventHandler(this.SendPack);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CancelTest = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.IDPack = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TestsInPack = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.TimeTest = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.TimeMin = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.CountRestart = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.IPList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.NamePack = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.Browser = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.Stend = ((System.Windows.Controls.ComboBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

