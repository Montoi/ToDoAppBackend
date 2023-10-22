using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Repository.IRepository;

namespace ToDoApp.Repository
{
    public class ColorRepository : Repository<Color>, IColor
    {
        private readonly ApplicationDbContext _db;

        public ColorRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

          
        public async Task<Color> UpdateAsync(Color entity)
        {
            entity.UpdatedDate = DateTime.Now; 
            _db.Colors.Update(entity);
             _db.SaveChanges();
            return entity;
        }
    }
}
