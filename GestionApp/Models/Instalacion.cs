using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionApp.Models
{
    public class Instalacion
    {
        public int InstalacionId { get; set; }

        public Boolean Exitosa { get; set; }
        public DateTime Fecha { get; set; }

        
       
        
        public int TelefonoId { get; set; }
        public virtual Telefono Telefono { get; set; }

        
        public int AppId { get; set; }
        public virtual App App { get; set; }

       
        public int OperarioId { get; set; }
        public virtual Operario Operario { get; set; }

        

    }
}
