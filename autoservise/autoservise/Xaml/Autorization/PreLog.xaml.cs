using autoservise.Controllers;
using autoservise.MainUI;
using autoservise.Models;
using autoservise.Models.Static;
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
    public partial class PreLog : ContentPage
    {
        AnimationController animation = AnimationController.GetInstance;
        AuthorizationPageModel authorization = AuthorizationPageModel.GetInstance;

        UserModel usermodel = UserModel.Instance();

        StackLayout mainLayout;
        RelativeLayout rootLayout;

        public PreLog()
        {
            InitializeComponent();

            mainLayout = (StackLayout)FindByName("Content");
            rootLayout = (RelativeLayout)FindByName("RootLayout");

            for (int i = 0; i< authorization.buttons.Count; i++)
            {
                ArrowButton arrowButton = new ArrowButton();
                arrowButton.SetData(authorization.buttons[i].source, authorization.buttons[i].title, authorization.buttons[i].subtitle, (PageType)i+1);
                arrowButton.SetButtonDelegate(buttonClickAction);
                mainLayout.Children.Add(arrowButton);
            }
        }

        public void buttonClickAction(PageType viewName)
        {
            switch(viewName)
            {
                case PageType.Authorization:
                    usermodel.setUserType(UserType.NONE);
                    break;
                case PageType.CreateCustomer:
                    usermodel.setUserType(UserType.CASTOMER);
                    break;
                case PageType.CreateExecutor:
                    usermodel.setUserType(UserType.EXECUTOR);
                    break;
            }
            animation.FeidOutOpacity(rootLayout, SwitchForm);
            
        }

        void SwitchForm()
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}