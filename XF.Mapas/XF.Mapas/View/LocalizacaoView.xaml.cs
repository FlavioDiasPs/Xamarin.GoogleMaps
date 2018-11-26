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
	public partial class LocalizacaoView : ContentPage
	{
        public LocalizacaoView()
        {
            InitializeComponent();

            meuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                   new Position(-23.562394, -46.655345), Distance.FromMiles(1000)));
        }

        private async void txtCoordenada_SearchButtonPressed(object sender, EventArgs e)
        {
            string[] coordenada = txtCoordenada.Text.Split(',');
            if (coordenada.Count() > 1)
            {
                try
                {
                    Position position = new Position(
                        Convert.ToDouble(coordenada[0]),
                        Convert.ToDouble(coordenada[1]));
                    await LocalizarNoMapa(position);
                }
                catch (Exception)
                {
                    await DisplayAlert("Localização", "Coordenada inválida.", "OK");
                }
            }
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
                meuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(item, Distance.FromMiles(0.5)));
                break;
            }
        }

        private async Task LocalizarNoMapa(Position param)
        {
            Geocoder geocoder = new Geocoder();
            Task<IEnumerable<string>> resultado =
                geocoder.GetAddressesForPositionAsync(param);

            IEnumerable<string> posicoes = await resultado;
            foreach (var item in posicoes)
            {
                await LocalizarNoMapa(item);
                break;
            }
        }
    }
}