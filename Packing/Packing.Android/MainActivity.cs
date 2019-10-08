using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ZXing.Mobile;
using Plugin.Permissions;
using System.Threading.Tasks;
using PackingCygest.Droid.Implementation;
using Android.Content;
using PackingCygest.Shared;
using Android.Graphics;
using Android.Provider;

namespace PackingCygest.Droid
{
    [Activity(Label = "PackingCygest", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
    ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)

        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            MobileBarcodeScanner.Initialize(Application);
            base.OnCreate(bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

          
            //lazer BarcodeReader broadcastReceiver
            var lazerrec = new BarCodeReader();

            IntentFilter filter = new IntentFilter();
            filter.AddAction("com.zebra.sdl.action.STARTED");
            filter.AddAction("com.zebra.sdl.action.RELEASED");
            filter.AddAction("com.zebra.sdl.action.SCAN_COMPLETE");
            RegisterReceiver(lazerrec, filter);

            //Intent camera = new Intent(MediaStore.ActionImageCapture);
            //StartActivityForResult(camera, 1);

            LoadApplication(new App());
            Task.Run(async () => {
                await GrantPermissions();
            });

        }

        private async Task GrantPermissions()
        {
            await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Storage, Plugin.Permissions.Abstractions.Permission.Camera });

        }


        /// <summary>
        /// Called when [back pressed] prevent back press on the phone control navigation 
        /// </summary>
        public override void OnBackPressed()
        {
            
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //global::ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }


        //protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{
        //    if (requestCode == 1 && requestCode == Result.Ok)
        //    {
        //        Bundle extras = data.Extras;
        //        var res = (Bitmap)extras.Get("data");
        //    }
        //    base.OnActivityResult(requestCode, resultCode, data);
        //}
    }
}

