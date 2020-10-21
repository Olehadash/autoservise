using autoservise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace autoservise.Xaml.UserPanel.ImageLoader
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Galerry : ContentPage
    {
        OrderModel order = OrderModel.GetInstance;

        Grid mainGrid;

        int columne = 0;
        int row = 0;

        List<GeleryView> mass = new List<GeleryView>();

        public Galerry()
        {
            InitializeComponent();

            mainGrid = (Grid)this.FindByName("MainGrid");
            AddElement();
        }

        public void AddElement()
        {
            GeleryView view = new GeleryView();
            view.SetDelegate(AddElement);
            view.setId(mass.Count);
            view.SetRemoveDelegate(id => RemoveById(id));
            mass.Add(view);
            mainGrid.Children.Add(view, columne, row);
            if (columne ==0)
                columne++;
            else
            {
                columne = 0;
                row++;
            }
        }

        public void RemoveById(int id)
        {
            mainGrid.Children.Remove(mass[id]);
            if (columne == 1) columne = 0;
            else
            {
                columne = 1;
                row--;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            List<byte[]> images = new List<byte[]>();
            for(int i = 0; i< mass.Count; i++)
            {
                images.Add(mass[i].immageArray);
            }

            order.oreder.images = images;
            App.Current.MainPage = new AddOrder();
        }


        
    }
}