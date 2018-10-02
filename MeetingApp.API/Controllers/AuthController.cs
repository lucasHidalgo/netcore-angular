using System.Threading.Tasks;
using MeetingApp.API.Data;
using MeetingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registrarse(string nombreUsuario, string password)
        {
            //validate request
            nombreUsuario = nombreUsuario.ToLower();

            if(await _repo.ExisteUsuario(nombreUsuario)){
                return BadRequest("Ya existe nombre usuario");
            }
            var usuarioCrear = new Usuario{
                NombreUsuario = nombreUsuario
            };

            var usuarioCreado = await _repo.Registrarse(usuarioCrear, password);

            return StatusCode(201);
        }
    }
}