using FoccoEmFrente.Kanban.Application.Context;
using FoccoEmFrente.Kanban.Application.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Application.Repositories
{
    public class ActivityRepositoy : IActivityRepositoy
    {
        protected readonly KanbanContext Dbcontext;
        protected readonly DbSet<Activity> DbSet;

        public IUnitOfWork UnitOfWork => Dbcontext;

        public ActivityRepositoy(KanbanContext context)
        {
            Dbcontext = context;
            DbSet = Dbcontext.Set<Activity>();
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(Guid userId)
        {
            return await DbSet
                .Where(activities => activities.UserId == userId)
                .ToListAsync();
        }

        public Activity Add(Activity activity)
        {
           var entry = DbSet.Add(activity);

            

            return entry.Entity;
        }

        public void Dispose()
        {
            Dbcontext.Dispose();
        }

        public async Task<Activity> GetByIdAsync(Guid id, Guid userId)
        {
            return await DbSet
                .Where(activities => activities.UserId == userId && activities.Id == id)
                .FirstOrDefaultAsync();
        }

        public Activity Update(Activity activity)
        {
            var entry = DbSet.Update(activity); 

            

            return entry.Entity;


        }

        public Activity Remove(Activity activity)
        {
            var entry = DbSet.Remove(activity);

            

            return entry.Entity;

        }

        public async Task<bool> ExistAsync(Guid id, Guid userId)
        {
            return await DbSet.
                Where(activities => activities.UserId == userId && activities.Id == id)
                .AnyAsync();
        }
    }
  
}
