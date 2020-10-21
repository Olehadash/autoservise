using autoservise.Xaml.Autorization;
using autoservise.Xaml.UserPanel.ImageLoader;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();



            MainPage = new Galerry();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
