using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;

namespace FilmesAPI.Services
{
    public class ManagerService
    {
        private readonly FilmesContext _DbContext;
        private readonly IMapper _mapper;

        public ManagerService(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
        }

        public ReadMTManagerDTO Create(MTManagerDTO managerDTO)
        {
            MTManager manager = _mapper.Map<MTManager>(managerDTO);

            _DbContext.Add(manager);
            _DbContext.SaveChanges();

            ReadMTManagerDTO readDTO = _mapper.Map<ReadMTManagerDTO>(manager);

            return readDTO;
        }

        public List<ReadMTManagerDTO> GetAll()
        {
            List<MTManager> managersList = _DbContext.Managers.ToList();
            List<ReadMTManagerDTO> readDTOList = _mapper.Map<List<ReadMTManagerDTO>>(managersList);

            return readDTOList;
        }

        public ReadMTManagerDTO GetById(int id)
        {
            MTManager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

            if(manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            ReadMTManagerDTO readDTO = _mapper.Map<ReadMTManagerDTO>(manager);

            return readDTO;
        }

        public ReadMTManagerDTO Update(int id, MTManagerDTO managerDTO)
        {
            MTManager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

            if(manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            _mapper.Map(managerDTO, manager);

            _DbContext.SaveChanges();

            ReadMTManagerDTO readDTO = _mapper.Map<ReadMTManagerDTO>(manager);

            return readDTO;
        }

        public void Delete(int id)
        {
            MTManager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            _DbContext.Remove(manager);
            _DbContext.SaveChanges();
        }
    }
}
