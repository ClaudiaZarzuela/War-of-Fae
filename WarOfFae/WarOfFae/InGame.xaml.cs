using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarOfFae
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class InGame : Page
    {
        public ObservableCollection<ViewPersonajes> ListaPersonajes { get; } = new ObservableCollection<ViewPersonajes>();
        public ObservableCollection<ViewPowerUp> ListaPowerUpsElegidos { get; } = new ObservableCollection<ViewPowerUp>();
        public ObservableCollection<ViewPowerUp> ListaPowerUps { get; } = new ObservableCollection<ViewPowerUp>();

        public string Turn = "Your ";
        public InGame()
        {
            this.InitializeComponent();
        }

        private void Ajustes_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsInGame));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e?.Parameter is ObservableCollection<ViewPowerUp> pList)
            {
               for(int i=0; i<pList.Count; i++)
               {
                    ListaPowerUpsElegidos.Add(pList[i]); 
               }
            }
            base.OnNavigatedTo(e);
        }

        private void ImageGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
