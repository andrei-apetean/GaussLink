﻿#pragma checksum "..\..\..\..\..\..\Views\MainDisplay\Tabs\GraphTabView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C639AC686139F4B55608A42BD4E54BFB39E3E7E4111A009EA0A8DFC5FB264817"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GaussLink.ViewModels.MainDisplay.Tabs;
using GaussLink.Views.MainDisplay.Tabs;
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


namespace GaussLink.Views.MainDisplay.Tabs {
    
    
    /// <summary>
    /// GraphTabView
    /// </summary>
    public partial class GraphTabView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\..\..\Views\MainDisplay\Tabs\GraphTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid parent;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\..\..\Views\MainDisplay\Tabs\GraphTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas plotView;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\..\Views\MainDisplay\Tabs\GraphTabView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SmoothFactorTxt;
        
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
            System.Uri resourceLocater = new System.Uri("/GaussLink;component/views/maindisplay/tabs/graphtabview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Views\MainDisplay\Tabs\GraphTabView.xaml"
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
            this.parent = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.plotView = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.SmoothFactorTxt = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\..\..\..\Views\MainDisplay\Tabs\GraphTabView.xaml"
            this.SmoothFactorTxt.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SmoothFactorTxt_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

