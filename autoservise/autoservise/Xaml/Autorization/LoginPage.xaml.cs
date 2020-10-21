using autoservise.Controllers;
using autoservise.Models;
using autoservise.Xaml.UserPanel;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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

    

    public partial class LoginPage : ContentPage
    {
        AuthorizationController controller = AuthorizationController.Instance();
        UserModel usermodel = UserModel.Instance();
        Entry email;
        Entry password;

        LoadingPopupPage loading = new LoadingPopupPage();
        public LoginPage()
        {
            
            InitializeComponent();
            email = (Entry)this.FindByName("mailtxt");
            password = (Entry)this.FindByName("passtxt");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new ForgetPassword();
        }
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(password.Text))
            {
                ErrorPane("Warning", "Заполните Все обязятельные поля *");
                return;
            }

            await PopupNavigation.PushAsync(loading);
            usermodel.user.email = email.Text;
            usermodel.user.password = password.Text;

            controller.login(Suckess, Error);
        }

        private async void Suckess ()
        {
            PopupNavigation.RemovePageAsync(loading);
            App.Current.MainPage = new CunstamerOrderCotegory();
        }

        private async void Error()
        {
            await PopupNavigation.RemovePageAsync(loading);
            ErrorPane("Error in connect!", "Error in connection. Try again!");


        }

        private async void ErrorPane(string Title, string message)
        {
            await DisplayAlert(Title, message, "OK");
        }
    }
}