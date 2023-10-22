using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Repository.IRepository;

namespace ToDoApp.Repository
{
    public class NoteRepository : Repository<Note>, INotaRepository
    {
        private readonly ApplicationDbContext _db;

        public NoteRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

          
        public async Task<Note> UpdateAsync(Note entity)
        {
            entity.Date = DateTime.Now; 
            _db.Notes.Update(entity);
             _db.SaveChanges();
            return entity;
        }
    }
}
