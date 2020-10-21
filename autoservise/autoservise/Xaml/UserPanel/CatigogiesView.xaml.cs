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
    public partial class CatigogiesView : ContentView
    {
        Grid maingrid;
        Label titleLabale;
        Label descriptionLabele;

        OrderModel order = OrderModel.GetInstance;

        int category_id = 0;

        public CatigogiesView()
        {
            InitializeComponent();

            titleLabale = (Label)this.FindByName("Title");
            descriptionLabele = (Label)this.FindByName("Descroption");
            maingrid = (Grid)this.FindByName("MainGerid");

           
        }
         
        public void SetData(int id, string title, string descriprton)
        {
            category_id = id;
            titleLabale.Text = title;
            descriptionLabele.Text = descriprton;
        }

        public void SetDelegate(SuckessDelegate action)
        {
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                order.oreder.category_id = category_id;
                if (action != null)
                    action();

            };
            maingrid.GestureRecognizers.Add(tgr);
        }

    }
}