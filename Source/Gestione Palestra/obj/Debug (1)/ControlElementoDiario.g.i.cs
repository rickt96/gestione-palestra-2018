﻿#pragma checksum "..\..\ControlElementoDiario.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "EE38D3F34D72907B745AAF334E10D5305A558FAB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GestioneClienti;
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


namespace GestioneClienti {
    
    
    /// <summary>
    /// ControlElementoDiario
    /// </summary>
    public partial class ControlElementoDiario : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\ControlElementoDiario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_salva_annotazione;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\ControlElementoDiario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_titolo;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ControlElementoDiario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_modifiche_sospese;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ControlElementoDiario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_testo;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\ControlElementoDiario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox ckb_svolto;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ControlElementoDiario.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbl_data;
        
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
            System.Uri resourceLocater = new System.Uri("/Gestione Palestra;component/controlelementodiario.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ControlElementoDiario.xaml"
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
            this.btn_salva_annotazione = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\ControlElementoDiario.xaml"
            this.btn_salva_annotazione.Click += new System.Windows.RoutedEventHandler(this.btn_salva_annotazione_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txt_titolo = ((System.Windows.Controls.TextBox)(target));
            
            #line 15 "..\..\ControlElementoDiario.xaml"
            this.txt_titolo.LostFocus += new System.Windows.RoutedEventHandler(this.control_lost_focus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lbl_modifiche_sospese = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.txt_testo = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\ControlElementoDiario.xaml"
            this.txt_testo.LostFocus += new System.Windows.RoutedEventHandler(this.control_lost_focus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ckb_svolto = ((System.Windows.Controls.CheckBox)(target));
            
            #line 23 "..\..\ControlElementoDiario.xaml"
            this.ckb_svolto.LostFocus += new System.Windows.RoutedEventHandler(this.control_lost_focus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lbl_data = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

