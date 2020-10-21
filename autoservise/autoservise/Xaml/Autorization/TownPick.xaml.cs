using autoservise.Controllers;
using autoservise.Models;
using autoservise.Xaml.UserPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.Autorization
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TownPick : ContentPage
    {
        DataModel datamodel = DataModel.GetInstance;
        UserModel userModel = UserModel.Instance();
        TownPickController townpickcontroller = TownPickController.GetInstance;
        List<TownView> pickerlist = new List<TownView>();

        StackLayout layout;

        public TownPick()
        {
            InitializeComponent();

            layout = (StackLayout)this.FindByName("Content");

            townpickcontroller.GetData(Build, Error);
        }

        public void Build()
        {
            int cityMax = datamodel.cities.Count;
            for(int i =0; i<cityMax; i++)
            {
                TownView town = new TownView();
                town.setdata(datamodel.cities[i].id, datamodel.cities[i].name);
                town.SetDelegate(ClickOnMain);

                layout.Children.Add(town);

                pickerlist.Add(town);
            }
            
        }

        public void ClickOnMain()
        {
            for (int i = 0; i < pickerlist.Count; i++)
            {
                pickerlist[i].unpick();
            }
        }


        public void Error()
        {

        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (userModel.user.user_type.Equals("castomer"))
            {
                App.Current.MainPage = new CunstamerOrderCotegory();
            }
        }
    }
}