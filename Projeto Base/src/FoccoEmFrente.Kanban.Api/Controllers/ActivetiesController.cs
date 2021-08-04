using FoccoEmFrente.Kanban.Api.Controllers.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoccoEmFrente.Kanban.Api.Controllers
{
    [Route("api/activeties")]
    [ApiController]
    [ValidateModelState]
    [Authorize]
    public class ActivetiesController : ControllerBase
    {
       public IActionResult Get()
        {
            return Ok("ok");
        }

    }
}
   