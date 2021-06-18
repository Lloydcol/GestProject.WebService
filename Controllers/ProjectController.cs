using GestProject.WebService.Dto;
using GestProject.WebService.Dto.Forms;
using GestProject.WebService.Security;
using GestProject.WebService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestProject.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : MasterController
    {
        private readonly ProjectService _service;

        public ProjectController(ProjectService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Get()
        {
            //if(userId == null)
            //    return Ok(_service.GetAllProject());
            //else
            return Ok(_service.GetAllProjectByUser(UserId));
        }

        [HttpGet("{id}")]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Get(int id)
        {
            ProjectDto dto = _service.FindById(id);
            if (dto == null)
                return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Post([FromBody] ProjectAddFormDto form)
        {
            _service.AddProject(form, UserId);
            return NoContent();
        }

        [HttpPut]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Put([FromBody] ProjectUpdateFormDto form)
        {
            _service.UpdateProject(form);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Delete(int id)
        {
            _service.DeleteProject(id);
            return NoContent();
        }
    }
}
