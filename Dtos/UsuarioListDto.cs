using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIWeb.Dtos {
    public class UsuarioListDto {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CorreoElectronico { get; set; }
        public DateTime FechaDeAlta { get; set; }
        public bool Activo { get; set; }
    }
}