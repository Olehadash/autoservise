using autoservise.Xaml.Autorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.Tutorial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tutorial3 : ContentPage
    {
        public Tutorial3()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new PreLog();
        }
    }
}