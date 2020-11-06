using autoservise.Models;
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
    public partial class ToolBarUI : ContentView
    {
        UserModel usermodel = UserModel.Instance();

        StackLayout profileBut;
        StackLayout chatBut;
        StackLayout ordersBut;
        StackLayout recalsBut;
        StackLayout setOrderBut;
        StackLayout lastOrderBut;

        public ToolBarUI()
        {
            InitializeComponent();

            profileBut = (StackLayout)this.FindByName("ProfileButton");
            chatBut = (StackLayout)this.FindByName("ChatButton");
            ordersBut = (StackLayout)this.FindByName("OredersButton");
            recalsBut = (StackLayout)this.FindByName("RecallsButton");
            setOrderBut = (StackLayout)this.FindByName("SetOrderButton");
            lastOrderBut = (StackLayout)this.FindByName("LastOrdersButton");


            ordersBut.IsVisible = (usermodel.user.user_type == "customer");
            recalsBut.IsVisible = (usermodel.user.user_type == "executor");
            setOrderBut.IsVisible = (usermodel.user.user_type == "customer");
            lastOrderBut.IsVisible = (usermodel.user.user_type == "executor");

        }
    }
}