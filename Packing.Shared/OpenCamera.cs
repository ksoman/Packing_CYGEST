using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace PackingCygest.Shared
{
    public  class OpenCamera : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //get the camera instance
            Intent camera = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(camera,1);

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (requestCode == 1 && requestCode == -1)
            {
                Bundle extras = data.Extras;
                var res = (Bitmap)extras.Get("data");
            }
            base.OnActivityResult(requestCode, resultCode, data);
        }


        public static void Init(ContextWrapper context)
        {

        }

    }
}
