﻿#pragma checksum "..\..\..\Windows\Auth.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7CD07993DD44842DD49EB2863AB9394EAFEBF9D575B53F2300BB7C9FD68D6C6B"
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
    /// Auth
    /// </summary>
    public partial class Auth : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\..\Windows\Auth.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox loginAuth;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Windows\Auth.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passAuth;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Windows\Auth.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAuth;
        
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
            System.Uri resourceLocater = new System.Uri("/DashBoardClient;component/windows/auth.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\Auth.xaml"
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
            this.loginAuth = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\..\Windows\Auth.xaml"
            this.loginAuth.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SelectEditAuth);
            
            #line default
            #line hidden
            return;
            case 2:
            this.passAuth = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 40 "..\..\..\Windows\Auth.xaml"
            this.passAuth.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SelectEditAuth);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnAuth = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Windows\Auth.xaml"
            this.btnAuth.Click += new System.Windows.RoutedEventHandler(this.EnterAuth);
            
            #line default
            #line hidden
            
            #line 45 "..\..\..\Windows\Auth.xaml"
            this.btnAuth.MouseEnter += new System.Windows.Input.MouseEventHandler(this.HoverBtn);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

