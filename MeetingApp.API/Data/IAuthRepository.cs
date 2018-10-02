using System.Threading.Tasks;
using MeetingApp.API.Models;

namespace MeetingApp.API.Data
{
    public interface IAuthRepository
    {
         Task<Usuario> Registrarse(Usuario usuario, string password);
         Task<Usuario> Login(string nombreUsuario, string password);
         Task<bool> ExisteUsuario(string nombreUsuario);
    }
}