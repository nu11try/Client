﻿#pragma checksum "..\..\..\Windows\AutostartAddChange.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1F5E776A351F5350DA5129016A2CC12711808FEC93748327BBAECE6A21CFCCB1"
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
    /// AutostartAddChange
    /// </summary>
    public partial class AutostartAddChange : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameAut;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitAut;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CancelAut;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox weekDay;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox hourSelected;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox minuteSelected;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox packName;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Windows\AutostartAddChange.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkTranslateType;
        
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
            System.Uri resourceLocater = new System.Uri("/DashBoardClient;component/windows/autostartaddchange.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AutostartAddChange.xaml"
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
            this.NameAut = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.SubmitAut = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\Windows\AutostartAddChange.xaml"
            this.SubmitAut.Click += new System.Windows.RoutedEventHandler(this.SendDoc);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CancelAut = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\Windows\AutostartAddChange.xaml"
            this.CancelAut.Click += new System.Windows.RoutedEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            return;
            case 4:
            this.weekDay = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.hourSelected = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.minuteSelected = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.packName = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.checkTranslateType = ((System.Windows.Controls.CheckBox)(target));
            
            #line 50 "..\..\..\Windows\AutostartAddChange.xaml"
            this.checkTranslateType.Click += new System.Windows.RoutedEventHandler(this.ReplaceType);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

