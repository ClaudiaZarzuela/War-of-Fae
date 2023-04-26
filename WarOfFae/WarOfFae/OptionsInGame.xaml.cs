using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.Media.Playback;
using Windows.Media.Core;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace WarOfFae
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    /// 
    public sealed partial class OptionsInGame : Page, INotifyPropertyChanged
    {
        string Music = "50";
        string Music2 = "50";
        public event PropertyChangedEventHandler PropertyChanged;
        bool ready = false;
        public OptionsInGame()
        {
            this.InitializeComponent();
            double v = WarOfFae.App.backgroundSound.Volume;
            double realV = v * 100;
            string volume = (realV).ToString();
            Music = volume;
            MusicSlider.Value = realV;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Music)));
            ready = true;

        }
        
      
        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
          
        }

        private void MainMenu_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Restart_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PreGame), false);
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (ready)
            {
                string msg = string.Format("{0}", e.NewValue);
                Music = msg;
                double volume = ((double)int.Parse(msg)) / 100;
                WarOfFae.App.backgroundSound.Volume = volume;
                WarOfFae.App.volumeMusic = volume;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Music)));

            }
        }
        private void Slider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            string msg = string.Format("{0}", e.NewValue);
            Music2 = msg;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Music2)));
        }
    }
}
