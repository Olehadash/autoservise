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
    public partial class ToggleUI : ContentView
    {
        OrderModel order = OrderModel.GetInstance;

        StackLayout main;
        Image icon;
        Label label;

        bool isToggled = false;

        string toggled = "togglepick.png";
        string untoggled = "toggle.png";

        public ToggleUI()
        {
            InitializeComponent();

            main = (StackLayout)this.FindByName("MainLayout");
            icon = (Image)this.FindByName("ToggleImage");
            label = (Label)this.FindByName("Title");

            TapGestureRecognizer tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                isToggled = !isToggled;
                icon.Source = isToggled ? toggled : untoggled;
                order.order.deal_price = isToggled;
            };

            main.GestureRecognizers.Add(tgr);
        }
    }
}