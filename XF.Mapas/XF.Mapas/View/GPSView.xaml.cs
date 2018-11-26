using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.Mapas.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GPSView : ContentPage
	{
        string desativado = "Indisponível";
        public int Precisao { get; set; } = (int)GeolocationAccuracy.Medium;
        public string[] Accuracies => Enum.GetNames(typeof(GeolocationAccuracy));
        CancellationTokenSource cts;

        public GPSView()
        {
            InitializeComponent();

            pkrAccuracies.ItemsSource = Accuracies;
        }

        string Formato(Location location, Exception ex = null)
        {
            if (location == null)
            {
                return $"Não foi possível recuperar a localização: {ex?.Message ?? string.Empty}";
            }

            return
                $"Latitude: {location.Latitude}\n" +
                $"Longitude: {location.Longitude}\n" +
                $"Precisão: {location.Accuracy}\n" +
                $"Altitude: {(location.Altitude.HasValue ? location.Altitude.Value.ToString() : desativado)}\n" +
                $"Heading: {(location.Course.HasValue ? location.Course.Value.ToString() : desativado)}\n" +
                $"Velocidade: {(location.Speed.HasValue ? location.Speed.Value.ToString() : desativado)}\n" +
                $"Data (UTC): {location.Timestamp:d}\n" +
                $"Horário (UTC): {location.Timestamp:T}";
        }

        private async void GetLocalizacao_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                lblUltimaLocal.Text = Formato(location);
            }
            catch (Exception ex)
            {
                lblUltimaLocal.Text = Formato(null, ex);
            }
        }

        private async void GetLocalizacaoAtual_Clicked(object sender, EventArgs e)
        {
            try
            {
                var requisicao = new GeolocationRequest((GeolocationAccuracy)Precisao);
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(requisicao, cts.Token);
                lblLocalizacao.Text = Formato(location);
            }
            catch (Exception ex)
            {
                lblLocalizacao.Text = Formato(null, ex);
            }
            finally
            {
                cts.Dispose();
                cts = null;
            }
        }
    }
}