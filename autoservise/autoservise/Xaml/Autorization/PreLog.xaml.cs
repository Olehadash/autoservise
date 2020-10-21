using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.Autorization
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreLog : ContentPage
    {
        CreateUserModel gum = CreateUserModel.Instance();
        public PreLog()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            gum._isConsumer = true;
            App.Current.MainPage = new CreateUser();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            gum._isConsumer = false;
            App.Current.MainPage = new CreateUser();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}