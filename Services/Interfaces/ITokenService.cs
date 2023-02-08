using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Models;

namespace APIWeb.Services.Interfaces {
    public interface ITokenService {
        string CreateToken(Usuario usuario);
    }
}