using autoservise.Controllers;
using autoservise.Models;
using autoservise.Models.Static;
using autoservise.Xaml.Autorization;
using autoservise.Xaml.UserPanel.ImageLoader;
using autoservise.Xaml.UserPanel.UserMainInterface;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise
{
    public partial class App : Application
    {
        CachPreferens cache = CachPreferens.GetInstance;
        DataModel data = DataModel.GetInstance;
        UserModel userModel = UserModel.Instance();
        BreadScribe breadScribe = BreadScribe.GetInstance;
        AuthorizationPageModel auth = AuthorizationPageModel.GetInstance;
        ServerController server = ServerController.GetInstance;


        public App()
        {
            InitializeComponent();

            userModel.LoadData();
            if(cache.HasKey("data"))
            {
                data.LoadData(cache.GetString("data"));
            }


            if (!cache.HasKey("tutorial"))
                MainPage = new MainPage();
            else if (!cache.HasKey("user_id"))
                MainPage = new PreLog();
            else if (cache.HasKey("user_password"))
            {
                Console.WriteLine("Logn");
                MainPage = new UserMainInterface();
            }
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
