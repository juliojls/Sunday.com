using FoccoEmFrente.Kanban.Application.Entities;
using FoccoEmFrente.Kanban.Application.Enums;
using FoccoEmFrente.Kanban.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepositoy _activityRepositoy;

        public ActivityService(IActivityRepositoy activityRepositoy)
        {
            _activityRepositoy = activityRepositoy;

        }
        public async Task<Activity> AddAsync(Activity activity)
        {
            var newActivity = _activityRepositoy.Add(activity);

            await _activityRepositoy.UnitOfWork.CommitAsync();

            return newActivity;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<bool> ExistAsync(Guid id, Guid userId)
        {
            return await _activityRepositoy.ExistAsync(id, userId);
        }

        public async Task<IEnumerable<Activity>> GetAllAsync(Guid userId)
        {
            return await _activityRepositoy.GetAllAsync(userId);
        }

        public async Task<Activity> GetByIdAsync(Guid id, Guid userId)
        {
            return await _activityRepositoy.GetByIdAsync(id, userId);
        }

        public async Task<Activity> RemoveAsync(Activity activity)
        {
            var activityExists = await ExistAsync(activity.Id, activity.UserId);

            if (!activityExists)
                throw new Exception("Atividade não pode ser encontrada.");

            var oldActivity = _activityRepositoy.Remove(activity);

            await _activityRepositoy.UnitOfWork.CommitAsync();

            return oldActivity;
        }

        public async Task<Activity> RemoveAsync(Guid id, Guid userId)
        {
            var activityTobeRemove = await GetByIdAsync(id, userId);

            if (activityTobeRemove == null)
                throw new Exception("Atividade não pode ser encontrada.");

            var oldActivity = _activityRepositoy.Remove(activityTobeRemove);

            await _activityRepositoy.UnitOfWork.CommitAsync();

            return oldActivity;
        }

        public async Task<Activity> UpdateAsync(Activity activity)
        {
            var activityExists = await ExistAsync(activity.Id, activity.UserId);

            if (!activityExists)
                throw new Exception("Atividade não pode ser encontrada.");

            var updateActivity = _activityRepositoy.Update(activity);

            await _activityRepositoy.UnitOfWork.CommitAsync();

            return updateActivity;
        }

        public async Task<Activity> UpdateToTodoAsync(Guid id, Guid userId)
        {
            return await UpdateStatusAsync(id, userId, ActivityStatus.Todo);
        }

        public async Task<Activity> UpdateToDoingAsync(Guid id, Guid userId)
        {
            return await UpdateStatusAsync(id, userId, ActivityStatus.Doing);
        }

        public async Task<Activity> UpdateToDoneAsync(Guid id, Guid userId)
        {
            return await UpdateStatusAsync(id, userId, ActivityStatus.Done);
        }

        public async Task<Activity> UpdateStatusAsync(Guid id, Guid userId, ActivityStatus status)
        {
            var activity = await GetByIdAsync(id, userId);
            activity.Status = status;
            return await UpdateAsync(activity);
        }
    }
}
