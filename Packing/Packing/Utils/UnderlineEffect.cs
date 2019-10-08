using Xamarin.Forms;

namespace PackingCygest.Utils
{
   public  class UnderlineEffect : RoutingEffect
    {
        public const string EffectNamespace = "Example";

        public UnderlineEffect() : base($"{EffectNamespace}.{nameof(UnderlineEffect)}")
        {
        }
    }
}
