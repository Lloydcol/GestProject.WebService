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
    public class TicketController : MasterController
    {
        private readonly TicketService _service;

        public TicketController(TicketService service)
        {
            _service = service;
        }

        [HttpGet]
        [Produces("application/json", Type = null)]
        public IActionResult Get([FromQuery] int columnId)
        {
            return Ok(_service.GetAllByColumn(columnId));
        }

        [HttpGet("{id}")]
        [Produces("application/json", Type = null)]
        public IActionResult GetById(int id)
        {
            TicketDto dto = _service.GetById(id);
            if (dto is null) return NotFound();
            return Ok(dto);
        }

        [HttpPost]
        [Produces("application/json", Type = null)]
        public IActionResult Post(TicketAddFormDto form)
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
        public IActionResult Put(TicketUpdateFormDto form)
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
