using autoservise.Controllers;
using autoservise.MainUI;
using autoservise.Models;
using autoservise.Models.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmMailForm : ContentView
    {
        UserModel usermodel = UserModel.Instance();
        AnimationController animation = AnimationController.GetInstance;
        AuthorizationPageModel pagemodel = AuthorizationPageModel.GetInstance;
        BreadScribe breadScribe = BreadScribe.GetInstance;
        ServerController server = ServerController.GetInstance;
        List<InputTextViewer> inputs = new List<InputTextViewer>();

        Label timer;
        Grid buttonline;
        Label mailView;
        StackLayout layout;
        InputTextViewer input;



        int time = 30;
        bool isTimerStarted = false;
        public ConfirmMailForm()
        {
            InitializeComponent();

            timer = (Label)this.FindByName("TemerText");
            buttonline = (Grid)this.FindByName("buttonsLine");
            mailView = (Label)this.FindByName("MailVievText");
            layout = (StackLayout)this.FindByName("Content");

            Build();
        }

        void Build()
        { 
            
            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.ConfirmMail);
            layout.Children.Add(input);
            mailView.Text = "Код для подтверждения отправлен на почту " + usermodel.user.email;
            mailView.HorizontalOptions = LayoutOptions.Center;
            timer.HorizontalOptions = LayoutOptions.Center;
            buttonline.IsVisible = false;
            this.Opacity = 0;
        }

        public async Task Show()
        {
            await animation.OpacityIn(this);
        }
        public async Task Hide()
        {
            for (int i = 0; i < layout.Children.Count; i++)
            {
                animation.OpacityOutWthMove(layout.Children[i], i);
                await Task.Delay(20);
            }
        }

        public void SetTimer()
        {
            if (isTimerStarted) return;
            time = 30;
            timer.Text = "Получить новый код можно через " + time.ToString() + " сек";
            buttonline.IsVisible = false;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                isTimerStarted = true;
                timer.Text = "Получить новый код можно через " + time.ToString() + " сек";
                time--;
                mailView.HorizontalOptions = LayoutOptions.Center;
                timer.HorizontalOptions = LayoutOptions.Center;
                if (time == 0)
                {
                    timer.IsVisible = false;
                    isTimerStarted = false;
                    buttonline.IsVisible = true;
                }
                
                return time > 0;
            });
            
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await pagemodel.SendCode();
            if (server.ServerResult)
            {
                switch (breadScribe.GetFirstPageType())
                {
                    case PageType.CreateCustomer:
                        await breadScribe.NextPage(PageType.CityPick);
                        break;
                    case PageType.CreateExecutor:
                        await breadScribe.NextPage(PageType.ExecutorInfo);
                        break;
                    case PageType.Authorization:
                        await breadScribe.NextPage(PageType.ResetPassword);
                        break;
                }
            }
            else
            {
                input.SetError("Неверный код");
            }
            

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            SetTimer();
            timer.IsVisible = true;
            await pagemodel.GetCodeAgain();
            // go To Main
            
        }
    }
}