using autoservise.Controllers;
using autoservise.Models;
using autoservise.Models.Static;
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
    public partial class InputTextViewer : ContentView
    {
        AuthorizationPageModel amodel = AuthorizationPageModel.GetInstance;
        RoolsFormValidate rools = RoolsFormValidate.Instance();
        UserModel usermodel = UserModel.Instance();
        ServerController server = ServerController.GetInstance;

        InputTextViewerType type;
        Label title;
        Frame border;
        Image icon;
        Entry entry;
        Label errorLabel;
        Grid mainGrid;

        public InputTextViewer()
        {
            InitializeComponent();

            title = (Label)FindByName("Title");
            border = (Frame)FindByName("Border");
            icon = (Image)FindByName("Icon");
            entry = (Entry)FindByName("EntryLabel");
            errorLabel = (Label)FindByName("ErrorLabel");
            mainGrid = (Grid)FindByName("gridMain");

            errorLabel.IsVisible = false;
            icon.IsVisible = false;

            var tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            tgr.TappedCallback = (sender, args) =>
            {
                entry.Focus();
            };

            border.GestureRecognizers.Add(tgr);


        }

        public string GetInfo()
        {
            return entry.Text;
        }


        public void SetData(string title, string placeholder)
        {
            entry.Placeholder = placeholder;
            SetTitle(title);
        }
        public void SetData(string title, string placeholder, string source, bool isPassvord = false)
        {
            this.SetData(title, placeholder);
            SetIcon(source);
            entry.IsPassword = isPassvord;
        }
        public void SetData (InputTextViewerType type)
        {
            this.type = type;
            InputTextViewModel model = amodel.inputTextView[type];
            this.SetData(model.title, model.placeholder, model.source, model.isPassword);
            if (type == InputTextViewerType.ConfirmMail)
            {
                entry.HorizontalTextAlignment = TextAlignment.Center;
                mainGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
                mainGrid.ColumnDefinitions[1].Width = new GridLength(100, GridUnitType.Star);
            }
        }

        public bool CheckLocalRools()
        {
            HideError();

            if(type == InputTextViewerType.Email && !rools.IsValidEmail(entry.Text))
            {
                SetError("Неверный формат");
                return true;
            }
            if(rools.IsNullValidate(entry.Text))
            {
                SetError("Вы не заполнили поле");
                return true;
            }
            if(type == InputTextViewerType.Password)
            {
                if (rools.SetPasLink(entry.Text) && !rools.ComparePassword())
                {
                    SetError("Пароли не совпадают");
                    return true;
                }
            }

            return false;
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                this.title.IsVisible = false;
                return;
            }
            this.title.Text = title;
        }

        public void SetIcon(string source)
        {
            if(string.IsNullOrEmpty(source))
            {
                icon.IsVisible = false;
                return;
            }
            icon.IsVisible = true;
            icon.Source = source;
        }

        public void SetError(string errorCaption)
        {
            errorLabel.Text = errorCaption;
            errorLabel.IsVisible = true;

            border.BorderColor = Color.Red;
        }

        public void HideError()
        {
            errorLabel.IsVisible = false;
            border.BorderColor = Color.Transparent;
        }

        private void EntryLabel_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (this.type)
            {
                case InputTextViewerType.CustomerName:
                    usermodel.user.name = entry.Text;
                    break;
                case InputTextViewerType.Email:
                    usermodel.user.email = entry.Text;
                    break;
                case InputTextViewerType.OrganizationName:
                    usermodel.user.organization_name = entry.Text;
                    break;
                case InputTextViewerType.Password:
                    usermodel.user.password = entry.Text;
                    break;
                case InputTextViewerType.Phone:
                    usermodel.user.phone = entry.Text;
                    break;
                case InputTextViewerType.ConfirmMail:
                    usermodel.user.code = entry.Text;
                    break;
            }
        }
    }
}