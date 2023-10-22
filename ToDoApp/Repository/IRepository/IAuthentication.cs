using ToDoApp.Models;

namespace ToDoApp.Repository.IRepository
{
    public interface IAuthentication : IRepository<Authentification>
    {
        Task<Authentification> UpdateAsync(Authentification entity);
    }
}
