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
    public partial class TimeAndDate : ContentPage
    {
        DatePicker datepicker;
        TimePicker timepicker;

        OrderModel order = OrderModel.GetInstance;

        public TimeAndDate()
        {
            InitializeComponent();
            datepicker = (DatePicker)this.FindByName("DatePick");
            timepicker = (TimePicker)this.FindByName("TimePick");
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            order.oreder.time = datepicker.Date.ToString() + " " + timepicker.Time.ToString();
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