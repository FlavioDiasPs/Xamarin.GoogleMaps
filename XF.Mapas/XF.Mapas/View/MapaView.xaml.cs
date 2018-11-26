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
	public partial class MapaView : ContentPage
	{
		public MapaView ()
		{
			InitializeComponent ();

            meuMapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(-23.562394, -46.655345), Distance.FromMiles(1000)));
        }
	}
}