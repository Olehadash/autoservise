using autoservise.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.UserPanel.UserMainInterface.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderViewForm : ContentView
    {
        UserModel usermodel = UserModel.Instance();

        Label category;
        Label city;
        Label description;

        Label adress;
        Label dateLabel;
        Label timelabel;
        Label budget;
        Label phonelabel;

        ScrollView galery;
        StackLayout galeryStack;

        Label titleLabel;

        StackLayout content;


        public OrderViewForm()
        {
            InitializeComponent();

            category = (Label)this.FindByName("NameLabel");
            city = (Label)this.FindByName("City");
            description = (Label)this.FindByName("DiscriptionLabel");

            adress = (Label)this.FindByName("AdressLabel");
            dateLabel = (Label)this.FindByName("LastDateLabel");
            timelabel = (Label)this.FindByName("TimeLabel");
            budget = (Label)this.FindByName("Budget");
            phonelabel = (Label)this.FindByName("PhoneLabel");

            galery = (ScrollView)this.FindByName("Galery");
            galeryStack = (StackLayout)this.FindByName("GaleryContent");

            titleLabel = (Label)this.FindByName("TitleLabel");

        }

        public async Task SetData(Order order)
        {
            category.Text = order.category_name;
            city.Text = order.city;
            description.Text = order.description;
            adress.Text = order.adres;

            DateTime date = Convert.ToDateTime(order.time);

            dateLabel.Text = date.Day + "." + date.Month + "." + date.Year;
            timelabel.Text = date.Hour + ":" + date.Minute;

            budget.Text = order.price.ToString();
            if (usermodel.user.user_type == "customer")
                phonelabel.Text = usermodel.user.phone.ToString();
            if(order.images.Count > 0)
            {
                galery.IsVisible = true;
                for(int i =0; i<order.images.Count; i++)
                {
                    Image image = new Image();
                    Stream stream = new MemoryStream(order.images[i]);
                    image.Source = ImageSource.FromStream(()=> new MemoryStream(order.images[i]));
                    image.WidthRequest = 100;
                    image.HeightRequest = 100;
                    galeryStack.Children.Add(image);
                }
            }
            titleLabel.Text = "Предложения";
            // Recall ContentBuild
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}