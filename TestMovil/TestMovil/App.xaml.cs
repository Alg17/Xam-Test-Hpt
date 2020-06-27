using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestMovil.Services;
using TestMovil.Views;

namespace TestMovil
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new ClienteViewPage();
        }

        protected override void OnStart()
        {
            ApiClient.Init();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
