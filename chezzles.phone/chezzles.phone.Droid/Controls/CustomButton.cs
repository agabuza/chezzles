using Android.Graphics;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using chezzles.phone.Droid.Controls;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(MyButtonRenderer))]
namespace chezzles.phone.Droid.Controls
{
    public class MyButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var label = (TextView)Control; // for example
            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "LeagueGothic-Regular.otf");
            label.TextSize = 28; // gets smaller if it doesn't fit
            label.Typeface = font;
        }
    }
}