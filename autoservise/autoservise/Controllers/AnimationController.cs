using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace autoservise.Controllers
{
    class AnimationController
    {
        private static AnimationController _instance = new AnimationController();

        public AnimationController GetInstance
        {
            get {
                return _instance;
            }
        }

        public void SwipeAnimation()
        {

        }

    }
}
