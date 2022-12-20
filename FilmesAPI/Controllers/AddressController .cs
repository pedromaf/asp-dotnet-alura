using System;
using System.Data;
using System.Net;
using AutoMapper;
using FilmesAPI.Controllers;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly AddressService _addressService;

        public AddressController(AddressService service)
        {
            _addressService = service;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateAddress([FromBody] AddressDTO addressDTO)
        {
            try
            {
                ReadAddressDTO newAddress = _addressService.Create(addressDTO);

                return CreatedAtAction(nameof(GetAddressById), new { newAddress.Id }, newAddress);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular-user")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                List<ReadAddressDTO> addressesList = _addressService.GetAll();

                return Ok(addressesList);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin, regular-user")]
        public IActionResult GetAddressById(int Id)
        {
            try
            {
                ReadAddressDTO requestedAddress = _addressService.GetById(Id);
            
                return Ok(requestedAddress);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateAddress(int Id, [FromBody] AddressDTO address)
        {
            try
            {
                ReadAddressDTO updatedAddress = _addressService.Update(Id, address);

                return Ok(updatedAddress);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteAddress(int id)
        {
            try
            {
                _addressService.Delete(id);

                return NoContent();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
