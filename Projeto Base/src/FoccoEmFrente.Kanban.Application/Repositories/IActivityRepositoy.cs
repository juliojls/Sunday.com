using FoccoEmFrente.Kanban.Application.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Application.Repositories
{
   public interface IActivityRepositoy : IRepository<Activity>
    {
        Task<IEnumerable<Activity>> GetAllAsync(Guid userId);
        Task<Activity> GetByIdAsync(Guid id, Guid userId);
        Task<bool> ExistAsync(Guid id, Guid userId); 

        Activity Add(Activity activity);
        Activity Update(Activity activity);
        Activity Remove(Activity activity);
    }
}
