using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private ManagerService _managerService;

        public ManagerController(ManagerService service)
        {
            _managerService = service;
        }

        [HttpPost]
        public IActionResult CreateManager([FromBody] MTManagerDTO managerDTO)
        {
            try
            {
                MTManager newManager = _managerService.Create(managerDTO);

                return CreatedAtAction(nameof(GetManagerById), new { Id = newManager.Id }, newManager);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetManagerById(int id)
        {
            try
            {
                ReadMTManagerDTO readManagerDTO = _managerService.GetById(id);

                return Ok(readManagerDTO);
            }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        public IActionResult GetAllManagers()
        {
            try
            {
                List<MTManager> managersList = _managerService.GetAll();

                return Ok(managersList);
            }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateManager(int id, [FromBody] MTManagerDTO managerDTO)
        {
            try
            {
                MTManager updatedManager = _managerService.Update(id, managerDTO);

                return Ok(updatedManager);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteManager(int id)
        {
            try
            {
                _managerService.Delete(id);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }
    }
}
