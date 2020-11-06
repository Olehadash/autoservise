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
    public partial class ButtonUI : ContentView
    {

        Label caption;
        public ButtonUI()
        {
            InitializeComponent();
            caption = (Label)this.FindByName("Caption");
        }

        public void SetCaption(string caption)
        {
            this.caption.Text = caption;
        }
    }
}