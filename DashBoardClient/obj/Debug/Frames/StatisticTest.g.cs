﻿#pragma checksum "..\..\..\Frames\StatisticTest.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "429E36C381146159EE2798905A71426888D94BBBD0AF2AACB360D24C9F019737"
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
    /// StatisticTest
    /// </summary>
    public partial class StatisticTest : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 38 "..\..\..\Frames\StatisticTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView TestsView;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\Frames\StatisticTest.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView TestsInfo;
        
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
            System.Uri resourceLocater = new System.Uri("/DashBoardClient;component/frames/statistictest.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Frames\StatisticTest.xaml"
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
            this.TestsView = ((System.Windows.Controls.ListView)(target));
            
            #line 38 "..\..\..\Frames\StatisticTest.xaml"
            this.TestsView.AddHandler(System.Windows.Controls.ScrollViewer.ScrollChangedEvent, new System.Windows.Controls.ScrollChangedEventHandler(this.TestsView_ScrollChanged));
            
            #line default
            #line hidden
            return;
            case 2:
            this.TestsInfo = ((System.Windows.Controls.ListView)(target));
            
            #line 117 "..\..\..\Frames\StatisticTest.xaml"
            this.TestsInfo.AddHandler(System.Windows.Controls.ScrollViewer.ScrollChangedEvent, new System.Windows.Controls.ScrollChangedEventHandler(this.TestsInfo_ScrollChanged));
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

