using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.Tutorial.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorialImmageForm : ContentView
    {
        StackLayout mainLayout;
        Image mainImage;
        Label title;
        Label description;
        public TutorialImmageForm()
        {
            InitializeComponent();

            mainLayout = (StackLayout)FindByName("MainLayout");
            mainImage = (Image)FindByName("Picture");
            title = (Label)FindByName("Title");
            description = (Label)FindByName("Discription");
        }

        public StackLayout getMainStack
        {
            get
            {
                return mainLayout;
            }
        }

        public void SetData(string picture, string titles, string descr)
        {
            mainImage.Source = picture;
            title.Text = titles;
            description.Text = descr;
        }

        public void SetIsVisible(bool value)
        {
            mainLayout.IsVisible = value;
        }
    }
}