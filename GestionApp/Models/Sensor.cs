using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionApp.Models
{
    public class Sensor
    {
        public int SensorId { get; set; }
        public string Nombre { get; set; }

        //public int TelefonoId { get; set; }            
        
        
        public virtual ICollection<Telefono> Telefonos { get; set; }
        
    }
}
