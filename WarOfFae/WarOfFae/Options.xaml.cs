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
    public sealed partial class Options : Page, INotifyPropertyChanged
    {
        string Music = "50";
        string Music2 = "50";
        public event PropertyChangedEventHandler PropertyChanged;
        string LanguageImage = "Assets/UK.png";
        MediaPlayer backgroundSound;
        int volumeMusic = 50/100;

        public Options()
        {
            this.InitializeComponent();
            backgroundSound = new MediaPlayer();
            startMusic();

        }

        private async void startMusic()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("backgroundMusic.mp3");
            backgroundSound.AutoPlay = true;
            backgroundSound.IsLoopingEnabled = true;
            backgroundSound.Source = MediaSource.CreateFromStorageFile(file);
            backgroundSound.Volume = (double)0.5;
         
            backgroundSound.Play();
        }
        private void Back_Boton_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            string msg = string.Format("{0}", e.NewValue);
            Music = msg;
            volumeMusic = int.Parse(msg);
            if (backgroundSound != null)
                backgroundSound.Volume = (double)volumeMusic / 100;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Music)));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(backgroundSound.Volume)));
        }
        private void Slider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            string msg = string.Format("{0}", e.NewValue);
            Music2 = msg;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Music2)));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Windows.UI.Xaml.Controls.ComboBoxItem u = Languaje.SelectedItem as Windows.UI.Xaml.Controls.ComboBoxItem;
            string o = u.Content.ToString();
            switch (o)
            {
                
                case "English(UK)": LanguageImage = "Assets/UK.png"; break;
                case "German": LanguageImage = "Assets/germany.png"; break;
                case "Spanish(ES)": LanguageImage = "Assets/spain.png"; break;
                
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LanguageImage)));
        }
    }
}
