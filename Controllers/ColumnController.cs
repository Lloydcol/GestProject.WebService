using GestProject.WebService.Dto;
using GestProject.WebService.Dto.Forms;
using GestProject.WebService.Security;
using GestProject.WebService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestProject.WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : MasterController
    {
        private readonly ColumnService _service;

        public ColumnController(ColumnService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces("application/json", Type = null)]
        public IActionResult Get([FromQuery] int projectId)
        {
            return Ok(_service.GetAllByProject(projectId));
        }

        [HttpGet("{id}")]
        [Produces("application/json", Type = null)]
        public IActionResult GetById(int id)
        {
            ColumnDto dto = _service.GetById(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Post(ColumnAddFormDto form)
        {
            try
            {
                _service.Add(form);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Put(ColumnUpdateFormDto form)
        {
            try
            {
                _service.Update(form);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [ApiAuthorize("ADMIN")]
        [Produces("application/json", Type = null)]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
