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
    public partial class OrderAdres : ContentPage
    {


        Picker picker;
        Entry adress;

        OrderModel order = OrderModel.GetInstance;

        DataModel data = DataModel.GetInstance;


        public OrderAdres()
        {
            InitializeComponent();

            adress = (Entry)this.FindByName("ArdesEntry");
            picker = (Picker)this.FindByName("TownPicker");

            for(int i = 0; i<data.cities.Count; i++)
            {
                picker.Items.Add(data.cities[i].name);
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            int selectedIndex = picker.SelectedIndex;
            for (int i = 0; i < data.cities.Count; i++)
            {
                if(picker.Items[selectedIndex] == data.cities[i].name)
                {
                    order.order.adres = data.cities[i].name + " ";
                }
            }

            order.order.adres += adress.Text;

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