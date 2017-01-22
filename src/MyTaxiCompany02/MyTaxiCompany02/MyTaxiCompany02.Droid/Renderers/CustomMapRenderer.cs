using MyTaxiCompany02.Droid.Renderers;
using MyTaxiCompany02.Controls;
using Xamarin.Forms;
using Android.Gms.Maps;
using Xamarin.Forms.Maps.Android;
using System;
using System.ComponentModel;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps;
using MyTaxiCompany02.Maps;
using MyTaxiCompany02.Droid.Maps;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MyTaxiCompany02.Droid.Renderers
{
    public class CustomMapRenderer : MapRenderer, IOnMapReadyCallback
    {
        private MapView _androidMapView;
        private GoogleMap _nativeMap;
        private CustomMap _customMap;
        private MapManager _mapManager;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _nativeMap = null;
                _mapManager = null;
                _androidMapView = null;
            }

            if (e.NewElement != null)
            {
                _androidMapView = Control;
                _customMap = (CustomMap)Element;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("Renderer", StringComparison.CurrentCultureIgnoreCase))
            {
                _androidMapView.GetMapAsync(this);
            }
            else
            {
                _mapManager?.HandleCustomMapPropertyChange(e);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            _nativeMap = googleMap;

            // Disable zoom buttons
            _nativeMap.UiSettings.ZoomControlsEnabled = true;
            _nativeMap.UiSettings.MapToolbarEnabled = false;

            AddManagers();

            _mapManager?.Initialize();
        }

        private void AddManagers()
        {
            var annotationManager = new MarkerManager(_nativeMap, _customMap);
          
            _mapManager = new MapManager(_customMap, annotationManager);
        }
    }
}