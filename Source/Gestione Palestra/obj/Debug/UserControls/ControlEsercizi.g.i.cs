﻿#pragma checksum "..\..\..\UserControls\ControlEsercizi.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1DB2E459AD38CADD15B031B983596A82791C29C3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Il codice è stato generato da uno strumento.
//     Versione runtime:4.0.30319.42000
//
//     Le modifiche apportate a questo file possono provocare un comportamento non corretto e andranno perse se
//     il codice viene rigenerato.
// </auto-generated>
//------------------------------------------------------------------------------

using GestionePalestra;
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


namespace GestionePalestra {
    
    
    /// <summary>
    /// ControlEsercizi
    /// </summary>
    public partial class ControlEsercizi : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\..\UserControls\ControlEsercizi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal GestionePalestra.ControlEsercizi uc;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\UserControls\ControlEsercizi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_main;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\UserControls\ControlEsercizi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Rectangle rect;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\UserControls\ControlEsercizi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_header;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\UserControls\ControlEsercizi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_header_2;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\UserControls\ControlEsercizi.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_img;
        
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
            System.Uri resourceLocater = new System.Uri("/Gestione Palestra;component/usercontrols/controlesercizi.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\ControlEsercizi.xaml"
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
            this.uc = ((GestionePalestra.ControlEsercizi)(target));
            
            #line 7 "..\..\..\UserControls\ControlEsercizi.xaml"
            this.uc.MouseLeave += new System.Windows.Input.MouseEventHandler(this.uc_MouseLeave);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\UserControls\ControlEsercizi.xaml"
            this.uc.MouseEnter += new System.Windows.Input.MouseEventHandler(this.uc_MouseEnter);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\UserControls\ControlEsercizi.xaml"
            this.uc.LostFocus += new System.Windows.RoutedEventHandler(this.uc_LostFocus);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\UserControls\ControlEsercizi.xaml"
            this.uc.GotFocus += new System.Windows.RoutedEventHandler(this.uc_GotFocus);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grid_main = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.rect = ((System.Windows.Shapes.Rectangle)(target));
            return;
            case 4:
            this.lbl_header = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.lbl_header_2 = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.grid_img = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

