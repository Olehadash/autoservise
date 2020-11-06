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

namespace autoservise.Xaml.Autorization
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUser : ContentPage
    {
        UserModel usermodel = UserModel.Instance();
        CreateUserModel gum = CreateUserModel.Instance();
        RoolsFormValidate rools = RoolsFormValidate.Instance();
        CreateUserController creator = CreateUserController.Instance();

        LoadingPopupPage loading = new LoadingPopupPage();

        StackLayout createCons;
        StackLayout createxecutor;

        Entry customerName;
        Entry customerPhone;
        Entry customerEmail;
        Entry customepass;
        Entry customepassrepeat;

        Label ercn;
        Label ercp;
        Label erce;
        Label ercpas;
        Label ercpasr;

        Entry executorName;
        Entry executorPhone;
        Entry executorEmail;
        Entry executorpass;
        Entry executorpassrepeat;

        Label eren;
        Label erep;
        Label eree;
        Label erepas;
        Label erepasr;

        public CreateUser()
        {
            InitializeComponent();
            createCons = (StackLayout)this.FindByName("CreateConsumer");
            createxecutor = (StackLayout)this.FindByName("CreateExecutor");

            ercn = (Label)this.FindByName("cusNameErr");
            ercp = (Label)this.FindByName("cusPhoneErr");
            erce = (Label)this.FindByName("cusMailErr");
            ercpas = (Label)this.FindByName("cuspassErr");
            ercpasr = (Label)this.FindByName("cusNameErr");

            customerName = (Entry)this.FindByName("CustomerName");
            customerPhone = (Entry)this.FindByName("CustomerPhone");
            customerEmail = (Entry)this.FindByName("CustomerEmail");
            customepass = (Entry)this.FindByName("Customepass");
            customepassrepeat = (Entry)this.FindByName("customerPasswordRepeat");

            eren = (Label)this.FindByName("execNameErr");
            erep = (Label)this.FindByName("execPhoneErr");
            eree = (Label)this.FindByName("execMailErr");
            erepas = (Label)this.FindByName("execpassErr");
            erepasr = (Label)this.FindByName("execNameErr");

            executorName = (Entry)this.FindByName("ExecName");
            executorPhone = (Entry)this.FindByName("execPhone");
            executorEmail = (Entry)this.FindByName("execMail");
            executorpass = (Entry)this.FindByName("execPass");
            executorpassrepeat = (Entry)this.FindByName("execpassreErr");

            if (gum._isConsumer)
            {
                createCons.IsVisible = true;
                createxecutor.IsVisible = false;
            }
            else
            {
                createCons.IsVisible = false;
                createxecutor.IsVisible = true;
            }
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new PreLog();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Validate())
                return;
            if (gum._isConsumer)
            {
                usermodel.user.name = customerName.Text;
                usermodel.user.phone = customerPhone.Text;
                usermodel.user.email = customerEmail.Text;
                usermodel.user.user_type = "customer";
                usermodel.user.password = customepass.Text;
            }
            else
            {
                usermodel.user.organization_name = executorName.Text;
                usermodel.user.phone = executorPhone.Text;
                usermodel.user.email = executorEmail.Text;
                usermodel.user.user_type = "executor";
                usermodel.user.password = executorpass.Text;
            }

            await PopupNavigation.PushAsync(loading);
            creator.CreateUser(Succksess, Error);
        }

        public void Succksess()
        {
            PopupNavigation.RemovePageAsync(loading);
            //App.Current.MainPage = new ConfirmMailSecond();
        }

        public void Error()
        {
            PopupNavigation.RemovePageAsync(loading);
            ErrorPane("Error in connect!", "Error in connection. Try again!");
        }

        public bool Validate()
        {
            bool stat = false;

            if (gum._isConsumer)
            {
                ercn.IsVisible = false;
                ercp.IsVisible = false;
                erce.IsVisible = false;
                ercpas.IsVisible = false;
                ercpasr.IsVisible = false;

                if (!rools.IsValidEmail(customerEmail.Text))
                {
                    erce.Text = "Вы ввели неверный формат E-mail";
                    erce.IsVisible = false;
                    stat = true;
                }
                if (rools.IsNullValidate(customerEmail.Text))
                {
                    erce.Text = "Вы не указали E-mail";
                    erce.IsVisible = false;
                    stat = true;
                }
                if (rools.IsNullValidate(customepass.Text))
                {
                    ercpas.Text = "Вы не указали пароль";
                    ercpas.IsVisible = true;
                    stat = true;
                }
                if (rools.IsNullValidate(customerName.Text))
                {
                    ercn.IsVisible = true;
                    stat = true;
                }
                if (rools.IsNullValidate(customerPhone.Text))
                {
                    ercp.IsVisible = true;
                    stat = true;
                }
                if (!rools.CompareLineValidate(customepass.Text, customepassrepeat.Text))
                {
                    ercpasr.Text = "Пополи не совпадают";
                    ercpasr.IsVisible = true;
                    stat = true;
                }
            }
            else
            {
                eren.IsVisible = false;
                erep.IsVisible = false;
                eree.IsVisible = false;
                erepas.IsVisible = false;
                erepasr.IsVisible = false;

                if (!rools.IsValidEmail(executorEmail.Text))
                {
                    eree.Text = "Вы ввели неверный формат E-mail";
                    eree.IsVisible = false;
                    stat = true;
                }
                if (rools.IsNullValidate(executorEmail.Text))
                {
                    eree.Text = "Вы не указали E-mail";
                    eree.IsVisible = false;
                    stat = true;
                }
                if (rools.IsNullValidate(executorpass.Text))
                {
                    erepas.Text = "Вы не указали пароль";
                    erepas.IsVisible = true;
                    stat = true;
                }
                if (rools.IsNullValidate(executorName.Text))
                {
                    eren.IsVisible = true;
                    stat = true;
                }
                if (rools.IsNullValidate(executorPhone.Text))
                {
                    erep.IsVisible = true;
                    stat = true;
                }
                if (!rools.CompareLineValidate(executorpass.Text, executorpassrepeat.Text))
                {
                    erepasr.Text = "Пополи не совпадают";
                    erepasr.IsVisible = true;
                    stat = true;
                }
            }

            return stat;
        }

        private async void ErrorPane(string Title, string message)
        {
            await DisplayAlert(Title, message, "OK");
        }
    }
}