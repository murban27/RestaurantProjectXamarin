﻿using App2.ViewModels;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using App2.ConvertClass;
using System.Globalization;
using System.Windows.Input;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.TabView;
using Java.Lang;
using Syncfusion.SfDataGrid.XForms;
using App2.Models;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Zkouska : ContentPage
    {

        public TableesViewModel viewModel;
        public Zkouska()
        {
            InitializeComponent();
            BindingContext = viewModel = new TableesViewModel();


        }

         async override protected void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Stolies.Count() < 1)
            {
                await viewModel.GetTask();
            }

        }

        private void SfDataGrid_GridDoubleTapped(object sender, Syncfusion.SfDataGrid.XForms.GridDoubleTappedEventArgs e)
        {
         var s= (Tables)SfGrid.SelectedItem;
            Navigation.PushAsync(new TableCollection(s));

        }
    }
}


