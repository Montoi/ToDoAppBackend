using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Repository.IRepository;

namespace ToDoApp.Repository
{
    public class UserRepository : Repository<User>, IUser
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

          
        public async Task<User> UpdateAsync(User entity)
        {
            entity.UpdatedDate = DateTime.Now; 
            _db.Users.Update(entity);
             _db.SaveChanges();
            return entity;
        }
    }
}
