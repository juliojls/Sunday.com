using FoccoEmFrente.Kanban.Api.Controllers.Attributes;
using FoccoEmFrente.Kanban.Application.Entities;
using FoccoEmFrente.Kanban.Application.Repositories;
using FoccoEmFrente.Kanban.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoccoEmFrente.Kanban.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModelState]
    [Authorize]
    public class ActivtiesController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IActivityService _activityService;
        public ActivtiesController(IActivityService activityService, UserManager<IdentityUser> userManager)
        {
            _activityService = activityService;
            _userManager = userManager;
        }


        protected Guid UserId => Guid.Parse(_userManager.GetUserId(User));

        [HttpGet] 
          
        public async Task<IActionResult> ListarAsync()
        {
            var atividades = await _activityService.GetAllAsync(UserId);

            return Ok(atividades);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> SelecionarAsync(Guid id)
        {
            var atividades = await _activityService.GetByIdAsync(id, UserId);
            if (atividades == null)
                return NotFound();

            return Ok(atividades);
        }

        [HttpPut]
        public async Task<IActionResult> Alterar(Activity activity)
        {
            activity.UserId = UserId;

            var dbActivity = await _activityService.UpdateAsync(activity);

            return Ok(dbActivity);

        }

        [HttpPost]
        public async Task<IActionResult> Inserir(Activity activity)
        {
            activity.UserId = UserId;

            var newActivity = await _activityService.AddAsync(activity);

            return Ok(newActivity);  

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Activity activity)
        {
            activity.UserId = UserId;

            var oldActivity = await _activityService.RemoveAsync(activity);

            return Ok(oldActivity);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var oldActivity = await _activityService.RemoveAsync(id,UserId);

            return Ok(oldActivity);

        }

        [HttpPut("{id}/todo")]
        public async Task<IActionResult> AtualizarStatusParaTodo(Guid id)
        {
            var activity = await _activityService.UpdateToTodoAsync(id, UserId);

            return Ok(activity);
        }

        [HttpPut("{id}/doing")]
        public async Task<IActionResult> AtualizarStatusParaDoing(Guid id)
        {
            var activity = await _activityService.UpdateToDoingAsync(id, UserId);

            return Ok(activity);
        }

        [HttpPut("{id}/done")]
        public async Task<IActionResult> AtualizarStatusParaDone(Guid id)
        {
            var activity = await _activityService.UpdateToDoneAsync(id, UserId);

            return Ok(activity);
        }

    } 
}
