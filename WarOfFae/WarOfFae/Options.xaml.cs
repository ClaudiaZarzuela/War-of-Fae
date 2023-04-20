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
        public Options()
        {
            this.InitializeComponent();
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Music)));
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
