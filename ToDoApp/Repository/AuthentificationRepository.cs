using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Repository.IRepository;

namespace ToDoApp.Repository
{
    public class AuthentificationRepository : Repository<Authentification>, IAuthentication
    {
        private readonly ApplicationDbContext _db;

        public AuthentificationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public async Task<Authentification> UpdateAsync(Authentification entity)
        {
           
         
            _db.SaveChanges();
            return entity;
        }
    }
}
