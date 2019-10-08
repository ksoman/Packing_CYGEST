using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using System.ComponentModel;
using Xamarin.Forms;
using PackingCygest.Droid.Implementation;
using PackingCygest.Utils;

[assembly: ExportRenderer(typeof(CustomProgressBar), typeof(CustomProgressBarRenderer))]
namespace PackingCygest.Droid.Implementation
{
    public class CustomProgressBarRenderer : ProgressBarRenderer

    {
        /// <summary>
        /// Raises the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="ElementChangedEventArgs{ProgressBar}"/> instance containing the event data.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            if (Control != null)
            {
                UpdateBarColor();
                Control.ScaleY = 10; //Changes the height
               

            }
        }

        /// <summary>
        /// Called when [element property changed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomProgressBar.BarColorProperty.PropertyName)
            {
                UpdateBarColor();

            }
        }

        /// <summary>
        /// Updates the color of the bar.
        /// http://stackoverflow.com/a/29199280
        /// </summary>
        private void UpdateBarColor()
        {
            var element = Element as CustomProgressBar;

            if (element != null)
            {
                Control.IndeterminateDrawable.SetColorFilter(element.BarColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                Control.ProgressDrawable.SetColorFilter(element.BarColor.ToAndroid(), PorterDuff.Mode.SrcIn);
            }



        }
    }
}