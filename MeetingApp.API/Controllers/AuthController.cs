using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MeetingApp.API.Data;
using MeetingApp.API.Dtos;
using MeetingApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;

namespace MeetingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _config = config;
            _repo = repo;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrarse(UserForRegisterDto usuario)
        {
            //validate request            
            usuario.NombreUsuario = usuario.NombreUsuario.ToLower();

            if (await _repo.ExisteUsuario(usuario.NombreUsuario))
            {
                return BadRequest("Ya existe nombre usuario");
            }
            var usuarioCrear = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario
            };

            var usuarioCreado = await _repo.Registrarse(usuarioCrear, usuario.Password);

            return StatusCode(201);
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto usuario)
        {
            //chequeo que existe usuario
            var userFromRepo = await _repo.Login(usuario.NombreUsuario.ToLower(), usuario.Password);

            if (userFromRepo == null)
                return Unauthorized();

            //creo los claims, valores que tendra el token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.NombreUsuario)
            };
            //security key 
            var key = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes( _config.GetSection("AppSettings:Token").Value));
            //firmar el token con el security key y encriptar el key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //crear el token, pasar los claims, expiracion y credenciales
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            
            
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var user = _mapper.Map<UserForListDto>(userFromRepo);
            return Ok(new {
                token = tokenHandler.WriteToken(token),
                user
            });

        }
    }
}