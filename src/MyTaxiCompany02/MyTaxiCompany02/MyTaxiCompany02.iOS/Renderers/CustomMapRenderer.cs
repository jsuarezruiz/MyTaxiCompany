using MapKit;
using MyTaxiCompany02.Controls;
using MyTaxiCompany02.iOS.Maps;
using MyTaxiCompany02.iOS.Renderers;
using MyTaxiCompany02.Maps;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MyTaxiCompany02.iOS.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        private MKMapView _iosMapView;
        private CustomMap _customMap;
        private MapManager _mapManager;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _iosMapView = null;
                _mapManager = null;
            }

            if (e.NewElement != null)
            {
                _iosMapView = (MKMapView)Control;
                _customMap = (CustomMap)Element;
                _iosMapView.ZoomEnabled = true;

                AddManagers();
            }
        }

        private void AddManagers()
        {
            var annotationManager = new AnnotationManager(_iosMapView, _customMap);
            
            _mapManager = new MapManager(_customMap, annotationManager);
            _iosMapView.GetViewForAnnotation = annotationManager.GetViewForAnnotation;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("Renderer", StringComparison.CurrentCultureIgnoreCase))
            {
                _mapManager?.Initialize();
            }
            else
            {
                _mapManager?.HandleCustomMapPropertyChange(e);
            }
        }
    }
}