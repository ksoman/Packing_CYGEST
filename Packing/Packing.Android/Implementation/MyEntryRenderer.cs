using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using PackingCygest.Droid.Implementation;
using PackingCygest.Utils;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace PackingCygest.Droid.Implementation
{
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if(Control != null)
            {
                //Control.Background = Color.FromHex("#ffffff");
                this.Control.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
            }
        }
    }
}