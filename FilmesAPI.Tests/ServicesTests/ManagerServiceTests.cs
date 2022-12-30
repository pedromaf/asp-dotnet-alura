using AutoMapper;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories;
using FilmesAPI.Repositories.Interfaces;
using FilmesAPI.Services;
using Moq;
using Xunit;

namespace FilmesAPI.Tests.ServicesTests
{
    public class ManagerServiceTests
    {
        private readonly ManagerService _managerService;
        private readonly Mock<IManagerRepository> _repositoryMock = new Mock<IManagerRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public ManagerServiceTests()
        {
            _managerService = new ManagerService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Theory(DisplayName = "ManagerService.Create()")]
        [InlineData("Gerente teste 1")]
        [InlineData("Gerente teste 2")]
        [InlineData("Gerente teste 3")]
        [InlineData("Gerente teste 4")]
        [InlineData("Gerente teste 5")]
        public void CreateManager(string name)
        {
            //Arrange
            MTManagerDTO managerDTO = new MTManagerDTO
            {
                Name = name
            };

            MTManager manager = new MTManager
            {
                Name = name
            };

            ReadMTManagerDTO readManagerDTO = new ReadMTManagerDTO
            {
                Name = name
            };

            _mapperMock.Setup(mapper => mapper.Map<MTManager>(managerDTO)).Returns(manager);

            _repositoryMock.Setup(repository => repository.Add(manager));

            _mapperMock.Setup(mapper => mapper.Map<ReadMTManagerDTO>(manager)).Returns(readManagerDTO);

            //Act
            ReadMTManagerDTO result = _managerService.Create(managerDTO);

            //Assert
            Assert.Equal(name, result.Name);
        }

        [Theory(DisplayName = "ManagerService.GetAll()")]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        public void GetAllManagers(int listSize)
        {
            //Arrange
            MTManager manager;
            ReadMTManagerDTO readManagerDTO;
            List<MTManager> managerList = new List<MTManager>();
            List<ReadMTManagerDTO> readManagerDTOList = new List<ReadMTManagerDTO>();

            for (int i = 0; i < listSize; i++)
            {
                manager = new MTManager
                {
                    Name = $"Gerente {i}"
                };

                readManagerDTO = new ReadMTManagerDTO
                {
                    Name = $"Gerente {i}"
                };

                managerList.Add(manager);
                readManagerDTOList.Add(readManagerDTO);
            }

            _repositoryMock.Setup(repository => repository.GetAll()).Returns(managerList);

            _mapperMock.Setup(mapper => mapper.Map<List<ReadMTManagerDTO>>(managerList)).Returns(readManagerDTOList);

            //Act
            List<ReadMTManagerDTO> result = _managerService.GetAll();

            //Assert
            Assert.Equal(listSize, result.Count);
        }

        [Theory(DisplayName = "ManagerService.GetById()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetManagerById(int id)
        {
            //Arrange
            MTManager manager = new MTManager
            {
                Id = id
            };

            ReadMTManagerDTO readManagerDTO = new ReadMTManagerDTO
            {
                Id = id
            };

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(manager);

            _mapperMock.Setup(mapper => mapper.Map<ReadMTManagerDTO>(manager)).Returns(readManagerDTO);

            //Act
            ReadMTManagerDTO result = _managerService.GetById(id);

            //Assert
            Assert.Equal(id, result.Id);
        }

        [Fact(DisplayName = "ManagerService.GetById() throwing ElementNotFoundException")]
        public void GetManagerByIdWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadMTManagerDTO result = _managerService.GetById(id);
            });
        }

        [Theory(DisplayName = "ManagerService.Update()")]
        [InlineData(1, "Gerente Teste 1")]
        [InlineData(2, "Gerente Teste 2")]
        [InlineData(3, "Gerente Teste 3")]
        [InlineData(4, "Gerente Teste 4")]
        [InlineData(5, "Gerente Teste 5")]
        public void UpdateManager(int id, string newName)
        {
            //Arrange
            MTManagerDTO managerDTO = new MTManagerDTO
            {
                Name = newName,
            };

            MTManager manager = new MTManager
            {
                Id = id,
                Name = "Gerente"
            };

            ReadMTManagerDTO readManagerDTO = new ReadMTManagerDTO
            {
                Id = id,
                Name = newName
            };

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(manager);

            _mapperMock.Setup(mapper => mapper.Map(managerDTO, manager));

            _repositoryMock.Setup(repository => repository.Update(manager));

            _mapperMock.Setup(mapper => mapper.Map<ReadMTManagerDTO>(manager)).Returns(readManagerDTO);

            //Act
            ReadMTManagerDTO result = _managerService.Update(id, managerDTO);

            //Arrange
            Assert.Equal(newName, result.Name);
        }

        [Fact(DisplayName = "ManagerService.Update() throwing ElementNotFoundException.")]
        public void UpdateManagerWithAnInexistentId()
        {
            //Arrange
            int id = 1;
            MTManagerDTO managerDTO = new MTManagerDTO();

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Arrange
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadMTManagerDTO result = _managerService.Update(id, managerDTO);
            });
        }

        [Theory(DisplayName = "ManagerService.Delete()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void DeleteManager(int id)
        {
            //Arrange
            MTManager manager = new MTManager
            {
                Id = id
            };

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(manager);

            _repositoryMock.Setup(repository => repository.Delete(manager));

            //Act
            _managerService.Delete(id);
        }

        [Fact(DisplayName = "ManagerService.Delete()")]
        public void DeleteManagerWithAnInexistentId()
        {
            //Arrange
            int id = 1;
            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                _managerService.Delete(id);
            });
        }
    }
}
