using System.Linq;
using APIWeb.Data;
using APIWeb.Data.Interfaces;
using APIWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace APIWeb.Data {
    public class ApiRepository : IApiRepository {
        private readonly DataContext _context;

        public ApiRepository(DataContext context) {
            _context = context;
        }
        public async Task Add<T>(T entity) where T : class {

            if (entity == null) {
                return;
            }
            await _context.AddAsync(entity);
        }

        public void Delete<T>(T entity) where T : class {
            if (entity == null) {
                return;
            }
            _context.Remove(entity);
        }

        public async Task<Producto> GetProductoByIdAsync(int id) {

            if (_context.Productos == null) {
                return new Producto();
            }

            Producto? producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id); //1ra forma sin devolver el Default para evitar el null es usar FirstAsync pero genera error consumiendo el endpoint.

            return producto ?? new Producto();
        }

        public async Task<Producto> GetProductoByNombreAsync(string nombre) {
            if (_context.Productos == null) {
                return new Producto();
            }

            //2da forma usando el default pero verificando si es null 
            Producto? producto = await _context.Productos.FirstOrDefaultAsync(p => p.Nombre == nombre);

            return producto ?? new Producto();
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync() {

            if (_context.Productos == null) {
                return Enumerable.Empty<Producto>();
            }

            return await _context.Productos.ToListAsync() ?? Enumerable.Empty<Producto>();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id) {
            if (_context.Usuarios == null) {
                return new Usuario();
            }
            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            return usuario ?? new Usuario();
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombre) {
            if (_context.Usuarios == null) {
                return new Usuario();
            }
            Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == nombre);
            return usuario ?? new Usuario();
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync() {
            if (_context.Usuarios == null) {
                return Enumerable.Empty<Usuario>();
            }
            return await _context.Usuarios.ToListAsync() ?? Enumerable.Empty<Usuario>();
        }

        public async Task<bool> SaveAll() {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}