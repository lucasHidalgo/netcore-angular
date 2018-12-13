using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeetingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.API.Data
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly DataContext _context;
        public MeetingRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Usuario> GetUser(int id)
        {
            var user = await _context.Usuarios.Include(p=>p.Photos).FirstOrDefaultAsync(u=>u.Id == id);

            return user;
        }

        public async Task<IEnumerable<Usuario>> GetUsers()
        {
            return await _context.Usuarios.Include(p=>p.Photos).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<Usuario>> TestUsuario(Expression<Func<Usuario,bool>> predicate)
        {
            return await _context.Usuarios.Where(predicate).ToListAsync();
        }

        public async Task<Photos> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p=> p.Id == id);
            return photo;
        }

        public async Task<Photos> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.FirstOrDefaultAsync(u => u.UsuarioId == userId && u.IsMain);
            
        }
    }
}