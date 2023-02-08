using APIWeb.Data.Interfaces;
using APIWeb.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.Controllers {
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase {
        private readonly IApiRepository _repository;
        private readonly IMapper _mapper;
        public ProductoController(IApiRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            // var productos = await _repository.GetProductosAsync();
            // return Ok(productos);

            //Usando Automapper
            var productos = await _repository.GetProductosAsync();
            var productosDto = _mapper.Map<IEnumerable<ProductoListarDTO>>(productos);
            return Ok(productosDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            var producto = await _repository.GetProductoByIdAsync(id);

            //usando un dto para no exponer todos los campos
            // ProductoListarDTO productoDto = new ProductoListarDTO();
            // productoDto.Id = producto.Id;
            // productoDto.Nombre = producto.Nombre;
            // productoDto.Descripcion = producto.Descripcion;

            //Usando Automapper
            ProductoListarDTO productoDto = _mapper.Map<ProductoListarDTO>(producto);

            if (producto == null) {
                return NotFound("Producto no encontrado");
            }

            return Ok(productoDto);
        }
        [AllowAnonymous]  //Permite usar este action sin estar autenticado
        [HttpGet("nombre/{nombre}")]
        public async Task<IActionResult> Get(string nombre) {
            var producto = await _repository.GetProductoByNombreAsync(nombre);

            if (producto == null) {
                return NotFound("Producto no encontrado");
            }

            return Ok(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductoCreateDTO productoDto) {
            // var productoToCreate = new Producto();

            // productoToCreate.Nombre = productoDto.Nombre;
            // productoToCreate.Descripcion = productoDto.Descripcion;
            // productoToCreate.Precio = productoDto.Precio;

            //Usando Automapper
            var productoToCreate = _mapper.Map<Producto>(productoDto);

            await _repository.Add<Producto>(productoToCreate);
            if (await _repository.SaveAll()) {
                return Ok(productoToCreate);
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ProductoUpdateDTO productoDto, int id) {
            if (productoDto.Id != id) {
                return BadRequest("Los id (del producto y la url) no coinciden");
            }

            var productoToUpdate = await _repository.GetProductoByIdAsync(id);

            if (productoToUpdate == null) {
                return BadRequest();
            }

            // productoToUpdate.Descripcion = productoDto.Descripcion;
            // productoToUpdate.Precio = productoDto.Precio;

            //Usando Automapper
            _mapper.Map(productoDto, productoToUpdate);

            if (!await _repository.SaveAll()) {
                return NoContent();
            }
            return Ok(productoToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var productToDelete = await _repository.GetProductoByIdAsync(id);
            if (productToDelete == null) {
                return NotFound("Producto no encontrado");
            }
            _repository.Delete<Producto>(productToDelete);

            if (!await _repository.SaveAll())
                return BadRequest("No se pudo eliminar el producto");

            return Ok($"Producto {productToDelete.Nombre} con id: {productToDelete.Id} ha sido eliminado correctamente");
        }
    }
}