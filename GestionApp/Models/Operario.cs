using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionApp.Models
{
    public class Operario
    {
        public int OperarioId { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }

        
        public virtual ICollection<Instalacion> Instalaciones { get; set; }
        
    }
}
