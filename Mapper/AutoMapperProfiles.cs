using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Dtos;
using APIWeb.Models;
using AutoMapper;

namespace APIWeb.Mapper {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            #region MapperProductos
            //Para el put
            CreateMap<ProductoCreateDTO, Producto>();

            //Para hacer el put
            CreateMap<ProductoUpdateDTO, Producto>();

            //Para el get por id
            CreateMap<Producto, ProductoListarDTO>();

            #endregion

            #region MapperUsuario
            CreateMap<UsuarioRegistroDto, Usuario>();
            CreateMap<UsuarioLoginDto, Usuario>();
            CreateMap<Usuario, UsuarioListDto>();
            #endregion
        }
    }
}