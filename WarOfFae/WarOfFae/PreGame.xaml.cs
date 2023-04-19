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

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarOfFae
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class PreGame : Page, INotifyPropertyChanged
    {
        public ObservableCollection<ViewPowerUp> ListaPowerUps { get; } = new ObservableCollection<ViewPowerUp>();
        public ObservableCollection<ViewPersonajes> ListaPersonajes { get; } = new ObservableCollection<ViewPersonajes>();
        public ObservableCollection<ViewMapaPersonajes> ListaPersonajesMapa { get; } = new ObservableCollection<ViewMapaPersonajes>();
        public event PropertyChangedEventHandler PropertyChanged;
       


        public ObservableCollection<ViewPowerUp> ListaPowerUpsElegidos { get; }=new ObservableCollection<ViewPowerUp>();
        public ObservableCollection<ViewPowerUp> ListaPowerUpsElems { get; } = new ObservableCollection<ViewPowerUp>();

        ViewPersonajes p; 
        bool pressedPowerUp1 = false; int pressedPowerUp1Id= 0;
        bool pressedPowerUp2 = false; int pressedPowerUp2Id =1;
        int pressedEl = 1; //ids: aire agua fuego tierra

        public PreGame()
        {
            this.InitializeComponent();
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
      


            base.OnNavigatedTo(e);
        }
        

        private void Back_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ListaPowerUpsElegidos.Add(ListaPowerUpsElems[pressedEl]);
            ListaPowerUpsElegidos.Add(ListaPowerUps[pressedPowerUp1Id]);
            ListaPowerUpsElegidos.Add(ListaPowerUps[pressedPowerUp2Id]);
            Frame.Navigate(typeof(InGame), ListaPowerUpsElegidos);
        }
        private void AjustesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(OptionsInGame));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewPowerUp o = ImageFlipView.SelectedItem as ViewPowerUp;

            if(PowerUp1.Content.ToString() == "Empty" || pressedPowerUp1)
            {
                if (o.Explicacion.ToString() != PowerUp2.Content.ToString())
                {
                     PowerUp1.Content = o.Explicacion.ToString();
                     PowerUp1.Background = new SolidColorBrush(Windows.UI.Colors.Thistle);
                     PowerUp1.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                     pressedPowerUp1Id = o.Id;
                }
                else
                {
                    var messageDialog = new MessageDialog("Power Up already chosen");
                    await messageDialog.ShowAsync();
                }

            }
           
            else if(PowerUp2.Content.ToString() == "Empty" || pressedPowerUp2)
            {
                if (o.Explicacion.ToString() != PowerUp1.Content.ToString())
                {
                    PowerUp2.Content = o.Explicacion.ToString();
                    PowerUp2.Background = new SolidColorBrush(Windows.UI.Colors.Thistle);
                    PowerUp2.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    pressedPowerUp2Id = o.Id;
                }
                else
                {
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
                    var messageDialog = new MessageDialog("Descripción tu wapa del power up de awita");
                    await messageDialog.ShowAsync();
                    break;
                case "TORNADO":
                    var messageDialog1 = new MessageDialog("Descripción tu wapa del power up de aire");
                    await messageDialog1.ShowAsync();
                    break;
                case "EARTHQUAKE":
                    var messageDialog2 = new MessageDialog("Descripción tu wapa del power up de tierra");
                    await messageDialog2.ShowAsync();
                    break;
                case "MAGMA":
                    var messageDialog3 = new MessageDialog("Descripción tu wapa del power up de fuego");
                    await messageDialog3.ShowAsync();
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

        private void GridViewPersonajes_ItemClick(object sender, ItemClickEventArgs e)
        {
            ViewPersonajes o = e.ClickedItem as ViewPersonajes;
          
            Imagen_Personaje.Source = o.Img.Source;
            Puntos.Text = o.Nombre;
            Descripcion1.Text = o.Explicacion1;
            Descripcion2.Text = o.Explicacion2;
        }

        private void GridViewPersonajes_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            ViewPersonajes Item = e.Items[0] as ViewPersonajes; 
            e.Data.SetText(Item.Id.ToString()); 
            e.Data.RequestedOperation = DataPackageOperation.Move;
            Imagen_Personaje.Source = Item.Img.Source;
            Puntos.Text = Item.Nombre;
            Descripcion1.Text = Item.Explicacion1;
            Descripcion2.Text = Item.Explicacion2;
        }

        private void MiCanvas_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void MiCanvas_Drop(object sender, DragEventArgs e)
        {
            var id = await e.DataView.GetTextAsync();
            ViewPersonajes O = ListaPersonajes.ElementAt(Int32.Parse(id)) as ViewPersonajes;
            Windows.UI.Xaml.Controls.Border o = e.OriginalSource as Windows.UI.Xaml.Controls.Border;
            ImageBrush u = o.Background as ImageBrush;
            u.ImageSource = O.Img.Source;
        }
    }
}
