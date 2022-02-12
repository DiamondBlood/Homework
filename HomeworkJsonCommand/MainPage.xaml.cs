using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.ObjectModel;

using Xamarin.Forms.Xaml;

namespace HomeworkJsonCommand
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        
        private ViewModel _vm = new ViewModel();
        public MainPage()
        {
            
            InitializeComponent();
            BindingContext = _vm;

        }

        protected override async void OnAppearing()
        {
            
            base.OnAppearing();
            await _vm.Changevalue();
        }
    }
}

