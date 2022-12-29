using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using FilmesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class AddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ReadAddressDTO Create(AddressDTO addressDTO)
        {
            Address address = _mapper.Map<Address>(addressDTO);

            _repository.Add(address);

            ReadAddressDTO readDTO = _mapper.Map<ReadAddressDTO>(address);

            return readDTO;
        }

        public List<ReadAddressDTO> GetAll()
        {
            List<Address> addressesList = _repository.GetAll();
            List<ReadAddressDTO> readDTOList = _mapper.Map<List<ReadAddressDTO>>(addressesList);

            return readDTOList;
        }

        public ReadAddressDTO GetById(int id)
        {
            Address address = _repository.GetById(id);
            
            if(address == null)
            {
                throw new ElementNotFoundException(ElementType.ADDRESS);
            }

            ReadAddressDTO readDTO = _mapper.Map<ReadAddressDTO>(address);

            return readDTO;
        }

        public ReadAddressDTO Update(int id, AddressDTO addressDTO)
        {
            Address address = _repository.GetById(id);

            if (address == null)
            {
                throw new ElementNotFoundException(ElementType.ADDRESS);
            }

            _mapper.Map(addressDTO, address);

            _repository.Update(address);

            ReadAddressDTO readDTO = _mapper.Map<ReadAddressDTO>(address);

            return readDTO;
        }

        public void Delete(int id)
        {
            Address address = _repository.GetById(id);

            if (address == null)
            {
                throw new ElementNotFoundException(ElementType.ADDRESS);
            }

            _repository.Delete(address);
        }
    }
}
