using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class AddressService
    {
        private FilmesContext _DbContext;
        private IMapper _mapper;

        public AddressService(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
        }

        public Address Create(AddressDTO addressDTO)
        {
            Address address = _mapper.Map<Address>(addressDTO);

            _DbContext.Address.Add(address);
            _DbContext.SaveChanges();

            return address;
        }

        public List<ReadAddressDTO> GetAll()
        {
            List<Address> addressesList = _DbContext.Address.ToList();
            List<ReadAddressDTO> addressesDTOList = _mapper.Map<List<ReadAddressDTO>>(addressesList);

            return addressesDTOList;
        }

        public ReadAddressDTO GetById(int id)
        {
            Address address = _DbContext.Address.FirstOrDefault(m => m.Id == id);
            
            if(address == null)
            {
                throw new ElementNotFoundException(ElementType.ADDRESS);
            }

            ReadAddressDTO addressDTO = _mapper.Map<ReadAddressDTO>(address);

            return addressDTO;
        }

        public Address Update(int id, AddressDTO addressDTO)
        {
            Address address = _DbContext.Address.FirstOrDefault(m => m.Id == id);

            if (address == null)
            {
                throw new ElementNotFoundException(ElementType.ADDRESS);
            }

            _mapper.Map(addressDTO, address);

            _DbContext.SaveChanges();

            return address;
        }

        public void Delete(int id)
        {
            Address address = _DbContext.Address.FirstOrDefault(m => m.Id == id);

            if (address == null)
            {
                throw new ElementNotFoundException(ElementType.ADDRESS);
            }

            _DbContext.Remove(address);
            _DbContext.SaveChanges();
        }
    }
}
