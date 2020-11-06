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
    public partial class SwitcherUI : ContentView
    {
        OrderModel order = OrderModel.GetInstance;
        CustomUIViewerType type;

        Switch switcher;
        Label title;

        public SwitcherUI()
        {
            InitializeComponent();

            switcher = (Switch)this.FindByName("Switcher");
            title = (Label)this.FindByName("Title");
        }

        public void SetData(CustomUIViewerType ctype)
        {
            type = ctype;
            if (ctype == CustomUIViewerType.OrderSwitcher)
            {
                title.Text = "Показать мой номер спецам";
            }
        }

        private void Switcher_Toggled(object sender, ToggledEventArgs e)
        {
            switch (type)
            {
                case CustomUIViewerType.OrderSwitcher:
                    order.order.show_contacts = switcher.IsToggled;
                    break;
            }
        }
    }
}