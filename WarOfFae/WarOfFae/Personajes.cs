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
        public string Explicacion1 { get; set; }
        public string Explicacion2 { get; set; }

        public Personajes() { }
    }
    public class Model1
    {
        public static List<Personajes> Drones = new List<Personajes>()
        {
           new Personajes()
           {
               Id = 0,
               Nombre = "Puntos: 1",
               Imagen ="Assets/Personaje 1.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 1",

           },
            new Personajes()
           {
               Id = 1,
               Nombre = "Puntos: 2",
               Imagen ="Assets/Personaje 2.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 2",

           },
             new Personajes()
           {
               Id = 2,
               Nombre = "Puntos: 3",
               Imagen ="Assets/Personaje 3.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 3",

           },
              new Personajes()
           {
               Id = 3,
               Nombre = "Puntos: 4",
               Imagen ="Assets/Personaje 4.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 4",

           },
               new Personajes()
           {
               Id = 4,
               Nombre = "Puntos: 5",
               Imagen ="Assets/Personaje 5.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 5",

           },
                new Personajes()
           {
               Id = 5,
               Nombre = "Puntos: 6",
               Imagen ="Assets/Personaje 6.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 6",
           },
                 new Personajes()
           {
               Id = 6,
               Nombre = "Puntos: 7",
               Imagen ="Assets/Personaje 7.png" ,
              Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 7",

           },
                  new Personajes()
           {
               Id = 7,
               Nombre = "Puntos: 8",
               Imagen ="Assets/Personaje 8.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 8",

           },
                    new Personajes()
           {
               Id = 8,
               Nombre = "Puntos: 9",
               Imagen ="Assets/Personaje 9.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 9",
           },
                   new Personajes()
           {
               Id = 9,
               Nombre = "Puntos: 10",
               Imagen ="Assets/Personaje 10.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 10",

           },
                    new Personajes()
           {
               Id = 10,
               Nombre = "Puntos: 11",
               Imagen ="Assets/Personaje 11.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 11",
           },
                     new Personajes()
           {
               Id = 11,
               Nombre = "Puntos: 12",
               Imagen ="Assets/Personaje 12.png" ,
               Explicacion1 = "Tetxo chuli sobre ",
               Explicacion2 = "el Persoanje 12",

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
