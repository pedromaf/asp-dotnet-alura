using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs.Manager;
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

        public Manager Create(ManagerDTO managerDTO)
        {
            Manager newManager = _mapper.Map<Manager>(managerDTO);

            _DbContext.Add(newManager);
            _DbContext.SaveChanges();

            return newManager;
        }

        public List<Manager> GetAll()
        {
            return _DbContext.Managers.ToList();
        }

        public ReadManagerDTO GetById(int id)
        {
            Manager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

            if(manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            ReadManagerDTO managerDTO = _mapper.Map<ReadManagerDTO>(manager);

            return managerDTO;
        }

        public Manager Update(int id, ManagerDTO managerDTO)
        {
            Manager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

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
            Manager manager = _DbContext.Managers.FirstOrDefault(manager => manager.Id == id);

            if (manager == null)
            {
                throw new ElementNotFoundException(ElementType.MANAGER);
            }

            _DbContext.Remove(manager);
            _DbContext.SaveChanges();
        }
    }
}
