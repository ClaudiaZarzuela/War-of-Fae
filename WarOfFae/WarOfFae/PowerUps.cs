using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public PowerUps() { }
    }
    public class Model
    {
        public static List<PowerUps> Drones = new List<PowerUps>()
        {
            new PowerUps()
            {
                Id = 1,
                Nombre = "Dron2",
                Imagen = "Assets/escudo.png" ,
                Explicacion = @"Explicación Dron2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer id facilisis lectus. Cras nec convallis ante, quis pulvinar tellus. Integer dictum accumsan pulvinar. Pellentesque eget enim sodales sapien vestibulum consequat. Maecenas eu sapien ac urna aliquam dictum. Nullam eget mattis metus. Donec pharetra, tellus in mattis tincidunt, magna ipsum gravida nibh, vitae lobortis ante odio vel quam.",
                Estado = PowerUps.estados.Aterrizado,
             },
             new PowerUps()
            {
                Id = 2,
                Nombre = "Dron2",
                Imagen = "Assets/corazon.png" ,
                Explicacion = @"Explicación Dron2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer id facilisis lectus. Cras nec convallis ante, quis pulvinar tellus. Integer dictum accumsan pulvinar. Pellentesque eget enim sodales sapien vestibulum consequat. Maecenas eu sapien ac urna aliquam dictum. Nullam eget mattis metus. Donec pharetra, tellus in mattis tincidunt, magna ipsum gravida nibh, vitae lobortis ante odio vel quam.",
                Estado = PowerUps.estados.Aterrizado,
               
             },
              new PowerUps()
            {
                Id = 3,
                Nombre = "Dron2",
                Imagen = "Assets/speed.png",
                Explicacion = @"Explicación Dron2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer id facilisis lectus. Cras nec convallis ante, quis pulvinar tellus. Integer dictum accumsan pulvinar. Pellentesque eget enim sodales sapien vestibulum consequat. Maecenas eu sapien ac urna aliquam dictum. Nullam eget mattis metus. Donec pharetra, tellus in mattis tincidunt, magna ipsum gravida nibh, vitae lobortis ante odio vel quam.",
                Estado = PowerUps.estados.Aterrizado,

             },
               new PowerUps()
            {
                Id = 4,
                Nombre = "Dron2",
                Imagen ="Assets/stop.png" ,
                Explicacion = @"Explicación Dron2 Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer id facilisis lectus. Cras nec convallis ante, quis pulvinar tellus. Integer dictum accumsan pulvinar. Pellentesque eget enim sodales sapien vestibulum consequat. Maecenas eu sapien ac urna aliquam dictum. Nullam eget mattis metus. Donec pharetra, tellus in mattis tincidunt, magna ipsum gravida nibh, vitae lobortis ante odio vel quam.",
                Estado = PowerUps.estados.Aterrizado,

             },



          };


        public static IList<PowerUps> GetAllDrones()
        {
            return Drones;
        }

        public static PowerUps GetDronById(int id)
        {
            return Drones[id];
        }
    }
}
