using System.Linq.Expressions;
using ToDoApp.Models;

namespace ToDoApp.Repository.IRepository
{
    public interface IUser : IRepository<User>
    {
        Task<User> UpdateAsync(User entity);

    }
}
