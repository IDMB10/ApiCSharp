using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Dtos {
    public class UsuarioRegistroDto {
        public string? CorreoElectronico { get; set; }
        public string? Password { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }

        public UsuarioRegistroDto() {
            FechaDeAlta = DateTime.Now;
            Activo = true;
        }
    }
}