using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WarOfFae
{
    public class ViewPersonajes : Personajes
    {
        public Image Img;
        public ContentControl CCImg;
        public ViewPersonajes(Personajes dron)
        {
            Id = dron.Id;
            Nombre = dron.Nombre;
            Imagen = dron.Imagen;
            Explicacion1 = dron.Explicacion1;
            Explicacion2 = dron.Explicacion2;
            Img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + dron.Imagen;
            Img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));

        }
    }

    public class CVDron : ContentControl
    {
        public string Nombre;
        public Image Img;
        public CompositeTransform Transformacion;   //Nuevo: Traslación+Rotación
        public CVDron(ViewPersonajes dron, double X, double Y)
        {
            Nombre = dron.Nombre;
            Img = new Image();
            Img.Source = dron.Img.Source;
            Img.Width = 50;
            Img.Height = 50;
            this.Content = Img;
            Transformacion = new CompositeTransform();
            Transformacion.TranslateX = X;
            Transformacion.TranslateY = Y;
            this.RenderTransform = Transformacion;
        }
    }

}