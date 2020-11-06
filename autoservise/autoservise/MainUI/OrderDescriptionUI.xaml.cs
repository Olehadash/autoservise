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
    public partial class OrderDescriptionUI : ContentView
    {
        OrderModel orders = OrderModel.GetInstance;

        Order order;

        Label title;
        Label recall;
        Label description;

        public OrderDescriptionUI()
        {
            InitializeComponent();

            title = (Label)this.FindByName("Title");
            recall = (Label)this.FindByName("RecalsNum");
            description = (Label)this.FindByName("Description");
        }

        public void SetData(Order order)
        {
            this.order = order;
            this.title.Text = order.category_name;
            this.recall.Text = order.status.ToString() + " откликов";
            this.description.Text = order.description;
        }
    }
}