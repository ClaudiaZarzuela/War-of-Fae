using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarOfFae
{
    public class Personajes
    {
        public enum estados { Aterrizado, Autonomo, Manual };

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        //public Image Img;
        public string Explicacion { get; set; }

        public Personajes() { }
    }
    public class Model1
    {
        public static List<Personajes> Drones = new List<Personajes>()
        {
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },
           new Personajes()
           {
               Id = 4,
               Nombre = "Dron2",
               Imagen ="Assets/stop.png" ,
               Explicacion = "HeartStopper",

           },



        };


        public static IList<Personajes> GetAllDrones()
        {
            return Drones;
        }

        public static Personajes GetDronById(int id)
        {
            return Drones[id];
        }
    }
}
