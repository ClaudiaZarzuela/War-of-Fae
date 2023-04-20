using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarOfFae
{
    public class MapaConPersonajes
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        public MapaConPersonajes() { }
    }
    public class Model2
    {
        public static List<MapaConPersonajes> Drones = new List<MapaConPersonajes>()
        {
           new MapaConPersonajes()
           {
               Id = 1,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 2,
              Imagen ="Assets/StoreLogo.png" ,
           },
             new MapaConPersonajes()
           {
               Id = 3,
              Imagen ="Assets/StoreLogo.png" ,
           },
              new MapaConPersonajes()
           {
               Id = 4,
              Imagen ="Assets/StoreLogo.png" ,
           },
               new MapaConPersonajes()
           {
               Id = 5,
               Imagen ="Assets/StoreLogo.png" ,
           },
           new MapaConPersonajes()
           {
               Id = 6,
              Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 7,
               Imagen ="Assets/StoreLogo.png" ,
           },
           new MapaConPersonajes()
           {
               Id = 8,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 9,
              Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 10,
              Imagen ="Assets/StoreLogo.png" ,
           },
             new MapaConPersonajes()
           {
               Id = 11,
               Imagen ="Assets/StoreLogo.png" ,
           },
              new MapaConPersonajes()
           {
               Id = 12,
               Imagen ="Assets/StoreLogo.png" ,
           },
               new MapaConPersonajes()
           {
               Id = 13,
               Imagen ="Assets/StoreLogo.png" ,
           },
           new MapaConPersonajes()
           {
               Id = 14,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 15,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 16,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 17,
               Imagen ="Assets/StoreLogo.png" ,
           },
             new MapaConPersonajes()
           {
               Id = 18,
               Imagen ="Assets/StoreLogo.png" ,
           },
              new MapaConPersonajes()
           {
               Id = 19,
               Imagen ="Assets/StoreLogo.png" ,
           },
               new MapaConPersonajes()
           {
               Id = 20,
               Imagen ="Assets/StoreLogo.png" ,
           },
           new MapaConPersonajes()
           {
               Id = 21,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 22,
               Imagen ="Assets/StoreLogo.png" ,
           },
           new MapaConPersonajes()
           {
               Id = 23,
              Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 24,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 25,
              Imagen ="Assets/StoreLogo.png" ,
           },
             new MapaConPersonajes()
           {
               Id = 26,
              Imagen ="Assets/StoreLogo.png" ,
           },
              new MapaConPersonajes()
           {
               Id = 27,
               Imagen ="Assets/StoreLogo.png" ,
           },
               new MapaConPersonajes()
           {
               Id = 28,
               Imagen ="Assets/StoreLogo.png" ,
           },
           new MapaConPersonajes()
           {
               Id = 29,
               Imagen ="Assets/StoreLogo.png" ,
           },
            new MapaConPersonajes()
           {
               Id = 30,
               Imagen ="Assets/StoreLogo.png" ,
           },


        };


        public static IList<MapaConPersonajes> GetAllDrones()
        {
            return Drones;
        }

        public static MapaConPersonajes GetDronById(int id)
        {
            return Drones[id];
        }
    }
}
