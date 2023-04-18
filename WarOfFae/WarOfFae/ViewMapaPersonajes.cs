using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WarOfFae
{
    public class ViewMapaPersonajes : MapaConPersonajes
    {
        public Image Img;
        public ContentControl CCImg;
        public ViewMapaPersonajes(MapaConPersonajes dron)
        {
            Id = dron.Id;
            Imagen = dron.Imagen;
            Img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + dron.Imagen;
            Img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));

        }
    }
}