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
               Nombre = "Banshee",
               Imagen ="Assets/Personaje 1.png" ,
               Explicacion1 = "Strength: 1",
               Explicacion2 = "can kill the governor",

           },
            new Personajes()
           {
               Id = 1,
               Nombre = "Fae",
               Imagen ="Assets/Personaje 4.png" ,
               Explicacion1 = "Strength: 2",
               Explicacion2 = "",

           },
             new Personajes()
           {
               Id = 2,
               Nombre = "Engineer",
               Imagen ="Assets/Personaje 3.png" ,
               Explicacion1 = "Strength: 3",
               Explicacion2 = "can deactivate bombs",

           },
              new Personajes()
           {
               Id = 3,
               Nombre = "Archer",
               Imagen ="Assets/Personaje 2.png" ,
               Explicacion1 = "Strength: 4",
               Explicacion2 = "",

           },
               new Personajes()
           {
               Id = 4,
               Nombre = "Elf",
               Imagen ="Assets/Personaje 5.png" ,
               Explicacion1 = "Strength: 5",
               Explicacion2 = "",

           },
                new Personajes()
           {
               Id = 5,
               Nombre = "Witch",
               Imagen ="Assets/Personaje 12.png" ,
               Explicacion1 = "Strength: 6",
               Explicacion2 = "",
           },
                 new Personajes()
           {
               Id = 6,
               Nombre = "Gorgon",
               Imagen ="Assets/Personaje 6.png" ,
              Explicacion1 = "Strength: 7",
               Explicacion2 = "",

           },
                  new Personajes()
           {
               Id = 7,
               Nombre = "Ogre",
               Imagen ="Assets/Personaje 10.png" ,
               Explicacion1 = "Strength: 8",
               Explicacion2 = "",

           },
                    new Personajes()
           {
               Id = 8,
               Nombre = "Cazador",
               Imagen ="Assets/Personaje 8.png" ,
               Explicacion1 = "Strength: 9",
               Explicacion2 = "",
           },
                   new Personajes()
           {
               Id = 9,
               Nombre = "The Governor",
               Imagen ="Assets/Personaje 11.png" ,
               Explicacion1 = "can kill everyone",
               Explicacion2 = "except the Banshee",

           },
                    new Personajes()
           {
               Id = 10,
               Nombre = "Bomb",
               Imagen ="Assets/Personaje 7.png" ,
               Explicacion1 = "can kill everyone except ",
               Explicacion2 = "the Engineer, cannot move",
           },
                     new Personajes()
           {
               Id = 11,
               Nombre = "Dragon Egg",
               Imagen ="Assets/Personaje 9.png" ,
               Explicacion1 = "Once captured you win.",
               Explicacion2 = "Cannot move",

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
