using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Xaml.UserPanel.ImageLoader
{

    delegate void RemoveByIdDelegate(int id);
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeleryView : ContentView
    {
        public byte[] immageArray;

        int id;

        StackLayout close_layout;
        StackLayout open_layout;
        Image phoneImage;
        ImageButton closeBut;

        Frame Mainframe;

        TapGestureRecognizer tgr = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
        TapGestureRecognizer closer = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

        SuckessDelegate afterImageLoad;

        public GeleryView()
        {
            InitializeComponent();

            close_layout = (StackLayout)this.FindByName("close");
            open_layout = (StackLayout)this.FindByName("AddLayout");
            phoneImage = (Image)this.FindByName("imageView");
            Mainframe = (Frame)this.FindByName("main");
            closeBut = (ImageButton)this.FindByName("closebutton");
        }

        public void setId(int id)
        {
            this.id = id;
        }


        public void SetDelegate(SuckessDelegate action)
        {
            
            tgr.TappedCallback = (sender, args) =>
            {
                
                if (action != null)
                    afterImageLoad = action;

                UploadPhoto();



            };
            Mainframe.GestureRecognizers.Add(tgr);
        }

        public void SetRemoveDelegate(Action<int> action)
        {
            
            closer.TappedCallback = (sender, args) =>
            {
                UploadPhoto();
                if (action != null)
                    action(this.id);
            };
            closeBut.GestureRecognizers.Add(closer);
        }

        async void UploadPhoto()
        {
            await CrossMedia.Current.Initialize();

            if(!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                //PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                CompressionQuality = 40
            });

            immageArray = System.IO.File.ReadAllBytes(file.Path);

            Mainframe.GestureRecognizers.Remove(tgr);

            imageView.Source = ImageSource.FromFile(file.Path);
            open_layout.IsVisible = false;
            close.IsVisible = true;

            afterImageLoad();


        }

    }


}