using System;
using System.Threading.Tasks;
using MeetingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        public DataContext _context { get; set; }
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<bool> ExisteUsuario(string nombreUsuario)
        {
            if(await _context.Usuarios.AnyAsync(x=>x.NombreUsuario == nombreUsuario)){
                return true;
            }
            return false;
        }

        public async Task<Usuario> Login(string nombreUsuario, string password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x=>x.NombreUsuario == nombreUsuario);

            if(usuario == null){return null;}

            if(!VerifyPasswordHash(password, usuario.PasswordHash, usuario.PasswordSalt)){
                return null;
            }

            return usuario;
            
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {               
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<Usuario> Registrarse(Usuario usuario, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            usuario.PasswordHash = passwordHash;
            usuario.PasswordSalt = passwordSalt;

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }            
        }
    }
}