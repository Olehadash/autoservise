using autoservise.Controllers;
using autoservise.Models.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

public delegate void UiButtonDelegate(PageType name);

namespace autoservise.MainUI
{
    class arrowbut
    {
        public string source { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }

        public arrowbut(string source, string title, string subtitle)
        {
            this.source = source;
            this.title = title;
            this.subtitle = subtitle;
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArrowButton : ContentView
    {
        StackLayout layuot;
        Image image;
        Label title;
        Label subtitle;

        PageType idData;

        public ArrowButton()
        {
            InitializeComponent();

            layuot = (StackLayout)FindByName("MainLayout");
            image = (Image)FindByName("imageIcon");
            title = (Label)FindByName("Title");
            subtitle = (Label)FindByName("Subtitle");

        }

        public void SetData(String source, string title, string subtitle, PageType idData)
        {
            this.idData = idData;
            image.Source = source;
            this.title.Text = title;
            if (String.IsNullOrEmpty(subtitle))
            {
                this.subtitle.IsVisible = false;
                this.title.VerticalOptions = LayoutOptions.Center;
            }
            else
            {
                this.subtitle.Text = subtitle;
            }
        }

        public void SetButtonDelegate(UiButtonDelegate action)
        {
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                if (action != null) action(this.idData);
            };
            layuot.GestureRecognizers.Add(tgr);
        }


    }
}