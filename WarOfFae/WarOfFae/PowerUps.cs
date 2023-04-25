using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFi;
using Windows.Media.Devices;

namespace WarOfFae
{
    public class PowerUps
    {
        public enum estados { Aterrizado, Autonomo, Manual };

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        //public Image Img;
        public string Explicacion { get; set; }
        public estados Estado { get; set; }
        public int Precio { get; set; }

        public PowerUps() { }
    }
    public class Model
    {
        public static List<PowerUps> Drones = new List<PowerUps>()
        {
            new PowerUps()
            {
                Id = 0,
                Nombre = "Unbreakable Shield",
                Imagen = "Assets/escudo.png" ,
                Explicacion = "Stops every attack to one character from the enemy for 2 turns",
                Estado = PowerUps.estados.Aterrizado,
                Precio=10,
             },
             new PowerUps()
            {
                Id = 1,
                Nombre = "Healing Power",
                Imagen = "Assets/corazon.png" ,
                 Explicacion = "Gives you the ability to bring back a member of your troops",
                Estado = PowerUps.estados.Aterrizado,
                Precio=20,

    },
              new PowerUps()
            {
                Id = 2,
                Nombre = "Super Speed",
                Imagen = "Assets/speed.png",
                 Explicacion = "You can move a character two squares at a time for two turns",
                Estado = PowerUps.estados.Aterrizado,
                Precio=30,

             },
               new PowerUps()
            {
                Id = 3,
                Nombre = "HeartStopper",
                Imagen ="Assets/stop.png" ,
                 Explicacion = "You can kill an enemy diagonally",
                Estado = PowerUps.estados.Aterrizado,
                Precio=40,

             },



          };
        public static List<PowerUps> Drones2 = new List<PowerUps>()
        {
            //lista de powerups d elemento
            new PowerUps()
            {
                Id = 0,
                Nombre = "Air",
                Imagen = "Assets/air.png" ,
                Explicacion = "Tornado",
                Estado = PowerUps.estados.Aterrizado,
                Precio=0,
             },
             new PowerUps()
            {
                Id = 1,
                Nombre = "Water",
                Imagen = "Assets/water.png" ,
                Explicacion = "Tsunami",
                Estado = PowerUps.estados.Aterrizado,
                Precio=0,
             },
              new PowerUps()
            {
                Id = 2,
                Nombre = "Fire",
                Imagen = "Assets/fire.png" ,
                Explicacion = "Magma",
                Estado = PowerUps.estados.Aterrizado,
                Precio=0,
             },
               new PowerUps()
            {
                Id = 3,
                Nombre = "Earth",
                Imagen = "Assets/earth.png" ,
                Explicacion = "Earthquake",
                Estado = PowerUps.estados.Aterrizado,
                Precio=0,
             },
        };
        public static IList<PowerUps> GetAllDrones()
        {
            return Drones;
        }
        public static IList<PowerUps> GetAllDrones2()
        {
            return Drones2;
        }

        public static PowerUps GetDronById(int id)
        {
            return Drones[id];
        }
       
            
        
    }
}
