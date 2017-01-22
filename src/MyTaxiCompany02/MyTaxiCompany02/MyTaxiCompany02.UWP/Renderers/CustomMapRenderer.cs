using MyTaxiCompany02.Controls;
using MyTaxiCompany02.Maps;
using MyTaxiCompany02.UWP.Maps;
using MyTaxiCompany02.UWP.Renderers;
using System;
using System.ComponentModel;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MyTaxiCompany02.UWP.Renderers
{
    public class CustomMapRenderer : MapRenderer
    {
        private MapControl _nativeMap;
        private CustomMap _customMap;
        private MapManager _mapManager;

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                _nativeMap = null;
                _mapManager = null;
            }

            if (e.NewElement != null)
            {
                _nativeMap = Control;
                _customMap = (CustomMap)Element;

                AddManagers();
            }
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

        private void AddManagers()
        {
            var annotationManager = new PushpinManager(_nativeMap, _customMap);
        
            _mapManager = new MapManager(_customMap, annotationManager);
        }
    }
}