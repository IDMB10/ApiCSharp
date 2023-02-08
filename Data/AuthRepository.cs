using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Data.Interfaces;
using APIWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Data {
    public class AuthRepository : IAuthRepository {
        private readonly DataContext _context;

        public AuthRepository(DataContext context) {
            _context = context;
        }

        public async Task<bool> ExisteUsuario(string correo) {
            if (_context.Usuarios != null) {
                if (await _context.Usuarios.AnyAsync(u => u.CorreoElectronico == correo)) {
                    return true;
                }
            }
            return false;
        }

        public async Task<Usuario> Login(string correo, string password) {
            if (_context.Usuarios == null)
                return null;

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.CorreoElectronico == correo);

            if (usuario == null)
                return null;

            if (!VerifyPasswordHash(password, usuario.PasswordHash, usuario.PasswordSalt))
                return null;

            return usuario;
        }

        private static bool VerifyPasswordHash(string password, byte[]? passwordHash, byte[]? passwordSalt) {
            if (passwordSalt != null && passwordHash != null) {
                using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) {
                    var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHash.Length; i++) {
                        if (computedHash[i] != passwordHash[i])
                            return false;
                    }
                }
                return true;
            }
            return false;
        }

        public async Task<Usuario> Registrar(Usuario usuario, string password) {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            if (_context.Usuarios == null) {
                return new Usuario();
            }
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}