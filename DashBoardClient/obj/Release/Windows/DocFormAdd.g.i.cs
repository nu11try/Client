﻿#pragma checksum "..\..\..\Windows\DocFormAdd.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "52BCBFAF6947715BFC61D90A422628CE27D7BFEAA65A43B5EE1503EC5FFA002B"
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
    /// DocFormAdd
    /// </summary>
    public partial class DocFormAdd : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Windows\DocFormAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PimLink;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Windows\DocFormAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DateBlock;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Windows\DocFormAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SubmitTest;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Windows\DocFormAdd.xaml"
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
            System.Uri resourceLocater = new System.Uri("/DashBoardClient;component/windows/docformadd.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\DocFormAdd.xaml"
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
            this.PimLink = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.DateBlock = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.SubmitTest = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\..\Windows\DocFormAdd.xaml"
            this.SubmitTest.Click += new System.Windows.RoutedEventHandler(this.SendDoc);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CancelTest = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\..\Windows\DocFormAdd.xaml"
            this.CancelTest.Click += new System.Windows.RoutedEventHandler(this.CloseWindow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
