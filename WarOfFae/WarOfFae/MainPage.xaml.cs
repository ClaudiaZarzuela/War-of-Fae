using System;
using System.Collections.Generic;
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
using Windows.UI.Xaml.Navigation;
using Windows.Media.Playback;
using Windows.Media.Core;
using System.Threading.Tasks;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace WarOfFae
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MediaPlayer player;
        MediaPlayer music;
       // MediaPlayer music;
        public MainPage()
        {
            this.InitializeComponent();
            player = new MediaPlayer();
            music = new MediaPlayer();
            //startMusic();
        }

        private async void startMusic()
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("mainMenuMusic.wav");
            music.AutoPlay = true;
            music.IsLoopingEnabled = true;
            music.Source = MediaSource.CreateFromStorageFile(file);
            music.Play();
        }
        private async void Solo_Button(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(@"Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("buttonSound.mp3");
            player.AutoPlay = false;
            player.Source = MediaSource.CreateFromStorageFile(file);

            player.Play();
            Frame.Navigate(typeof(PreGame));
            music.Pause();
        }
        private void Duo_Button(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PreGame));
            music.Pause();
        }

        private void Inventario_Boton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Inventory));
            music.Pause();
        }

        private void Ajustes_Boton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Options));
            music.Pause();
        }
    }
}
