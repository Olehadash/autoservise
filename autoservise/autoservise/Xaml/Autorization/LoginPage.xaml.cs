using autoservise.Controllers;
using autoservise.MainUI;
using autoservise.Models;
using autoservise.Models.Static;
using autoservise.Xaml.Forms;
using autoservise.Xaml.UserPanel;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.Autorization
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    

    public partial class LoginPage : ContentPage
    {
        AuthorizationController controller = AuthorizationController.Instance();
        AuthorizationPageModel pagemodel = AuthorizationPageModel.GetInstance;
        UserModel usermodel = UserModel.Instance();
        AnimationController animation = AnimationController.GetInstance;
        BreadScribe breadscribe = BreadScribe.GetInstance;
        ServerController server = ServerController.GetInstance;

        StackLayout mainLayout;
        StackLayout backButton;
        Label title;
        Button enterButton;
        SimpleButton button;

        PageType pageType;

        List<InputTextViewer> elemnts = new List<InputTextViewer>();
        public LoginPage()
        {
            
            InitializeComponent();

            mainLayout = (StackLayout)FindByName("Content");
            title = (Label)FindByName("Title");
            backButton = (StackLayout)FindByName("BackButton");
            enterButton = (Button)FindByName("BottomButton");

            pageType = usermodel.getPrelog();


            breadscribe.SetFirstPage(pageType);
            breadscribe.RegistratePage(PageType.Authorization, AuthorizationView, Hide);
            breadscribe.RegistratePage(PageType.CreateCustomer, CreateCustomer, Hide);
            breadscribe.RegistratePage(PageType.CreateExecutor, CreateExecutor, Hide);
            breadscribe.RegistratePage(PageType.CityPick, GotoCityList, CityListHide);
            breadscribe.RegistratePage(PageType.ConfirmMail, ConfirmMailShow, ConfirmMailHide);
            switch (pageType)
            {
                case PageType.None:

                    ConfirmMailShow();
                    break;
                case PageType.Authorization:
                    AuthorizationView();
                    break;
                case PageType.CreateCustomer:
                    CreateCustomer();
                    break;
                case PageType.CreateExecutor:
                    CreateExecutor();
                    break;
            }

        }

        async Task AuthorizationView()
        {
            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.Email);
            elemnts.Add(input);
            mainLayout.Children.Add(input);
            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Password);
            elemnts.Add(input);
            mainLayout.Children.Add(input);
            button = new SimpleButton();
            button.SetAction(Button_Clicked);
            mainLayout.Children.Add(button);
            backButton.IsVisible = false;
            title.Text = "Авторизация";
            title.Margin = new Thickness(10,50,10,10);

            await AthorizationAnimate();

        }
        async Task AthorizationAnimate()
        {
            if (button != null)
                button.Opacity = 0;
            await Show();

            if (button != null)
                await animation.OpacityInWthMove(button, elemnts.Count);
            
        }

        /*Animatio Methods*/
        async Task Show()
        {
            animation.OpacityIn(title);
            animation.OpacityIn(enterButton);
            ;
            for (int i = 0; i < elemnts.Count; i++)
                elemnts[i].Opacity = 0;
            await Task.Delay(100);
            for (int i = 0; i < elemnts.Count; i++)
            {
                animation.OpacityInWthMove(elemnts[i], i);
                await Task.Delay(50);
            }
        }

        async Task Hide()
        {
            animation.OpacityOut(title);
            animation.OpacityOut(enterButton);
            for (int i = 0; i < elemnts.Count; i++)
                elemnts[i].Opacity = 0;
            await Task.Delay(100);
            for (int i = 0; i < elemnts.Count; i++)
                await animation.OpacityOutWthMove(elemnts[i], i);
            if(button!=null)
                await animation.OpacityInWthMove(button, elemnts.Count);
        }
        /**/
        async Task CreateCustomer()
        {
            backButton.IsVisible = false;
            title.Text = "Введите свои данные";
            title.Margin = new Thickness(10, 50, 10, 10);

            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.UserName);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Phone);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Email);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Password);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Password);
            input.SetTitle("Повторите пароль *");
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            enterButton.Text = "Зарегистрироваться";

            await Show();
        }
        async Task CreateExecutor()
        {
            backButton.IsVisible = false;
            title.Text = "Введите свои данные";
            title.Margin = new Thickness(10, 50, 10, 10);

            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.OrganizationName);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Phone);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Email);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Password);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Password);
            input.SetTitle("Повторите пароль *");
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            enterButton.Text = "Зарегистрироваться";

            await Show();
        }

        private void Button_Clicked()
        {
            App.Current.MainPage = new ForgetPassword();
        }
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            bool result = false;
            for(int i = 0; i<elemnts.Count; i++)
            {
                result = elemnts[i].CheckLocalRools();
            }
            if (result) return;

            switch (pageType)
            {
                case PageType.Authorization:
                    elemnts.Clear();
                    usermodel.user.email = elemnts[0].GetInfo();
                    usermodel.user.password = elemnts[1].GetInfo();
                    pagemodel.login();
                    break;
                case PageType.CreateCustomer:
                    await pagemodel.CreateUser();
                    if (server.ServerResult)
                    {
                        elemnts.Clear();
                        await breadscribe.NextPage(PageType.CityPick);
                    }
                    else
                    {
                        Error();
                    }
                    break;
                case PageType.CreateExecutor:
                    await pagemodel.CreateUser();
                    if (server.ServerResult)
                    {
                        elemnts.Clear();
                        await breadscribe.NextPage(PageType.CityPick);
                    }
                    else
                    {
                        Error();
                    }
                    break;
                case PageType.CityPick:
                    await breadscribe.NextPage(PageType.ConfirmMail);
                    break;
            }
        }

        async Task GotoCityList()
        {
            
            pageType = PageType.CityPick;
            mainLayout.Children.Clear();
            backButton.IsVisible = true;
            title.Text = "Укажите ваш город";
            title.Margin = new Thickness(10, 0, 10, 10);
            CityPickForm citypick = new CityPickForm();
            mainLayout.Children.Add(citypick);
            enterButton.Text = "Далее";

            
            await Show();
            await citypick.Build();
        }
        
        async Task CityListHide()
        {
            CityPickForm citypick = mainLayout.Children[0] as CityPickForm;
            
            await Hide();
        }

        async Task ConfirmMailShow()
        {
            pageType = PageType.ConfirmMail;
            backButton.IsVisible = true;
            title.IsVisible = false;
            enterButton.IsVisible = false;
            ConfirmMailForm confirmMail = new ConfirmMailForm();
            mainLayout.Children.Add(confirmMail);
            await confirmMail.Show();


        }

        async Task ConfirmMailHide()
        {
            ConfirmMailForm confirmMail = mainLayout.Children[0] as ConfirmMailForm;
            
            await confirmMail.Hide();
            await Hide();
            
        }


        private void Error()
        {
            breadscribe.Cancel();
            ErrorPane("Error in connect!", "Error in connection. Try again!");


        }

        private async void ErrorPane(string Title, string message)
        {
            await DisplayAlert(Title, message, "OK");
        }
    }
}