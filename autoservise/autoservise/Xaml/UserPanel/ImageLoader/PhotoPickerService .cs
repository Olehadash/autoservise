using autoservise.Xaml.UserPanel.ImageLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace autoservise.Xaml.UserPanel.ImageLoader
{


    class PhotoPickerService
    {
        TaskCompletionSource<Stream> taskCompletionSource;
        

        public Task<Stream> GetImageStreamAsync()
        {
            throw new NotImplementedException();
        }
    }
}
