using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using XF.Mapas.App_Code;
using XF.Mapas.Droid;

[assembly: ExportRenderer(typeof(MapaCustomizado), typeof(MapaCustomizadoRenderer))]
namespace XF.Mapas.Droid
{
    class MapaCustomizadoRenderer : MapRenderer
    {
        public MapaCustomizadoRenderer(Context context) : base(context) { }

        CircoNoMapa demarcarCirculo;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) { }

            if (e.NewElement != null)
            {
                var androidMapa = (MapaCustomizado)e.NewElement;
                demarcarCirculo = androidMapa.AreaDemarcada;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            var circleOptions = new CircleOptions();
            circleOptions.InvokeCenter(
                new LatLng(demarcarCirculo.Posicao.Latitude, demarcarCirculo.Posicao.Longitude));
            circleOptions.InvokeRadius(demarcarCirculo.RaioDemarcado);
            circleOptions.InvokeFillColor(0X66FF0000);
            circleOptions.InvokeStrokeColor(0X66FF0000);
            circleOptions.InvokeStrokeWidth(0);

            NativeMap.AddCircle(circleOptions);
        }
    }
}