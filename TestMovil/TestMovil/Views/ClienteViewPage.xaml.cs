using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMovil.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestMovil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClienteViewPage : ContentPage
    {
        public ClienteViewPage()
        {
            InitializeComponent();
            BindingContext = new ClienteViewModel();
        }
    }
}