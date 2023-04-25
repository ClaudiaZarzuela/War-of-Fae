using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using Point = Windows.Foundation.Point;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarOfFae
{

    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PreGame : Page, INotifyPropertyChanged
    {
        MediaPlayer errorSound;
        MediaPlayer backgroundSound;
        MediaPlayer buttonSound;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ViewPowerUp> ListaPowerUps { get; } = new ObservableCollection<ViewPowerUp>();
        public ObservableCollection<ViewPersonajes> ListaPersonajes { get; } = new ObservableCollection<ViewPersonajes>();
        public ObservableCollection<ViewMapaPersonajes> ListaPersonajesMapa { get; } = new ObservableCollection<ViewMapaPersonajes>();
        public ObservableCollection<ViewPowerUp> ListaPowerUpsElegidos { get; }=new ObservableCollection<ViewPowerUp>();
        public ObservableCollection<ViewPowerUp> ListaPowerUpsElems { get; } = new ObservableCollection<ViewPowerUp>();

        ViewPersonajes p; 
        bool pressedPowerUp1 = false; int pressedPowerUp1Id= 0;
        bool pressedPowerUp2 = false; int pressedPowerUp2Id =1;
        int pressedEl = 1; //ids: aire agua fuego tierra
        int numPersonajesRestantes = 30;
        double volumeMusic = 100;


        public struct personajeEnMapa
        {
            public int i;
            public int j;
            public double x;
            public double y;
            public Image image;
            public bool hasImage;
            public ContentControl c;
            public int id;
        }

        public struct PersonajeSelecionado
        {
            public BitmapImage image;
            public bool esDelGridView;
            public int i;
            public int j;
            public int id;
        }
        PersonajeSelecionado pS = new PersonajeSelecionado();
       
        personajeEnMapa[,] matrizPersonajes = new personajeEnMapa[10, 3];
        public struct info 
        { 
            public personajeEnMapa[,] personajesP; 
            public ObservableCollection<ViewPowerUp>powerupsP ; 
        }

        public PreGame()
        {
            this.InitializeComponent();
            errorSound = new MediaPlayer();
            backgroundSound = new MediaPlayer();
            buttonSound = new MediaPlayer();
            pS.image = null;
            startMusic();
            double w = Mi_Mapa.ActualWidth;
            double h = Mi_Mapa.ActualHeight;
            for (int i = 0; i < matrizPersonajes.GetLength(0); i++)
            {
                for (int j = 0; j < matrizPersonajes.GetLength(1); j++)
                {
                    ContentControl content = new ContentControl();
                    content.IsTabStop = true;
                    content.UseSystemFocusVisuals = true;
                    //content.KeyDown += 
                    double x = (w / 10) * i;
                    double y = (h / 3) * j;
                    Image im = new Image();
                    string s = System.IO.Directory.GetCurrentDirectory() + '\\' + "Assets\\StoreLogo.png";
                    im.Source = new BitmapImage(new Uri(s));
                    content.Content = im;
                    personajeEnMapa p = new personajeEnMapa();
                    p.i = i;
                    p.j = j;
                    p.x = x;
                    p.y = y;
                    p.id =-1;
                    p.image = im;
                    p.hasImage = false;
                    p.c = content;
                    matrizPersonajes[i, j] = p;
                    Mi_Mapa.Children.Add(matrizPersonajes[i, j].c);
                    matrizPersonajes[i, j].c.SetValue(Canvas.TopProperty, p.y);
                    matrizPersonajes[i, j].c.SetValue(Canvas.LeftProperty, p.x);
                    matrizPersonajes[i, j].c.SetValue(Canvas.WidthProperty, w / 10);
                    matrizPersonajes[i, j].c.SetValue(Canvas.HeightProperty, h / 3);
                    matrizPersonajes[i, j].c.KeyDown += add_change_ItemInMap;
                }
            }


        }
        private async void startMusic()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("backgroundMusic.mp3");
            backgroundSound.AutoPlay = true;
            backgroundSound.IsLoopingEnabled = true;
            backgroundSound.Source = MediaSource.CreateFromStorageFile(file);
            backgroundSound.Volume = (double)volumeMusic / 100;
            backgroundSound.Play();
        }
      
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (ListaPowerUps != null)
            {
                foreach (PowerUps dron in Model.GetAllDrones())
                {
                    ViewPowerUp VMitem = new ViewPowerUp(dron);
                    ListaPowerUps.Add(VMitem);
                }
            }
            if (ListaPowerUpsElems != null)
            {
                foreach (PowerUps dron in Model.GetAllDrones2())
                {
                    ViewPowerUp VMitem = new ViewPowerUp(dron);
                    ListaPowerUpsElems.Add(VMitem);
                }
            }
            if (ListaPersonajes != null)
            {
                foreach (Personajes dron1 in Model1.GetAllDrones())
                {
                    ViewPersonajes VMitem1 = new ViewPersonajes(dron1);
                    ListaPersonajes.Add(VMitem1);
                }

            }
            if (ListaPersonajesMapa != null)
            {
                foreach (MapaConPersonajes dron2 in Model2.GetAllDrones())
                {
                    ViewMapaPersonajes VMitem2 = new ViewMapaPersonajes(dron2);
                    ListaPersonajesMapa.Add(VMitem2);
                }

            }
            if (e?.Parameter is double music)
            {
                volumeMusic = music;
            }
            base.OnNavigatedTo(e);
        }
        

        private void Back_Boton_Click(object sender, RoutedEventArgs e)
        {
            backgroundSound.Pause();
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ListaPowerUpsElegidos.Add(ListaPowerUpsElems[pressedEl]);
            ListaPowerUpsElegidos.Add(ListaPowerUps[pressedPowerUp1Id]);
            ListaPowerUpsElegidos.Add(ListaPowerUps[pressedPowerUp2Id]);
            info p; p.personajesP = matrizPersonajes; p.powerupsP = ListaPowerUpsElegidos;
            backgroundSound.Pause();
            Frame.Navigate(typeof(InGame), p);
            //if ((numPersonajesRestantes <= 0) && (PowerUp1.Content.ToString() != "Empty") && (PowerUp2.Content.ToString() != "Empty"))
            //{
            //    ListaPowerUpsElegidos.Add(ListaPowerUpsElems[pressedEl]);
            //    ListaPowerUpsElegidos.Add(ListaPowerUps[pressedPowerUp1Id]);
            //    ListaPowerUpsElegidos.Add(ListaPowerUps[pressedPowerUp2Id]);
            //    info p; p.personajesP = matrizPersonajes; p.powerupsP = ListaPowerUpsElegidos;
            //    backgroundSound.Pause();
            //    Frame.Navigate(typeof(InGame), p);
            //}
            //else
            //{
            //    Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            //    Windows.Storage.StorageFile file = await folder.GetFileAsync("error.wav");
            //    errorSound.AutoPlay = false;
            //    errorSound.Source = MediaSource.CreateFromStorageFile(file);
            //    errorSound.Play();
            //    var messageDialog = new MessageDialog("You are not ready to start the match! Try checking if you have:\n" +
            //        "      - Chosen all the necesary power ups\n" +
            //        "      - Placed all your troops on the map");
            //    await messageDialog.ShowAsync();
            //}
        }
        private void AjustesButton_Click(object sender, RoutedEventArgs e)
        {
            backgroundSound.Pause();
            Frame.Navigate(typeof(OptionsInGame));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewPowerUp o = ImageFlipView.SelectedItem as ViewPowerUp;

            if(PowerUp1.Content.ToString() == "Empty" || pressedPowerUp1)
            {
                if (o.Explicacion.ToString() != PowerUp2.Content.ToString())
                {
                     Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                     Windows.Storage.StorageFile file = await folder.GetFileAsync("buttonSound.mp3");
                     buttonSound.AutoPlay = false;
                     buttonSound.Source = MediaSource.CreateFromStorageFile(file);
                     buttonSound.Play();
                     PowerUp1.Content = o.Explicacion.ToString();
                     PowerUp1.Background = new SolidColorBrush(Windows.UI.Colors.Thistle);
                     PowerUp1.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                     pressedPowerUp1Id = o.Id;
                }
                else
                {
                    Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                    Windows.Storage.StorageFile file = await folder.GetFileAsync("error.wav");
                    errorSound.AutoPlay = false;
                    errorSound.Source = MediaSource.CreateFromStorageFile(file);
                    errorSound.Play();
                    var messageDialog = new MessageDialog("Power Up already chosen");
                    await messageDialog.ShowAsync();
                }

            }
           
            else if(PowerUp2.Content.ToString() == "Empty" || pressedPowerUp2)
            {
                if (o.Explicacion.ToString() != PowerUp1.Content.ToString())
                {
                    Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                    Windows.Storage.StorageFile file = await folder.GetFileAsync("buttonSound.mp3");
                    buttonSound.AutoPlay = false;
                    buttonSound.Source = MediaSource.CreateFromStorageFile(file);
                    buttonSound.Play();
                    PowerUp2.Content = o.Explicacion.ToString();
                    PowerUp2.Background = new SolidColorBrush(Windows.UI.Colors.Thistle);
                    PowerUp2.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    pressedPowerUp2Id = o.Id;
                }
                else
                {
                    Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                    Windows.Storage.StorageFile file = await folder.GetFileAsync("error.wav");
                    errorSound.AutoPlay = false;
                    errorSound.Source = MediaSource.CreateFromStorageFile(file);
                    errorSound.Play();
                    var messageDialog = new MessageDialog("Power Up already chosen");
                    await messageDialog.ShowAsync();
                }
            }

        }
       
        private void FuegoButton_Click(object sender, RoutedEventArgs e)
        {
            
            Fire.Background = new SolidColorBrush(Windows.UI.Colors.Teal);
            Earth.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Air.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Water.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);

            PowerElement.Background = new SolidColorBrush(Windows.UI.Colors.OrangeRed);
            PowerElement.Content = "MAGMA";
            PowerElement.BorderBrush = new SolidColorBrush(Windows.UI.Colors.DarkRed);
            PowerElement.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkRed);

            Elemento.Foreground = new SolidColorBrush(Windows.UI.Colors.OrangeRed);
            Elemento.Text = "FIRE";
            pressedEl = 2;
        } 
        private void AguaButton_Click(object sender, RoutedEventArgs e)
        {
            Fire.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Earth.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Air.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Water.Background = new SolidColorBrush(Windows.UI.Colors.Teal);

            PowerElement.Background = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            PowerElement.Content = "TSUNAMI";
            PowerElement.BorderBrush = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
            PowerElement.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkBlue);

            Elemento.Foreground = new SolidColorBrush(Windows.UI.Colors.LightBlue);
            Elemento.Text = "WATER";
            pressedEl = 1;
        } 
        private void TierraButton_Click(object sender, RoutedEventArgs e)
        {
            Fire.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Earth.Background = new SolidColorBrush(Windows.UI.Colors.Teal);
            Air.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Water.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);

            PowerElement.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            PowerElement.Content = "EARTHQUAKE";
            PowerElement.BorderBrush = new SolidColorBrush(Windows.UI.Colors.DarkGreen);
            PowerElement.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkGreen);

            Elemento.Foreground = new SolidColorBrush(Windows.UI.Colors.LightGreen);
            Elemento.Text = "EARTH";
            pressedEl = 3;
        }
        private void AireButton_Click(object sender, RoutedEventArgs e)
        {
            Fire.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Earth.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);
            Air.Background = new SolidColorBrush(Windows.UI.Colors.Teal);
            Water.Background = new SolidColorBrush(Windows.UI.Colors.CadetBlue);

            PowerElement.Background = new SolidColorBrush(Windows.UI.Colors.Yellow);
            PowerElement.Content = "TORNADO";
            PowerElement.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Orange);
            PowerElement.Foreground = new SolidColorBrush(Windows.UI.Colors.Orange);

            Elemento.Foreground = new SolidColorBrush(Windows.UI.Colors.Yellow);
            Elemento.Text = "AIR";
            pressedEl =0;
        }

        private async void PowerElement_Click(object sender, RoutedEventArgs e)
        {
            Button h = sender as Button;
            // Create the message dialog and set its content
            switch (h.Content.ToString())
            {
                case "TSUNAMI":
                    var dlg = new ContentDialog()
                    {
                        Title = "TSUNAMI",
                        Content = "Descripción tu wapa del power up de awita",
                        PrimaryButtonText = "Ok"

                    };

                    SolidColorBrush color = new SolidColorBrush();
                    color.Color = Windows.UI.Colors.LightBlue;
                    dlg.Background = color;
                    await dlg.ShowAsync();
                  
                    break;
                case "TORNADO":
                    var dlg1 = new ContentDialog()
                    {
                        Title = "TORNADO",
                        Content = "Descripción tu wapa del power up de aire",
                        PrimaryButtonText = "Ok"

                    };

                    SolidColorBrush color1 = new SolidColorBrush();
                    color1.Color = Windows.UI.Colors.Yellow;
                    dlg1.Background = color1;
                    await dlg1.ShowAsync();
                    
                    break;
                case "EARTHQUAKE":
                    var dlg2 = new ContentDialog()
                    {
                        Title = "EARTHQUAKE",
                        Content = "Descripción tu wapa del power up de tierra",
                        PrimaryButtonText = "Ok"

                    };

                    SolidColorBrush color2 = new SolidColorBrush();
                    color2.Color = Windows.UI.Colors.LightGreen;
                    dlg2.Background = color2;
                    await dlg2.ShowAsync();
                  
                    break;
                case "MAGMA":
                    var dlg3 = new ContentDialog()
                    {
                        Title = "EARTHQUAKE",
                        Content = "Descripción tu wapa del power up de fuego",
                        PrimaryButtonText = "Ok"

                    };

                    SolidColorBrush color3 = new SolidColorBrush();
                    color3.Color = Windows.UI.Colors.OrangeRed;
                    dlg3.Background = color3;
                    await dlg3.ShowAsync();
                   
                    break;
            }
       
        }

        private void PowerUp_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            
            if ((button.Name == "PowerUp1" && pressedPowerUp1) || (button.Name == "PowerUp2" && pressedPowerUp2))
            {
                button.BorderBrush = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
                button.BorderThickness = new Thickness(1);
                if (button.Name == "PowerUp1")
                    pressedPowerUp1 = false;
                else
                    pressedPowerUp2 = false;
            }

            else
            {
                if (button.Name == "PowerUp1" && !pressedPowerUp1)
                {
                    pressedPowerUp1 = true;
                    pressedPowerUp2 = false;
                    PowerUp2.BorderBrush = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
                    PowerUp2.BorderThickness = new Thickness(1);
                    button.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    button.BorderThickness = new Thickness(3);
                }
                else if(button.Name == "PowerUp2" && !pressedPowerUp2)
                {
                    pressedPowerUp1 = false;
                    pressedPowerUp2 = true;
                    PowerUp1.BorderBrush = new SolidColorBrush(Windows.UI.Colors.DarkBlue);
                    PowerUp1.BorderThickness = new Thickness(1);
                    button.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Yellow);
                    button.BorderThickness = new Thickness(3);
                }
              
               
            }
           
            
        }

        private async void add_change_ItemInMap(object sender, KeyRoutedEventArgs e)
        {
            if(e.Key == Windows.System.VirtualKey.Enter || e.OriginalKey == Windows.System.VirtualKey.GamepadA)
            {

                ContentControl item = sender as ContentControl;
                if (pS.image != null)
                {
                    if (pS.esDelGridView)
                    {
                        int i = 0;
                        int j = 0;
                        bool founded = false;
                        while(i < matrizPersonajes.GetLength(0) && !founded)
                        {
                            j = 0;
                            while (j < matrizPersonajes.GetLength(1) && !founded)
                            {
                                if (matrizPersonajes[i, j].c == item) founded = true;
                                else ++j;
                            }
                            if(!founded)++i;
                        }
                        if (!matrizPersonajes[i, j].hasImage)
                        {
                            if (CambiarNumeroPersonaje(GridViewPersonajes.SelectedIndex))
                            {
                                matrizPersonajes[i, j].image.Source = pS.image;
                                pS.image = null;
                                pS.esDelGridView = false;
                                pS.i = 0;
                                pS.j = 0;
                                matrizPersonajes[i, j].id = pS.id;
                                matrizPersonajes[i, j].hasImage = true;
                                matrizPersonajes[i, j].image.CanDrag = true;
                                matrizPersonajes[i, j].image.DragStarting += Mi_Mapa_DragStarting;
                            }
                            else
                            {
                                Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                                Windows.Storage.StorageFile file = await folder.GetFileAsync("error.wav");
                                errorSound.AutoPlay = false;
                                errorSound.Source = MediaSource.CreateFromStorageFile(file);
                                errorSound.Play();
                                var messageDialog2 = new MessageDialog("No te quedan personajes de este tipo.");
                                await messageDialog2.ShowAsync();
                            }
                        }
                        //GridViewItem g = GridViewPersonajes.ContainerFromIndex(GridViewPersonajes.SelectedIndex) as GridViewItem;
                        //g.Focus(FocusState.Keyboard);

                    }
                    else
                    {
                        int i = 0;
                        int j = 0;
                        bool founded = false;
                        while (i < matrizPersonajes.GetLength(0) && !founded)
                        {
                            j = 0;
                            while (j < matrizPersonajes.GetLength(1) && !founded)
                            {
                                if (matrizPersonajes[i, j].c == item) founded = true;
                                else ++j;
                            }
                            if (!founded) ++i;
                        }
                        if (founded && matrizPersonajes[i, j].hasImage)
                        {
                            BitmapImage aux = (BitmapImage)matrizPersonajes[i, j].image.Source;
                            matrizPersonajes[i, j].image.Source = pS.image;
                            matrizPersonajes[i, j].id = pS.id;
                            matrizPersonajes[pS.i, pS.j].image.Source = aux;
                            pS.esDelGridView = false;
                            pS.image = null;
                            pS.i = 0;
                            pS.j = 0;
                            pS.id = -1;
                        }
                    }
                }
                else
                {
                    int i = 0;
                    int j = 0;
                    bool founded = false;
                    while (i < matrizPersonajes.GetLength(0) && !founded)
                    {
                        j = 0;
                        while (j < matrizPersonajes.GetLength(1) && !founded)
                        {
                            if (matrizPersonajes[i, j].c == item) founded = true;
                            else ++j;
                        }
                        if (!founded) ++i;
                    }
                    if (founded && matrizPersonajes[i, j].hasImage)
                    {
                        pS.image = (BitmapImage)matrizPersonajes[i, j].image.Source;
                        pS.id = matrizPersonajes[i, j].id;
                        pS.esDelGridView = false;
                        pS.i = i;
                        pS.j = j;
                    }
                    else
                    {
                        pS.esDelGridView = false;
                        pS.image = null;
                        pS.i = 0;
                        pS.j = 0;
                    }
                }
            }
            else if (e.Key == Windows.System.VirtualKey.Escape || e.OriginalKey == Windows.System.VirtualKey.GamepadB)
            {
                GridViewItem g = GridViewPersonajes.ContainerFromIndex(GridViewPersonajes.SelectedIndex) as GridViewItem;
                g.Focus(FocusState.Keyboard);
            }

        }
        private void GridViewPersonajes_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewPersonajes o = e.ClickedItem as ViewPersonajes;
            string name = o.Id.ToString();
            pS.esDelGridView = true;
            pS.image = (BitmapImage)o.Img.Source;
            //pS.id = o.Id;
            Imagen_Personaje.Source = o.Img.Source;
            Puntos.Text = o.Nombre;
            Descripcion1.Text = o.Explicacion1;
            Descripcion2.Text = o.Explicacion2;
            ContentControl g = Mi_Mapa.Children[0] as ContentControl;
            g.Focus(FocusState.Keyboard);
        }

        private async void GridViewPersonajes_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            ViewPersonajes Item = e.Items[0] as ViewPersonajes; 
            e.Data.SetText("0_"+Item.Id.ToString());
            e.Data.RequestedOperation = DataPackageOperation.Move;
            Imagen_Personaje.Source = Item.Img.Source;
            Puntos.Text = Item.Nombre;
            Descripcion1.Text = Item.Explicacion1;
            Descripcion2.Text = Item.Explicacion2;

        }

        private void Mi_Mapa_DragStarting(object sender, DragStartingEventArgs e)
        {
            double w = Mi_Mapa.ActualWidth;
            double h = Mi_Mapa.ActualHeight;
            Point p = e.GetPosition(Mi_Mapa);
            double y = h / 3;
            double x = w / 10;
            double column = p.X / x;
            double row = p.Y / y;
            if (column >= 10) column = 9;
            if (row >= 3) row = 2;

            e.Data.SetText("1_" + ((int)column).ToString() + "_" + ((int)row).ToString());
        }
        private void MiCanvas_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void MiCanvas_Drop(object sender, DragEventArgs e)
        {
            var id = await e.DataView.GetTextAsync();
            string[] name = id.Split('_',StringSplitOptions.RemoveEmptyEntries);
            if (name[0] == "0")
            {
                ViewPersonajes O = ListaPersonajes.ElementAt(Int32.Parse(name[1])) as ViewPersonajes;

                double w = Mi_Mapa.ActualWidth;
                double h = Mi_Mapa.ActualHeight;
                Point p = e.GetPosition(Mi_Mapa);
                double y = h / 3;
                double x = w / 10;
                double column =  p.X / x;
                double row = p.Y / y;
                if (column >= 10) column = 9;
                if (row >= 3) row = 2;

                if (!matrizPersonajes[(int)column, (int)row].hasImage)
                {
                    if (CambiarNumeroPersonaje(int.Parse(name[1])))
                    {
                        matrizPersonajes[(int)column, (int)row].image.Source = O.Img.Source;
                        matrizPersonajes[(int)column, (int)row].id = O.Id;
                        matrizPersonajes[(int)column, (int)row].hasImage = true;
                        matrizPersonajes[(int)column, (int)row].image.SetValue(Canvas.WidthProperty, w / 10);
                        matrizPersonajes[(int)column, (int)row].image.SetValue(Canvas.HeightProperty, h / 3);
                        matrizPersonajes[(int)column, (int)row].image.CanDrag = true;
                        matrizPersonajes[(int)column, (int)row].image.DragStarting += Mi_Mapa_DragStarting;

                    }
                    else
                    {
                        Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
                        Windows.Storage.StorageFile file = await folder.GetFileAsync("error.wav");
                        errorSound.AutoPlay = false;
                        errorSound.Source = MediaSource.CreateFromStorageFile(file);
                        errorSound.Play();
                        var messageDialog2 = new MessageDialog("No te quedan personajes de este tipo.");
                        await messageDialog2.ShowAsync();
                    }
                }
            }
            else
            {
                double w = Mi_Mapa.ActualWidth;
                double h = Mi_Mapa.ActualHeight;
                Point p = e.GetPosition(Mi_Mapa);
                double y = h / 3;
                double x = w / 10;
                double column = p.X / x;
                double row = p.Y / y;
                if (column >= 10) column = 9;
                if (row >= 3) row = 2;

                personajeEnMapa aux;
                int name1 = int.Parse(name[1]);
                int name2 = int.Parse(name[2]);
                aux.image = matrizPersonajes[name1, name2].image;
                aux.id = matrizPersonajes[name1, name2].id;
                BitmapImage b = (BitmapImage)matrizPersonajes[name1, name2].image.Source;
                aux.image.Source = matrizPersonajes[name1, name2].image.Source;
                aux.hasImage = matrizPersonajes[name1, name2].hasImage;

                matrizPersonajes[name1, name2].hasImage = matrizPersonajes[(int)column, (int)row].hasImage;
                matrizPersonajes[name1, name2].image.Source = matrizPersonajes[(int)column, (int)row].image.Source;
                matrizPersonajes[(int)column, (int)row].image.Source = b;
                matrizPersonajes[(int)column, (int)row].hasImage = aux.hasImage;
                matrizPersonajes[(int)column, (int)row].id= aux.id;

                if (!matrizPersonajes[name1, name2].hasImage)
                {
                    matrizPersonajes[name1, name2].image.CanDrag = false;
                    matrizPersonajes[name1, name2].image.DragStarting -= Mi_Mapa_DragStarting;
                    matrizPersonajes[name1, name2].hasImage = false;
                    matrizPersonajes[(int)column, (int)row].image.CanDrag = true;
                    matrizPersonajes[(int)column, (int)row].hasImage = true;
                    matrizPersonajes[(int)column, (int)row].image.DragStarting += Mi_Mapa_DragStarting;

                }
              
             
            }

        }
        private bool CambiarNumeroPersonaje(int num)
        {
            bool sePuede = true;
            TextBlock p = null;
            switch (num)
            {
                case 0: p =P0 ; break;
                case 1: p =P1 ; break;
                case 2: p =P2 ; break;
                case 3: p =P3 ; break;
                case 4: p =P4 ; break;
                case 5: p =P5 ; break;
                case 6: p =P6 ; break;
                case 7: p =P7 ; break;
                case 8: p =P8 ; break;
                case 9: p =P9 ; break;
                case 10: p =P10 ; break;
                case 11: p =P11 ; break;
            }
            if(p.Text == "0") sePuede = false;
            else
            {
                int n = int.Parse(p.Text);
                --n;
                if (n >= 0)
                {
                    --numPersonajesRestantes;
                    p.Text = n.ToString();
                }

            }
            return sePuede;
        }
    }
}
