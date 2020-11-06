using autoservise.Controllers;
using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.MainUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CutomButtonUI : ContentView
    {
        UIRegistrator registrator = UIRegistrator.GetInstance;
        BreadScribe breadScribe = BreadScribe.GetInstance;

        CustomUIViewerType type;

        StackLayout main;
        Image icon;
        Label title;
        Label subtitle;

        public CutomButtonUI()
        {
            InitializeComponent();

            main = (StackLayout)this.FindByName("MainLayout");
            icon = (Image)this.FindByName("Icon");
            title = (Label)this.FindByName("Title");
            subtitle = (Label)this.FindByName("Subtitle");
        }

        public void SetData(CustomUIViewerType ctype)
        {
            type = ctype;
            CustomUIViewModel model = registrator.UIView[ctype];
            icon.Source = model.source;
            title.Text = model.title;
            subtitle.Text = model.placeholder;

            TapGestureRecognizer tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                SetAction();
            };

            main.GestureRecognizers.Add(tgr);
        }

        async Task SetAction()
        { 
            switch(type)
            {
                case CustomUIViewerType.SetAdress:
                    await breadScribe.NextPage(PageType.AddAdressOrder);
                    break;
                case CustomUIViewerType.SetDate:
                    await breadScribe.NextPage(PageType.AddDateOrder);
                    break;

                case CustomUIViewerType.SetBudget:
                    await breadScribe.NextPage(PageType.AddBudgetOrder);
                    break;

                case CustomUIViewerType.GallaryOpenSecond:

                    break;
            }
        }
    }
}