using autoservise.Controllers;
using autoservise.Models;
using autoservise.Models.Static;
using autoservise.Xaml.Autorization;
using autoservise.Xaml.UserPanel.ImageLoader;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise
{
    public partial class App : Application
    {
        CachPreferens cache = CachPreferens.GetInstance;
        DataModel datamodel = DataModel.GetInstance;
        UserModel userModel = UserModel.Instance();
        BreadScribe breadScribe = BreadScribe.GetInstance;

        public App()
        {
            InitializeComponent();

            DataLoad();
            /*
            if (!cache.HasKey("tutorial"))
                MainPage = new MainPage();
            else
                MainPage = new PreLog();
            */
            userModel.user.email = "nias.adamov@gmail.com";
            MainPage = new LoginPage();
            

        }

        public void DataLoad()
        {
            datamodel.GetData(DataLoad);
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
