using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Models;

namespace APIWeb.Data.Interfaces {
    public interface IAuthRepository {
        Task<Usuario> Registrar(Usuario usuario, string password);
        Task<Usuario> Login(string correo, string password);
        Task<bool> ExisteUsuario(string correo);
    }
}