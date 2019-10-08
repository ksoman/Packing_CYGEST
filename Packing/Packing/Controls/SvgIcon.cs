using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PackingCygest.Controls
{
    /// <summary>
    /// Displays Svg
    /// http://www.pshul.com/2018/01/25/xamarin-forms-using-svg-images-with-skiasharp/
    /// </summary>
    public class SvgIcon : Xamarin.Forms.Frame
    {
        private readonly SKCanvasView _canvasView = new SKCanvasView();
        private SKCanvas _canvas;

        public SvgIcon()
        {
            Padding = new Thickness(0);
            HasShadow = false;
            BackgroundColor = Color.Transparent;

            Content = _canvasView;
            _canvasView.PaintSurface += CanvasViewOnPaintSurface;
        }

        private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            _canvas = args.Surface.Canvas;
            _canvas.Clear();

            if (string.IsNullOrEmpty(ResourceId))
                return;

            if (ResourceId.Contains("resource://"))
            {
                ResourceId = ResourceId.Replace("resource://", "");
            }

            using (Stream stream = GetType().Assembly.GetManifestResourceStream(ResourceId))
            {
                SkiaSharp.Extended.Svg.SKSvg svg = new SkiaSharp.Extended.Svg.SKSvg();
                if (ResourceId.Contains("storage"))
                {
                    svg.Load(ResourceId);
                }
                else
                {

                    svg.Load(stream);
                }

                SKImageInfo info = args.Info;
                _canvas.Translate(info.Width / 2f, info.Height / 2f);

                SKRect bounds = svg.ViewBox;
                float xRatio = info.Width / bounds.Width;
                float yRatio = info.Height / bounds.Height;

                float ratio = Math.Min(xRatio, yRatio);

                _canvas.Scale(ratio);
                _canvas.Translate(-bounds.MidX, -bounds.MidY);

                _canvas.DrawPicture(svg.Picture);
            }
        }

        #region Bindable Properties

        #region ResourceId

        public static readonly BindableProperty ResourceIdProperty = BindableProperty.Create(
            nameof(ResourceId), typeof(string), typeof(SvgIcon), default(string), propertyChanged: RedrawCanvas);


        public string ResourceId
        {
            get => (string)GetValue(ResourceIdProperty);
            set => SetValue(ResourceIdProperty, value);
        }

        #endregion

        #endregion

        private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
        {
            SvgIcon svgIcon = bindable as SvgIcon;
            svgIcon?._canvasView.InvalidateSurface();
        }
    }
}
