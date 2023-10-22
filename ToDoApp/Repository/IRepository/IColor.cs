using System.Linq.Expressions;
using ToDoApp.Models;

namespace ToDoApp.Repository.IRepository
{
    public interface IColor : IRepository<Color>
    {
        Task<Color> UpdateAsync(Color entity);

    }
}
