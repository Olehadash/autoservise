using autoservise.Controllers;
using autoservise.Models;
using autoservise.Xaml.Autorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    
    public partial class CityPickForm : ContentView
    {
        DataModel datamodel = DataModel.GetInstance;
        UserModel userModel = UserModel.Instance();
        TownPickController townpickcontroller = TownPickController.GetInstance;
        List<TownView> pickerlist = new List<TownView>();
        AnimationController animation = AnimationController.GetInstance;

        StackLayout layout;
        public CityPickForm()
        {
            InitializeComponent();

            layout = (StackLayout)this.FindByName("Content");
            
        }

        public async Task Build()
        {
            int cityMax = datamodel.cities.Count;
            for (int i = 0; i < cityMax; i++)
            {
                TownView town = new TownView();
                town.setdata(datamodel.cities[i].id, datamodel.cities[i].name);
                town.SetDelegate(ClickOnMain);
                town.Opacity = 0;
                layout.Children.Add(town);

                pickerlist.Add(town);
            }
            await Show();
        }

        async Task Show()
        {
            for (int i = 0; i < pickerlist.Count; i++)
            {
                animation.OpacityInWthMove(pickerlist[i], i,100);
                await Task.Delay(50);
            }
        }

        public async Task Hide()
        {
            for (int i = 0; i < pickerlist.Count; i++)
            {
                animation.OpacityOutWthMove(pickerlist[i], i, 100);
                await Task.Delay(10);
            }
        }

        public void ClickOnMain()
        {
            for (int i = 0; i < pickerlist.Count; i++)
            {
                pickerlist[i].unpick();
            }
        }
    }
}