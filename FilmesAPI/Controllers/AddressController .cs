using System;
using System.Net;
using AutoMapper;
using FilmesAPI.Controllers;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs.Address;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private FilmesContext _DbContext;
        private AddressService _addressService;
        private IMapper _mapper;

        public AddressController(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
            _addressService = new AddressService(_DbContext, mapper);
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressDTO addressDTO)
        {
            try
            {
                Address address = _addressService.Create(addressDTO);

                return CreatedAtAction(nameof(GetAddressById), new { Id = address.Id }, address);
            } 
            catch(DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch(DbUpdateException exc) { return GetErrorResult(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int Id)
        {
            try
            {
                ReadAddressDTO requestedAddress = _addressService.GetAddressById(Id);
            
                return Ok(requestedAddress);
            }
            catch(ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch(ArgumentNullException exc) { return GetErrorResult(exc); }
            catch(ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int Id, [FromBody] AddressDTO address)
        {
            try
            {
                Address updatedAddress = _addressService.Update(Id, address);

                return Ok(updatedAddress);
            }
            catch (DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch (DbUpdateException exc) { return GetErrorResult(exc); }
            catch (ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            try
            {
                _addressService.Delete(id);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch (DbUpdateException exc) { return GetErrorResult(exc); }
            catch (ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        private IActionResult GetErrorResult(Exception exc)
        {
            switch(exc.GetType().ToString())
            {
                case "FilmesAPI.Exceptions.ElementNotFoundException":
                    return NotFound(exc.Message);
                default:
                    return StatusCode(500, exc.Message);
            }
        } 
    }
}
