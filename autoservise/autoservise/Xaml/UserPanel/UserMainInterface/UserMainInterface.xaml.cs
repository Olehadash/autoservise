﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.UserPanel.UserMainInterface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserMainInterface : ContentPage
    {
        public UserMainInterface()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}