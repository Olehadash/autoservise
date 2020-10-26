using autoservise.Controllers;
using autoservise.Models;
using autoservise.Xaml.Autorization;
using autoservise.Xaml.Tutorial;
using autoservise.Xaml.Tutorial.Forms;
using FormsControls.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace autoservise
{
    public partial class MainPage : ContentPage, IAnimationPage
    {
        List<TutorialImmageForm> forms = new List<TutorialImmageForm>();
        TutorialModel model = new TutorialModel();

        AnimationController animation = AnimationController.GetInstance;
        CachPreferens cache = CachPreferens.GetInstance; 

        AbsoluteLayout content;
        Button button;
        StackLayout bline;

        int line = 0;
        public MainPage()
        {
            InitializeComponent();

            content = (AbsoluteLayout)FindByName("ImageContent");
            bline = (StackLayout)FindByName("ballLine");
            button = (Button)FindByName("PresButton");

            for (int i = 0; i < model.tutorials.Count; i++)
            {
                forms.Add(new TutorialImmageForm());
                forms[i].SetData(model.tutorials[i].picture, model.tutorials[i].title, model.tutorials[i].description);
                if (i > 0)
                    forms[i].SetIsVisible(false);

                content.Children.Add(forms[i]);
            }

            
        }

        public IPageAnimation PageAnimation => throw new NotImplementedException();

        public void OnAnimationFinished(bool isPopAnimation)
        {
            throw new NotImplementedException();
        }

        public void OnAnimationStarted(bool isPopAnimation)
        {
            throw new NotImplementedException();
        }
        public IPageAnimation PreLog { get; } = new FlipPageAnimation { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromTop };

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (line + 1 < forms.Count)
            {
                animation.SwypeAnimation(forms[line].getMainStack, forms[line + 1].getMainStack);
                animation.BallLineScaleSwitchAnimation(bline.Children[line], bline.Children[line + 1]);
                line++;
                if(line + 1 == forms.Count)
                {
                    button.BackgroundColor = Color.FromHex("#66C7F4");
                    button.TextColor = Color.White;
                    button.Text = "Начать";
                }
            }
            else
            {
                cache.SetKey("tutorial", true);
                App.Current.MainPage = new PreLog();
            }

        }
    }
}
