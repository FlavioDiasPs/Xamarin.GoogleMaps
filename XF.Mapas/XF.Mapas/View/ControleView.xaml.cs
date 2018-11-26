using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace XF.Mapas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ControleView : ContentPage
	{
        public ControleView()
        {
            InitializeComponent();

            meuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(-23.562394, -46.655345), Distance.FromMiles(1000)));
        }

        private void btnHybrid_Clicked(object sender, EventArgs e)
        {
            meuMapa.MapType = MapType.Hybrid;
        }

        private void btnSatellite_Clicked(object sender, EventArgs e)
        {
            meuMapa.MapType = MapType.Satellite;
        }

        private void btnStreet_Clicked(object sender, EventArgs e)
        {
            meuMapa.MapType = MapType.Street;
        }

        private void sldZoom_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var paramZoom = e.NewValue;
            var localizacao = 360 / (Math.Pow(2, paramZoom));

            meuMapa.MoveToRegion(
                new MapSpan(meuMapa.VisibleRegion.Center,
                localizacao, localizacao));
        }
    }
}