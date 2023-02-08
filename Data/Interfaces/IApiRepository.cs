using APIWeb.Models;

namespace APIWeb.Data.Interfaces {
    public interface IApiRepository {//Para abstraer mas se puede hacer interfaces por cada modelo
        Task Add<T>(T entity) where T : class;  //Permite adicionar, eliminar tanto usuario y producto
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task<Usuario> GetUsuarioByNombreAsync(string nombre);
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoByIdAsync(int id);
        Task<Producto> GetProductoByNombreAsync(string nombre);
    }
}