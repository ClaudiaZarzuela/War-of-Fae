﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class PreGame : Page
    {
        public ObservableCollection<ViewPowerUp> ListaPowerUps { get; } = new ObservableCollection<ViewPowerUp>();

        public PreGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Cosntruye las listas de ModelView a partir de la lista Modelo 
            if (ListaPowerUps != null)
                foreach (PowerUps dron in Model.GetAllDrones())
                {
                    ViewPowerUp VMitem = new ViewPowerUp(dron);
                    ListaPowerUps.Add(VMitem);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ViewPowerUp o =  as ViewPowerUp;
            
           // PowerUp1.Content = o.Explicacion.ToString();
           
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
        private void CommandInvokedHandler(IUICommand command)
        {
           
        }
    }
}
