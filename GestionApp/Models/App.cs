using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;


namespace GestionApp.Models
{
    public class App
    {
        public int AppId { get; set; }

        public string Nombre { get; set; }

        
        
        public virtual ICollection<Instalacion> Instalaciones { get; set; }

        
        
    }
}
