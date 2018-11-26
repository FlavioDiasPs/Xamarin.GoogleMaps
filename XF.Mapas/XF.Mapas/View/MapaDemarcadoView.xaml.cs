using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using XF.Mapas.App_Code;

namespace XF.Mapas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapaDemarcadoView : ContentPage
	{
        CircoNoMapa circoNoMapa;
        public MapaDemarcadoView()
        {
            InitializeComponent();

            var item = new Position(-23.562394, -46.655345);
            meuMapa.Pins.Clear();
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = item,
                Label = "Local",
                Address = "Av. Paulista, 2150"
            };
            meuMapa.Pins.Add(pin);
            var itemNovoPin = new Position(-23.573065, -46.661631);
            var pinNovo = new Pin
            {
                Type = PinType.Place,
                Position = itemNovoPin,
                Label = "Local",
                Address = "R. Estados Unidos, 386-576"
            };
            meuMapa.Pins.Add(pinNovo);
            circoNoMapa = new CircoNoMapa
            {
                Posicao = item,
                RaioDemarcado = 1000
            };
            meuMapa.AreaDemarcada = circoNoMapa;
            meuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(item, Distance.FromMiles(1.0)));
        }
    }
}