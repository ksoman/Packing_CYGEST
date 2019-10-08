using Android.Content;
using System;
using Xamarin.Forms;

namespace PackingCygest.Shared
{
    public class BarCodeReader : BroadcastReceiver
    {
        private static readonly Lazy<BarCodeReader> _barCodeReader =
            new Lazy<BarCodeReader>(() => new BarCodeReader());
        public static BarCodeReader Instance => _barCodeReader.Value;

        private string _code;

        public string Code
        {
            get => _code;
            set
            {
                _code = value;
                if (value != null)
                {
                    //@event.OnDevicesChanged(value);
                   Device.BeginInvokeOnMainThread(() => MessagingCenter.Send(this, "barcode", value));
                }
            }
        }

        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            if (action.Equals("com.zebra.sdl.action.SCAN_COMPLETE"))
            {
                string content = intent.Extras.GetString("com.zebra.sdl.extra.CONTENT");
                content = content.Replace("\\u0000", "");
                Code = content;
            }
        }


    }
}
