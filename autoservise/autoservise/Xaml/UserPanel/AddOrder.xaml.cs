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
    public partial class AddOrder : ContentPage
    {
        OrderModel order = OrderModel.GetInstance;

        Entry entry;
        Switch switcher;

        Grid adresGrid;
        Grid dataGrid;
        Grid budgedGrid;
        Grid photoGrid;
        public AddOrder()
        {
            InitializeComponent();

            entry = (Entry)this.FindByName("Entry_Des");
            switcher = (Switch)this.FindByName("Switcher");

            adresGrid = (Grid)this.FindByName("AddAress");
            dataGrid = (Grid)this.FindByName("AddData");
            budgedGrid = (Grid)this.FindByName("AddBudget");
            photoGrid = (Grid)this.FindByName("AddPhoto");

            AdAllEvents();
        }

        void AdAllEvents()
        {
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            var goData = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            var goBudget = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            var goPhoto = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                App.Current.MainPage = new OrderAdres();
            };
            goData.TappedCallback = (sender, args) =>
            {
                App.Current.MainPage = new TimeAndDate();
            };
            goBudget.TappedCallback = (sender, args) =>
            {
                App.Current.MainPage = new Budget();
            };
            goPhoto.TappedCallback = (sender, args) =>
            {
                //App.Current.MainPage = new Budget();
            };

            adresGrid.GestureRecognizers.Add(tgr);
            dataGrid.GestureRecognizers.Add(goData);
            budgedGrid.GestureRecognizers.Add(goBudget);
            photoGrid.GestureRecognizers.Add(goPhoto);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new CunstamerOrderCotegory();
        }

        private void Entry_BindingContextChanged(object sender, EventArgs e)
        {
            order.oreder.description = entry.Text;
        }

        private void Switcher_Toggled(object sender, ToggledEventArgs e)
        {
            order.oreder.show_contacts = switcher.IsToggled;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            order.SendData(Sukcess, Error);
        }


        void Sukcess()
        {

        }

        void Error()
        {

        }
    }
}