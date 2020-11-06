using autoservise.Controllers;
using autoservise.Models;
using Rg.Plugins.Popup.Services;
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

    

    public partial class CunstamerOrderCotegory : ContentPage
    {

        StackLayout scrollContent;

        DataModel dataModel = DataModel.GetInstance;

        TownPickController townpickcontroller = TownPickController.GetInstance;
        LoadingPopupPage loading = new LoadingPopupPage();

        public CunstamerOrderCotegory()
        {
            InitializeComponent();

            scrollContent = (StackLayout)this.FindByName("ScrollContent");

            PopupNavigation.PushAsync(loading);
            townpickcontroller.GetData(Build, Error);
        }

        public void Build()
        {
            
            int categoriesMax = dataModel.categories.Count();
            for(int i = 0; i< categoriesMax; i++)
            {
                CatigogiesView category = new CatigogiesView();
                //category.SetData(dataModel.categories[i].id, dataModel.categories[i].name, dataModel.categories[i].description);
                //category.SetDelegate(CategoryPick);
                scrollContent.Children.Add(category);
            }
            PopupNavigation.RemovePageAsync(loading);
        }

        public void CategoryPick()
        {
            App.Current.MainPage = new AddOrder();
        }

        public void Error()
        {

        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}