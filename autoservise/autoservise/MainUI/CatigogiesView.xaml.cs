using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

public delegate void IntparametrDelegate(int parametr);

namespace autoservise.Xaml.UserPanel
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CatigogiesView : ContentView
    {
        Categories _category;
        Grid maingrid;
        Label titleLabale;
        Label descriptionLabele;

        OrderModel order = OrderModel.GetInstance;

        int category_id = 0;
        int parent_id = 0;

        public CatigogiesView()
        {
            InitializeComponent();

            titleLabale = (Label)this.FindByName("Title");
            descriptionLabele = (Label)this.FindByName("Descroption");
            maingrid = (Grid)this.FindByName("MainGerid");

           
        }
         
        public void SetData(int id, string title, string descriprton, int parent_id)
        {
            this.parent_id = parent_id;
            category_id = id;
            titleLabale.Text = title;
            descriptionLabele.Text = descriprton;
        }

        public void SetData(Categories category)
        {
            this.category_id = category.id;
            this.titleLabale.Text = category.name;
            this.parent_id = category.parent_id;
            this.descriptionLabele.Text = category.description;
        }

        public void SetDelegate(IntparametrDelegate action)
        {
            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                order.order.category_id = category_id;
                if (action != null)
                    action(this.category_id);

            };
            maingrid.GestureRecognizers.Add(tgr);
        }

    }
}