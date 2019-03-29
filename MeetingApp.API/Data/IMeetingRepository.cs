using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeetingApp.API.Helpers;
using MeetingApp.API.Models;

namespace MeetingApp.API.Data
{
    public interface IMeetingRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<Usuario>> GetUsers(UserParams userParams);
         Task<Usuario> GetUser(int id);
         Task<IEnumerable<Usuario>> TestUsuario(Expression<Func<Usuario,bool>> predicate);
         Task<Photos> GetPhoto(int id);
         Task<Photos> GetMainPhotoForUser(int userId);
    }
}