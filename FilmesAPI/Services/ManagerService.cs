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
        private FilmesContext _DbContext;
        private IMapper _mapper;

        public ManagerService(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
        }

        public MTManager Create(MTManagerDTO managerDTO)
        {
            MTManager newManager = _mapper.Map<MTManager>(managerDTO);

            _DbContext.Add(newManager);
            _DbContext.SaveChanges();

            return newManager;
        }

        public List<ReadMTManagerDTO> GetAll()
        {
            List<MTManager> managersList = _DbContext.Managers.ToList();
            List<ReadMTManagerDTO> managersDTOList = _mapper.Map<List<ReadMTManagerDTO>>(managersList);

            return managersDTOList;
        }

        public ReadMTManagerDTO GetById(int id)
        {
            MTManager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

            if(manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            ReadMTManagerDTO managerDTO = _mapper.Map<ReadMTManagerDTO>(manager);

            return managerDTO;
        }

        public MTManager Update(int id, MTManagerDTO managerDTO)
        {
            MTManager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

            if(manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            _mapper.Map(managerDTO, manager);

            _DbContext.SaveChanges();

            return manager;
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
