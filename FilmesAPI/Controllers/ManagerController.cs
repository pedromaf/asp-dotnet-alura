using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerService _managerService;

        public ManagerController(ManagerService service)
        {
            _managerService = service;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateManager([FromBody] MTManagerDTO managerDTO)
        {
            try
            {
                ReadMTManagerDTO newManager = _managerService.Create(managerDTO);

                return CreatedAtAction(nameof(GetManagerById), new { newManager.Id }, newManager);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetManagerById(int id)
        {
            try
            {
                ReadMTManagerDTO manager = _managerService.GetById(id);

                return Ok(manager);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllManagers()
        {
            try
            {
                List<ReadMTManagerDTO> managersList = _managerService.GetAll();

                return Ok(managersList);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateManager(int id, [FromBody] MTManagerDTO managerDTO)
        {
            try
            {
                ReadMTManagerDTO updatedManager = _managerService.Update(id, managerDTO);

                return Ok(updatedManager);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteManager(int id)
        {
            try
            {
                _managerService.Delete(id);

                return NoContent();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
