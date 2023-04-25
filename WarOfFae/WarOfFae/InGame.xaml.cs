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
using Windows.UI.Xaml.Media.Imaging;
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
        /*
        public struct personajeEnMapa
        {
            public int i;
            public int j;
            public double x;
            public double y;
            public Image image;
            public bool hasImage;
        }
        */
        WarOfFae.PreGame.personajeEnMapa[,] matrizPersonajes = new WarOfFae.PreGame.personajeEnMapa[10, 3];
        public InGame()
        {
            this.InitializeComponent();
            //incializacion a vacio del mapa de personajes
            double w = Mi_Mapa.ActualWidth;
            double h = Mi_Mapa.ActualHeight;
            for (int i = 0; i < matrizPersonajes.GetLength(0); i++)
            {
                for (int j = 0; j < matrizPersonajes.GetLength(1); j++)
                {
                    double x = (w / 10) * i;
                    double y = (h / 3) * j;
                    Image im = new Image();
                    string s = System.IO.Directory.GetCurrentDirectory() + '\\' + "Assets\\StoreLogo.png";
                    im.Source = new BitmapImage(new Uri(s));
                    WarOfFae.PreGame.personajeEnMapa p = new WarOfFae.PreGame.personajeEnMapa();
                    p.i = i;
                    p.j = j;
                    p.x = x;
                    p.y = y;
                    p.image = im;
                    p.hasImage = false;
                    matrizPersonajes[i, j] = p;
                    Mi_Mapa.Children.Add(matrizPersonajes[i, j].image);
                    matrizPersonajes[i, j].image.SetValue(Canvas.TopProperty, p.y);
                    matrizPersonajes[i, j].image.SetValue(Canvas.LeftProperty, p.x);
                    matrizPersonajes[i, j].image.SetValue(Canvas.WidthProperty, w / 10);
                    matrizPersonajes[i, j].image.SetValue(Canvas.HeightProperty, h / 3);
                }
            }
        }

        private void Ajustes_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsInGame));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            /*
            if (e?.Parameter is ObservableCollection<ViewPowerUp> pList)
            {
              
            }*/

           if(e?.Parameter is WarOfFae.PreGame.info pInfo)
           {
                for (int i = 0; i <pInfo.personajesP.GetLength(0); i++)
                {
                    for(int j=0; j< pInfo.personajesP.GetLength(1);j++)
                    {
                        matrizPersonajes[i, j].image.Source = pInfo.personajesP[i, j].image.Source;
                        
                    }
                }
                    ObservableCollection<ViewPowerUp> pList = pInfo.powerupsP;
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

        private void MiCanvas_DragOver(object sender, DragEventArgs e)
        {

        }

        private void MiCanvas_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
