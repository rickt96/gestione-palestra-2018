﻿#pragma checksum "..\..\..\Windows\WindowEsercizio.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BA6B698ECAE9F980935A26C6F1D83307A6AEDD76"
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
    /// WindowEsercizio
    /// </summary>
    public partial class WindowEsercizio : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_salva;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_elimina;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid_img;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_carica;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_rimuovi;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_nome;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_descrizione;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmb_categoria;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Windows\WindowEsercizio.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider sld_difficolta;
        
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
            System.Uri resourceLocater = new System.Uri("/Gestione Palestra;component/windows/windowesercizio.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\WindowEsercizio.xaml"
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
            
            #line 8 "..\..\..\Windows\WindowEsercizio.xaml"
            ((GestionePalestra.WindowEsercizio)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btn_salva = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\Windows\WindowEsercizio.xaml"
            this.btn_salva.Click += new System.Windows.RoutedEventHandler(this.btn_salva_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_elimina = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Windows\WindowEsercizio.xaml"
            this.btn_elimina.Click += new System.Windows.RoutedEventHandler(this.btn_elimina_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.grid_img = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.btn_carica = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\..\Windows\WindowEsercizio.xaml"
            this.btn_carica.Click += new System.Windows.RoutedEventHandler(this.btn_carica_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_rimuovi = ((System.Windows.Controls.Button)(target));
            
            #line 17 "..\..\..\Windows\WindowEsercizio.xaml"
            this.btn_rimuovi.Click += new System.Windows.RoutedEventHandler(this.btn_rimuovi_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.txt_nome = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txt_descrizione = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.cmb_categoria = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.sld_difficolta = ((System.Windows.Controls.Slider)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

