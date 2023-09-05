using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TareaASPInfotep.Models
{
    public class Contacto
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }
}