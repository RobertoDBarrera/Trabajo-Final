using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GestionApp.Models
{
    public class Telefono
    {
        public Telefono()
        {
            Sensores = new List<Sensor>();
        }

        public int TelefonoId { get; set; }
        
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public float Precio { get; set; }

        
        
        [NotMapped]
        public List<int> SensoresList { get; set; }

        public virtual ICollection<Sensor> Sensores{ get; set; }
        
        
        public virtual ICollection<Instalacion> Instalaciones { get; set; }
        

    }
}
