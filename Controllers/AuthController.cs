using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIWeb.Data.Interfaces;
using APIWeb.Dtos;
using APIWeb.Models;
using APIWeb.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly IAuthRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repository, ITokenService tokenService, IMapper mapper) {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UsuarioRegistroDto usuarioDto) {
            if (string.IsNullOrEmpty(usuarioDto.CorreoElectronico) ||
                string.IsNullOrEmpty(usuarioDto.Password)) {
                return BadRequest("Correo Electronico o contraseña vacios");
            }

            usuarioDto.CorreoElectronico = usuarioDto.CorreoElectronico.ToLower();
            if (await _repository.ExisteUsuario(usuarioDto.CorreoElectronico)) {
                return BadRequest("Correo Electrónico en uso, ya registrado");
            }
            var usuarioNuevo = _mapper.Map<Usuario>(usuarioDto);
            var usuarioCreado = await _repository.Registrar(usuarioNuevo, usuarioDto.Password);
            var usuarioCreadoDto = _mapper.Map<UsuarioListDto>(usuarioCreado);
            return Ok(usuarioCreadoDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLogin) {
            Usuario? usuarioFromRepo = null;
            if (!string.IsNullOrEmpty(usuarioLogin.CorreoElectronico) &&
                !string.IsNullOrEmpty(usuarioLogin.Password)) {
                usuarioFromRepo = await _repository.Login(usuarioLogin.CorreoElectronico, usuarioLogin.Password);
            }

            if (usuarioFromRepo == null) {
                return Unauthorized();
            }

            var usuario = _mapper.Map<UsuarioListDto>(usuarioFromRepo);

            var token = _tokenService.CreateToken(usuarioFromRepo);

            return Ok(new {
                token,
                usuario
            });
        }
    }
}