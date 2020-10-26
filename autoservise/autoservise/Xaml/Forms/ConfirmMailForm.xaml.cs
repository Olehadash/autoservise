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

        Label timer;
        Grid buttonline;
        Label mailView;
        StackLayout layout;
        StackLayout rootlayout;



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
            buttonline.IsVisible = false;
            InputTextViewer input = new InputTextViewer();
            input.SetData(InputTextViewerType.ConfirmMail);
            layout.Children.Add(input);
            mailView.Text = "Код для подтверждения отправлен на почту " + usermodel.user.email;
            /*for(int i = 0; i<rootlayout.Children.Count;i++)
            {
                rootlayout.Children[i].Opacity = 0;
            }*/
        }

        public async Task Show()
        {
            for (int i = 0; i < rootlayout.Children.Count; i++)
            {
                animation.OpacityInWthMove(rootlayout.Children[i], i);
                await Task.Delay(20);
            }
        }
        public async Task Hide()
        {
            for (int i = 0; i < rootlayout.Children.Count; i++)
            {
                animation.OpacityOutWthMove(rootlayout.Children[i], i);
                await Task.Delay(20);
            }
        }

        void SetTimer()
        {
            if (isTimerStarted) return;
            time = 30;
            buttonline.IsVisible = false;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                isTimerStarted = true;
                timer.Text = "Получить новый код можно через " + time.ToString() + " сек";
                time--;
                if (time == 0)
                {
                    timer.IsVisible = false;
                    buttonline.IsVisible = true;
                }
                isTimerStarted = false;
                buttonline.IsVisible = true;
                return time > 0;
            });
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await pagemodel.GetCode();
            SetTimer();

        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            // go To Main
            await pagemodel.SendCode();
            if(server.ServerResult)
            {

            }else
            {
                
            }
        }
    }
}