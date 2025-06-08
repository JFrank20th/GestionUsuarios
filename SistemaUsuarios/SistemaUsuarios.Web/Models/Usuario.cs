using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaUsuarios.Web.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }
    }
}