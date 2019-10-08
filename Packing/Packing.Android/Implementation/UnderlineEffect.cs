using System;
using System.Globalization;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PackingCygest.Droid.Implementation;
using Android.Graphics;
using PackingCygest.Data;
using PackingCygest.Model;

[assembly: ResolutionGroupName(PackingCygest.Utils.UnderlineEffect.EffectNamespace)]
[assembly: ExportEffect(typeof(UnderlineEffect), nameof(UnderlineEffect))]
namespace PackingCygest.Droid.Implementation
{
    public class UnderlineEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            SetUnderline(true);
        }

        protected override void OnDetached()
        {
            SetUnderline(false);
        }

        protected override void OnElementPropertyChanged(System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == Label.TextProperty.PropertyName || args.PropertyName == Label.FormattedTextProperty.PropertyName)
            {
                SetUnderline(true);
            }
        }

        /// <summary>
        /// Sets the underline.
        /// </summary>
        /// <param name="underlined">if set to <c>true</c> [underlined].</param>
        private void SetUnderline(bool underlined)
        {
            try
            {
                var textView = (TextView)Control;
                if (underlined)
                {
                    textView.PaintFlags |= PaintFlags.UnderlineText;
                }
                else
                {
                    textView.PaintFlags &= ~PaintFlags.UnderlineText;
                }
            }
            catch (Exception e)
            {
                var da = new DatabaseAccessAsync();
                da.InsertException(new PackingCygestExceptionModel
                {
                    Message = e.Message,
                    StackTrace = e.StackTrace,
                    TimeSpan = DateTime.Today.ToString(CultureInfo.CurrentCulture),
                    MethodName = e.Source
                });
            }
        }
    }
}