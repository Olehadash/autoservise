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
    public partial class SimpleButton : ContentView
    {
        EmptyParametrDelegate action;
        Button button;

        public SimpleButton()
        {
            InitializeComponent();
            button = (Button)FindByName("");
        }

        public void SetAction(EmptyParametrDelegate action)
        {
            this.action = action;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (action != null) action();
        }
    }
}