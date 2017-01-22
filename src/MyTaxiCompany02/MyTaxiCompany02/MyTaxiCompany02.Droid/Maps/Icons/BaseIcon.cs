using Android.Gms.Maps.Model;

namespace MyTaxiCompany02.Droid.Maps.Icons
{
    public abstract class BaseIcon
    {
        public MarkerOptions MarkerOptions { get; }

        protected BaseIcon()
        {
            MarkerOptions = new MarkerOptions();
            MarkerOptions.Draggable(false);
            MarkerOptions.Anchor(0.5f, 0.5f);
        }
    }
}