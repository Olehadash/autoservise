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
    public partial class MyChatMessage : ContentView
    {
        public MyChatMessage()
        {
            InitializeComponent();
        }
    }
}