using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Identity.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using static WarOfFae.PreGame;
using Windows.Media.Playback;
using Windows.Media.Core;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarOfFae
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class InGame : Page, INotifyPropertyChanged
    {
        //Listas
        public ObservableCollection<ViewPersonajes> ListaPersonajes { get; } = new ObservableCollection<ViewPersonajes>();
        public ObservableCollection<ViewPowerUp> ListaPowerUpsElegidos { get; } = new ObservableCollection<ViewPowerUp>();
        public ObservableCollection<ViewPowerUp> ListaPowerUps { get; } = new ObservableCollection<ViewPowerUp>();

        string[] turn; 
        //Turno
        public string Turn; int turnN = 0;

        //Timer
        public string Time => (30-Stopwatch_timer.Elapsed.Seconds).ToString();
        private DispatcherTimer _timer;
        private DispatcherTimer _timerRect;
        private Stopwatch Stopwatch_timer;
        private Stopwatch Stopwatch_timerRect;
        public event PropertyChangedEventHandler PropertyChanged;


        //Mapa
        WarOfFae.PreGame.personajeEnMapa[,] matrizPersonajes = new WarOfFae.PreGame.personajeEnMapa[10, 3]; 


        public InGame()
        {
            this.InitializeComponent();
            //turno
            turn = new string [2];turn[0]="Your "; turn[1] = "Enemy's ";
            Turn = turn[0];
            //incializacion a vacio del mapa de personajes
            double w = Mi_Mapa.ActualWidth;
            double h = Mi_Mapa.ActualHeight;
            for (int i = 0; i < matrizPersonajes.GetLength(0); i++)
            {
                for (int j = 0; j < matrizPersonajes.GetLength(1); j++)
                {
                    ContentControl content = new ContentControl();
                    content.IsTabStop = true;
                    content.UseSystemFocusVisuals = true;
                    double x = (w / 10) * i;
                    double y = (h / 3) * j;
                    Image im = new Image();
                    string s = System.IO.Directory.GetCurrentDirectory() + '\\' + "Assets\\StoreLogo.png";
                    im.Source = new BitmapImage(new Uri(s));
                    content.Content = im;
                    WarOfFae.PreGame.personajeEnMapa p = new WarOfFae.PreGame.personajeEnMapa();
                    p.i = i;
                    p.j = j;
                    p.x = x;
                    p.y = y;
                    p.image = im;
                    p.hasImage = false;
                    p.c = content;
                    p.id = -1;
                    matrizPersonajes[i, j] = p;
                    Mi_Mapa.Children.Add(matrizPersonajes[i, j].c);
                    matrizPersonajes[i, j].c.SetValue(Canvas.TopProperty, p.y);
                    matrizPersonajes[i, j].c.SetValue(Canvas.LeftProperty, p.x);
                    matrizPersonajes[i, j].c.SetValue(Canvas.WidthProperty, w / 10);
                    matrizPersonajes[i, j].c.SetValue(Canvas.HeightProperty, h / 3); 
                    matrizPersonajes[i, j].c.KeyDown += show_ItemInMap;
                }
            }
            //personajes
            if (ListaPersonajes != null)
            {
                foreach (Personajes dron1 in Model1.GetAllDrones())
                {
                    ViewPersonajes VMitem1 = new ViewPersonajes(dron1);
                    ListaPersonajes.Add(VMitem1);
                }

            }
            //time
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timerRect = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };

            _timer.Tick += (sender, o) =>
            { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time))); 
                if (Stopwatch_timer.Elapsed.Seconds >= 30) 
                {
                    Flecha.Opacity = 1;
                    Rectangulo1.Opacity = 0.7;
                    Rectangulo2.Opacity = 0.7;
                    Rectangulo3.Opacity = 0.7;
                    Stopwatch_timer.Restart(); 
                    if (Turn == turn[0]) { Turn = turn[1]; } 
                    else { Turn = turn[0]; }
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Turn)));
                } 
              
            };
            _timerRect.Tick += (sender, o) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
                if (Stopwatch_timerRect.Elapsed.Seconds >= 32)
                {
                    Flecha.Opacity = 0;
                    Rectangulo1.Opacity = 0;
                    Rectangulo2.Opacity = 0;
                    Rectangulo3.Opacity = 0;
                    Stopwatch_timerRect.Restart();
                }

            };
            _timer.Start();
            _timerRect.Start();
            Stopwatch_timer = new Stopwatch(); Stopwatch_timer.Start();
            Stopwatch_timerRect = new Stopwatch(); Stopwatch_timerRect.Start();
        }
      

        private void Ajustes_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsInGame));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           DateTime.Now.ToLongTimeString();
           if (e?.Parameter is WarOfFae.PreGame.info pInfo)
           {
                for (int i = 0; i <pInfo.personajesP.GetLength(0); i++)
                {
                    for(int j=0; j< pInfo.personajesP.GetLength(1);j++)
                    {
                        matrizPersonajes[i, j].image.Source = pInfo.personajesP[i, j].image.Source;
                        matrizPersonajes[i, j].id = pInfo.personajesP[i, j].id;
                        matrizPersonajes[i, j].hasImage = pInfo.personajesP[i, j].hasImage;
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
       
        
        private async void show_ItemInMap(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter || e.OriginalKey == Windows.System.VirtualKey.GamepadA)
            {
                ContentControl item = sender as ContentControl;
                int i = 0;
                int j = 0;
                bool found = false;
                while (i < matrizPersonajes.GetLength(0) && !found)
                {
                    j = 0;
                    while (j < matrizPersonajes.GetLength(1) && !found)
                    {
                        if (matrizPersonajes[i, j].c == item) found = true;
                        else ++j;
                    }
                    if (!found) ++i;
                }
                if (found&& matrizPersonajes[i, j].hasImage)
                {
                    Imagen_Personaje.Source = matrizPersonajes[i, j].image.Source;
                    int idP = matrizPersonajes[i,j].id;
                    found = false; bool end = false;
                    i = 0;
                    while (!found&&!end) { if (ListaPersonajes[i].Id== idP) { found = true; } else { i++; if (i > ListaPersonajes.Count-1) end = true; } }
                    if (found)

                    {
                        Puntos.Text = ListaPersonajes[i].Nombre;
                        Descripcion1.Text = ListaPersonajes[i].Explicacion1;
                        Descripcion2.Text = ListaPersonajes[i].Explicacion2;
                        //esto no se
                        ContentControl g = Mi_Mapa.Children[0] as ContentControl;
                        g.Focus(FocusState.Keyboard);
                    }
                    /*
                    BitmapImage aux = (BitmapImage).image.Source;
                    matrizPersonajes[i, j].image.Source = pS.image;
                    matrizPersonajes[pS.i, pS.j].image.Source = aux;
                    pS.esDelGridView = false;
                    pS.image = null;
                    */
                }
            }
        }

        private async void PowerUp_OnClick(object sender, ItemClickEventArgs e)
        {
            ViewPowerUp Item = e.ClickedItem as ViewPowerUp;
            var dlg = new ContentDialog()
            {
                Title = "ACTIVITY",
                Content = "You used the "+Item.Nombre+" powerup",
                PrimaryButtonText = "Continue"

            };

            SolidColorBrush color = new SolidColorBrush();
            color.Color = Windows.UI.Colors.Pink;
            dlg.Background = color;
            await dlg.ShowAsync();
        }
    }
}
