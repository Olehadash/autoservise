using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.UserPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Budget : ContentPage
    {
        OrderModel order = OrderModel.GetInstance;

        Entry price;
        StackLayout dealperise;
        Image toggleImage;

        bool toggleStat = false;

        public Budget()
        {
            InitializeComponent();

            price = (Entry)this.FindByName("PriceText");
            dealperise = (StackLayout)this.FindByName("DialBut");
            toggleImage = (Image)this.FindByName("ToggleImage");

            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                toggleImage.Source = toggleStat ? "toggle.png" : "togglepick.png";
                toggleStat = !toggleStat;
                order.order.deal_price = toggleStat;
                if (toggleStat)
                    price.Text = "0";

            };

            dealperise.GestureRecognizers.Add(tgr);

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (!order.order.deal_price)
                order.order.price = Convert.ToInt32( price.Text);

            Back();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Back();
        }
        void Back()
        {
            App.Current.MainPage = new AddOrder();
        }
    }
}