﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace WarOfFae
{

    public class ViewPowerUp : PowerUps
    {
        public Image Img;
        public ContentControl CCImg;
        public ViewPowerUp(PowerUps dron)
        {
            Id = dron.Id;
            Nombre = dron.Nombre;
            Imagen = dron.Imagen;
            Explicacion = dron.Explicacion;
            Estado = dron.Estado;
            Precio= dron.Precio;
            Img = new Image();
            string s = System.IO.Directory.GetCurrentDirectory() + "\\" + dron.Imagen;
            Img.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri(s));
          
        }
    }
}
