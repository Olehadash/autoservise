using autoservise.Controllers;
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
    public partial class ConfirmMailSecond : ContentPage
    {
        UserModel usermodel = UserModel.Instance();

        ConfirmMailController controller = ConfirmMailController.Instance();

        Label timer;
        Grid secudes;
        Label mailView;
        Entry codetxt;

        int time = 30;
        bool isTimerStarted = false;

        public ConfirmMailSecond()
        {
            InitializeComponent();

            timer = (Label)this.FindByName("TemerText");
            secudes = (Grid)this.FindByName("buttonsLine");
            mailView = (Label)this.FindByName("MailVievText");
            codetxt = (Entry)this.FindByName("CodeText");

            mailView.Text = "Код для подтверждения отправлен на почту "+ usermodel.user.email;

            SetTimer();
        }

        void SetTimer()
        {
            secudes.IsVisible = false;

            if (isTimerStarted) return;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                isTimerStarted = true;
                timer.Text = "Получить новый код можно через" + time.ToString() + "сек";
                time--;
                if(time == 0)
                {
                    timer.IsVisible = false;
                    secudes.IsVisible = true;
                }
                isTimerStarted = false;
                return time > 0;
            });
        }

        

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(codetxt.Text== null)
            {

                return;
            }
            controller.SendCode(codetxt.Text);
        }

        void Success()
        {
            App.Current.MainPage = new TownPick();
        }
        void Error()
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            controller.GetCode();
        }

        void ErrorGetCode()
        {

        }
    }
}