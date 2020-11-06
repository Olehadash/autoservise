using autoservise.Controllers;
using autoservise.MainUI;
using autoservise.Models;
using autoservise.Models.Static;
using autoservise.Xaml.Forms;
using autoservise.Xaml.UserPanel;
using autoservise.Xaml.UserPanel.UserMainInterface;
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
        Label forgetPassword;

        PageType pageType;

        List<View> elemnts = new List<View>();
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
            breadscribe.RegistratePage(PageType.ForgetPassword, ForgetPasswordShow, ForgetPasswordHide);
            breadscribe.RegistratePage(PageType.ResetPassword, ResetPAsswordShow, Hide);
            breadscribe.RegistratePage(PageType.ExecutorInfo, ExecutrorInfoShow, Hide);

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

        /*
         * Build - Athorization method
         * */

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
            title.IsVisible = true;
            title.Text = "Авторизация";
            title.Margin = new Thickness(10,50,10,10);

            await AthorizationAnimate();

        }
        /*
         * Login authorize method 
            This method show Animation of Autorization
         */
        async Task AthorizationAnimate()
        {
            if (button != null)
                button.Opacity = 0;
            await Show();

            if (button != null)
                await animation.OpacityInWthMove(button, elemnts.Count);
            
        }
        /*--------------------------------------------------------------------------------------------------------------------------------*/

        /*
         * Animation Methods
         * Show - Hide
         */
        async Task Show()
        {
            if(title.IsVisible)
                animation.OpacityIn(title);
            animation.OpacityIn(enterButton);
            
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
            for (int i = 0; i < mainLayout.Children.Count; i++)
                mainLayout.Children[i].Opacity = 0;
            await Task.Delay(100);
            for (int i = 0; i < mainLayout.Children.Count; i++)
                await animation.OpacityOutWthMove(mainLayout.Children[i], i);
            if(button!=null)
                await animation.OpacityInWthMove(button, mainLayout.Children.Count);

            mainLayout.Children.Clear();
        }
        /*--------------------------------------------------------------------------------------------------------------------------------*/

        

        /*
         * Create Customer 
         * Show - Hide Function
         */
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
        /*
         * Create Executor 
         * Show - Hide Method
         */
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

        /*-----------------------------------------------------------------------------------------------------------------------------------------------*/
        /*
         * City List 
         * Show - Hide Method
         */
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
        /*-------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*
         * Conrim Mail 
         * Show- Hide Method
         * */
        async Task ConfirmMailShow()
        {
            pageType = PageType.ConfirmMail;
            backButton.IsVisible = true;
            title.IsVisible = false;
            enterButton.IsVisible = false;
            ConfirmMailForm confirmMail = new ConfirmMailForm();
            mainLayout.Opacity = 1;
            mainLayout.Children.Add(confirmMail);
            await confirmMail.Show();
            confirmMail.SetTimer();
            await pagemodel.GetCode();

        }

        async Task ConfirmMailHide()
        {
            ConfirmMailForm confirmMail = mainLayout.Children[0] as ConfirmMailForm;
            
            await confirmMail.Hide();
            await Hide();
            
        }

        /*--------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*
         * Forget Password
         * Show - hide
         * */

        async Task ForgetPasswordShow()
        {
            pageType = PageType.ForgetPassword;
            backButton.IsVisible = true;
            title.Text = "Восстановление пароля";
            title.Margin = new Thickness(10, 0, 10, 10);

            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.Email);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            forgetPassword = new Label
            {
                Text = "На него будет отправлено письмо с кодом",
                FontSize = 12,
                Margin = 10
            };
            mainLayout.Children.Add(forgetPassword);

            enterButton.Text = "Далее";

            title.Opacity = 0;
            mainLayout.Opacity = 0;
            enterButton.Opacity = 0;

            await animation.OpacityIn(title,100);
            await animation.OpacityIn(mainLayout, 100);
            await animation.OpacityIn(enterButton, 100);

        }
        async Task ForgetPasswordHide()
        {
            await animation.OpacityOut(title);
            await animation.OpacityOut(mainLayout);
            await animation.OpacityOut(enterButton);

            mainLayout.Children.Clear();

        }
        /*------------------------------------------------------------------------------------------------------------------------------------------------*/
        /*
         * Reset Password
         * Show - Hide
         * */

        async Task ResetPAsswordShow()
        { 
            pageType = PageType.ResetPassword;
            backButton.IsVisible = true;
            title.IsVisible = false;

            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.Password);
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            input = new InputTextViewer();
            input.SetData(InputTextViewerType.Password);
            input.SetTitle("Повторите пароль *");
            elemnts.Add(input);
            mainLayout.Children.Add(input);

            enterButton.IsVisible = true;
            enterButton.Opacity = 1;
            enterButton.Text = "Готово";
            await Show();
            await animation.OpacityIn(enterButton);
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------*/

        /*
         * ExecutrorInfo
         * show - Hide
         * */

        async Task ExecutrorInfoShow()
        {
            pageType = PageType.ExecutorInfo;
            backButton.IsVisible = true;
            title.IsVisible = false;
            CustomElementUI ui = new CustomElementUI();
            ui.SetData(CustomUIViewerType.City);
            mainLayout.Children.Add(ui);

            ui = new CustomElementUI();
            ui.SetData(CustomUIViewerType.Category);
            mainLayout.Children.Add(ui);

            ui = new CustomElementUI();
            ui.SetData(CustomUIViewerType.GalleryOpen);
            mainLayout.Children.Add(ui);

            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.Desctiption);
            input.SetNoBorderError("* Обезательное поле для заполнения");
            mainLayout.Children.Add(input);

            enterButton.Opacity = 1;
            enterButton.Text = "Сохранить";
            enterButton.IsVisible = true;

            await Show();
        }



        /*
         * Error Control Methods
         */

        private void Error()
        {
            breadscribe.Cancel();
            ErrorPane("Error in connect!", "Error in connection. Try again!");


        }

        private async void ErrorPane(string Title, string message)
        {
            await DisplayAlert(Title, message, "OK");
        }

        /*
         * ClickButtonHandlers
         * Button_clikc ForSwitchi Between Foms
         * */
        private void Button_Clicked()
        {
            elemnts.Clear();
            breadscribe.NextPage(PageType.ForgetPassword);
        }
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            bool result = false;
            for (int i = 0; i < elemnts.Count; i++)
            {
                result = (elemnts[i] as InputTextViewer).CheckLocalRools();
            }
            if (result) return;

            switch (pageType)
            {
                case PageType.Authorization:
                    
                    usermodel.user.email = (elemnts[0] as InputTextViewer).GetInfo();
                    usermodel.user.password = (elemnts[1] as InputTextViewer).GetInfo();
                    elemnts.Clear();
                    await pagemodel.login();
                    if (server.ServerResult)
                        App.Current.MainPage = new UserMainInterface();
                    else
                        Error();
                    break;
                case PageType.CreateCustomer:
                    await pagemodel.CreateUser();
                    if (server.ServerResult)
                    {
                        elemnts.Clear();
                        await breadscribe.NextPage(PageType.ConfirmMail);
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
                        await breadscribe.NextPage(PageType.ConfirmMail);
                    }
                    else
                    {
                        Error();
                    }
                    break;
                case PageType.CityPick:
                    App.Current.MainPage = new UserMainInterface();
                    break;
                case PageType.ForgetPassword:
                    await breadscribe.NextPage(PageType.ConfirmMail);
                    break;
                    
                case PageType.ResetPassword:
                    await pagemodel.ResetPassword();
                    if(server.ServerResult)
                    {
                        await breadscribe.GoToPage(0);
                    }
                    else
                    {
                        Error();
                    }
                    break;
                case PageType.ExecutorInfo:
                    App.Current.MainPage = new UserMainInterface();
                    break;
            }
        }
    }
}