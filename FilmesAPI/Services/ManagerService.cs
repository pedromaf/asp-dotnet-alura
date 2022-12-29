using AutoMapper;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using FilmesAPI.Repositories.Interfaces;

namespace FilmesAPI.Services
{
    public class ManagerService
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public ManagerService(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ReadMTManagerDTO Create(MTManagerDTO managerDTO)
        {
            MTManager manager = _mapper.Map<MTManager>(managerDTO);

            _repository.Add(manager);

            ReadMTManagerDTO readDTO = _mapper.Map<ReadMTManagerDTO>(manager);

            return readDTO;
        }

        public List<ReadMTManagerDTO> GetAll()
        {
            List<MTManager> managersList = _repository.GetAll();
            List<ReadMTManagerDTO> readDTOList = _mapper.Map<List<ReadMTManagerDTO>>(managersList);

            return readDTOList;
        }

        public ReadMTManagerDTO GetById(int id)
        {
            MTManager manager = _repository.GetById(id);

            if(manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            ReadMTManagerDTO readDTO = _mapper.Map<ReadMTManagerDTO>(manager);

            return readDTO;
        }

        public ReadMTManagerDTO Update(int id, MTManagerDTO managerDTO)
        {
            MTManager manager = _repository.GetById(id);

            if(manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            _mapper.Map(managerDTO, manager);

            _repository.Update(manager);

            ReadMTManagerDTO readDTO = _mapper.Map<ReadMTManagerDTO>(manager);

            return readDTO;
        }

        public void Delete(int id)
        {
            MTManager manager = _repository.GetById(id);

            if (manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            _repository.Delete(manager);
        }
    }
}
