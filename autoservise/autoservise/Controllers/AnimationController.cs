using autoservise.Models.Static;
using FormsControls.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
public delegate void EmptyParametrDelegate();
namespace autoservise.Controllers
{
    class AnimationController
    {
        private static AnimationController _instace = new AnimationController();

        static internal AnimationController GetInstance
        {
            get
            {
                return _instace;
            }
        }

        public async void SwypeAnimation(View hideView, View showView)
        {
            showView.IsVisible = true;
            showView.TranslationY = hideView.Y;
            showView.TranslationX = hideView.X + 250;
            double position = hideView.X;
            hideView.Opacity = 1;
            showView.Opacity = 0;

            await Task.WhenAll(
                hideView.TranslateTo(-250, hideView.Y, 250, Easing.CubicIn),
                hideView.FadeTo(0, 250),
                showView.TranslateTo(position, hideView.Y, 250, Easing.CubicIn),
                showView.FadeTo(1, 250)
            );
        }

        public async void BallLineScaleSwitchAnimation (View hideView, View showView)
        {
            await Task.WhenAll(
                hideView.ScaleTo(1),
                showView.ScaleTo(2)
            );
        }
         public async void FeidOutOpacity(View view, EmptyParametrDelegate action)
        {
            await view.FadeTo(0, 500);
            action();
        }
        public async Task OpacityIn(View view, uint duration = 500)
        {
            view.Opacity = 0;
            await view.FadeTo(1, duration);
        }
        public async Task OpacityOut(View view, uint duration = 500)
        {
            await view.FadeTo(0, duration);
        }
        public async Task OpacityInWthMove(View view, int i, uint duration = 250)
        {
            double position = i*view.WidthRequest;
            Console.WriteLine(position);
            view.TranslationY = position + 15;

            await Task.WhenAll(
                view.FadeTo(1, duration),
                view.TranslateTo(view.X, position, duration)
            );
        }
        public async Task OpacityOutWthMove(View view, int i, uint duration = 250)
        {
            double position = i * view.WidthRequest;
            Console.WriteLine(position);
            view.TranslationY = position + 15;

            await Task.WhenAll(
                view.FadeTo(1, duration),
                view.TranslateTo(view.X, position, duration)
            );
        }

    }
}
