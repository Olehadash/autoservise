using autoservise.Models;
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
    public partial class TownView : ContentView
    {
        UserModel usermodel = UserModel.Instance();
        Grid maingrid;
        Label label;
        Image image;

        SuckessDelegate clickedit;

        int city_id = 0;
        string city_name = "";

        public void setdata(int id, string name)
        {
            this.city_id = id;
            this.city_name = name;
            label.Text = name;
        }

        public void SetDelegate(SuckessDelegate delegat)
        {
            clickedit = delegat;
        }

        public TownView()
        {
            InitializeComponent();

            label = (Label)this.FindByName("townLabel");
            image = (Image)this.FindByName("townImage");

            maingrid = (Grid)this.FindByName("MainGrid");

            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) => {

                if (clickedit != null)
                    clickedit();
                pick();
            };

            maingrid.GestureRecognizers.Add(tgr);
        }

        public void pick()
        {
            image.Source = "togglepick.png";
            usermodel.user.city_id = this.city_id;
        }

        public void unpick()
        {
            image.Source = "togglepick.png";
        }
    }
}