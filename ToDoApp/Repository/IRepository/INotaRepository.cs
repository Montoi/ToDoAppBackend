using System.Linq.Expressions;
using ToDoApp.Models;

namespace ToDoApp.Repository.IRepository
{
    public interface INotaRepository : IRepository<Note>
    {
        Task<Note> UpdateAsync(Note entity);

    }
}
