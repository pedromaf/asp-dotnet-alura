using System;
using System.Net;
using AutoMapper;
using FilmesAPI.Controllers;
using FilmesAPI.Data;
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
    public class AddressController : ControllerBase
    {
        private AddressService _addressService;

        public AddressController(AddressService service)
        {
            _addressService = service;
        }

        [HttpPost]
        public IActionResult CreateAddress([FromBody] AddressDTO addressDTO)
        {
            try
            {
                Address address = _addressService.Create(addressDTO);

                return CreatedAtAction(nameof(GetAddressById), new { Id = address.Id }, address);
            } 
            catch(DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch(DbUpdateException exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int Id)
        {
            try
            {
                ReadAddressDTO requestedAddress = _addressService.GetById(Id);
            
                return Ok(requestedAddress);
            }
            catch(ElementNotFoundException exc) { return this.HandleException(exc); }
            catch(ArgumentNullException exc) { return this.HandleException(exc); }
            catch(ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAddress(int Id, [FromBody] AddressDTO address)
        {
            try
            {
                Address updatedAddress = _addressService.Update(Id, address);

                return Ok(updatedAddress);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            try
            {
                _addressService.Delete(id);

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
