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
	public partial class MarcarView : ContentPage
	{
        public MarcarView()
        {
            InitializeComponent();

            meuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(-23.562394, -46.655345), Distance.FromMiles(1000)));
        }

        private async void txtPesquisa_SearchButtonPressed(object sender, EventArgs e)
        {
            await LocalizarNoMapa(txtPesquisa.Text);
        }

        private async Task LocalizarNoMapa(string param)
        {
            Geocoder geocoder = new Geocoder();
            Task<IEnumerable<Position>> resultado =
                geocoder.GetPositionsForAddressAsync(param);

            IEnumerable<Position> posicoes = await resultado;
            foreach (Position item in posicoes)
            {
                meuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(item, Distance.FromMiles(5.0)));

                meuMapa.Pins.Clear();
                var pin = new Pin
                {
                    Type = PinType.Generic,
                    Position = item,
                    Label = "Localização",
                    Address = txtPesquisa.Text
                };
                meuMapa.Pins.Add(pin);
                break;
            }
        }
    }
}