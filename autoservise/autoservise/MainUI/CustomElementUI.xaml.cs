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
    public enum UIType 
    {
        NONE,
        PICKER,
        BUTTON,
        DATEPICK,
        TIMEPICK
    }

    public enum CustomUIViewerType
    {
        None,
        City,
        Category,
        GalleryOpen,
        SetAdress,
        SetDate,
        SetBudget,
        GallaryOpenSecond,
        OrderSwitcher,
        OrderCityPicker,
        OrderDatePicker,
        OrderTimePicker
    }

    public class CustomUIViewModel
    {
        public string title;
        public string placeholder;
        public string source;
        public UIType uiType;

        public CustomUIViewModel(string title, string placeholder, string source, UIType uiType)
        {
            this.title = title;
            this.placeholder = placeholder;
            this.source = source;
            this.uiType = uiType;
        }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomElementUI : ContentView
    {
        UIRegistrator amodel = UIRegistrator.GetInstance;
        OrderModel order = OrderModel.GetInstance;
        DataModel data = DataModel.GetInstance;
        UserModel user = UserModel.Instance();

        UIType uitype = UIType.NONE;
        CustomUIViewerType customType = CustomUIViewerType.None;

        Label title;
        Frame border;
        Image icon;
        Label errorLabel;
        Grid mainGrid;
        Label captionLaabel;
        Picker picker;
        DatePicker datePicker;
        TimePicker timePicker;
        public CustomElementUI()
        {
            InitializeComponent();

            title = (Label)FindByName("Title");
            border = (Frame)FindByName("Border");
            icon = (Image)FindByName("Icon");
            errorLabel = (Label)FindByName("ErrorLabel");
            mainGrid = (Grid)FindByName("gridMain");
            captionLaabel = (Label)FindByName("LabelCaption");
            picker = (Picker)FindByName("Picker");
            datePicker = (DatePicker)FindByName("DatePicker");
            timePicker = (TimePicker)FindByName("TimePicker");

            errorLabel.IsVisible = false;
            icon.IsVisible = false;
        }

        public void SetAction(EmptyParametrDelegate action)
        {
            if(uitype == UIType.BUTTON)
            {
                TapGestureRecognizer tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
                tgr.TappedCallback = (sender, args) =>
                {
                    action();
                };

                mainGrid.GestureRecognizers.Add(tgr);
            }
        }
        public void SetData(CustomUIViewerType ctype)
        {
            CustomUIViewModel model = amodel.UIView[ctype];
            this.uitype = model.uiType;
            this.customType = ctype;

            title.Text = model.title;
            if (!string.IsNullOrEmpty(model.source))
            {
                icon.IsVisible = true;
                icon.Source = model.source;
            }
            else
            {
                mainGrid.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Star);
                mainGrid.ColumnDefinitions[1].Width = new GridLength(100, GridUnitType.Star);
            }
            switch (uitype)
            {
                case UIType.BUTTON:
                    captionLaabel.IsVisible = true;
                    captionLaabel.Text = model.placeholder;
                    picker.IsVisible = false;
                    break;
                case UIType.PICKER:
                    captionLaabel.IsVisible = false;
                    picker.Title = model.placeholder;
                    picker.IsVisible = true;
                    break;
                case UIType.DATEPICK:
                    captionLaabel.IsVisible = false;
                    datePicker.IsVisible = true;
                    break;
                case UIType.TIMEPICK:
                    captionLaabel.IsVisible = false;
                    timePicker.IsVisible = true;
                    break;
            }
            List<string> items = new List<string>();
            switch (customType)
            {
                case CustomUIViewerType.City:
                    
                    for(int i = 0;i<data.cities.Count;i++)
                    {
                        items.Add(data.cities[i].name);
                    }
                    SetPickerList(items);
                    break;
                case CustomUIViewerType.Category:
                    for (int i = 0; i < data.categories.Count; i++)
                    {
                        items.Add(data.categories[i].name);
                    }
                    SetPickerList(items);
                    break;
                case CustomUIViewerType.OrderCityPicker:
                    for (int i = 0; i < data.categories.Count; i++)
                    {
                        items.Add(data.categories[i].name);
                    }
                    SetPickerList(items);
                    break;
            }

        }

        public string GetInfo()
        {
            if(uitype == UIType.PICKER)
            {
                return picker.Title;
            }
            return "";
        }

        void SetPickerList(List<string> items)
        {
            picker.ItemsSource = items;
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
            if (string.IsNullOrEmpty(source))
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

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (customType)
            {
                case CustomUIViewerType.City:
                    user.user.city_id = data.GetCityByName(picker.SelectedItem.ToString());
                    break;
                case CustomUIViewerType.Category:
                    user.user.category_id = data.GetCAtegoryIdByName(picker.SelectedItem.ToString());
                    break;
                case CustomUIViewerType.OrderCityPicker:
                    order.order.adres = picker.SelectedItem.ToString();
                    break;
            }
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            order.order.time = datePicker.Date.ToString() + " ";
            if(!string.IsNullOrEmpty(timePicker.Time.ToString()))
            {
                order.order.time += timePicker.Time.ToString();
            }
        }

        private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(order.order.time))
                order.order.time += timePicker.Time.ToString();
        }
    }
}