using AutoMapper;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;
using FilmesAPI.Services;
using Moq;
using Xunit;

namespace FilmesAPI.Tests.ServicesTests
{
    public class AddressServiceTests
    {
        private readonly AddressService _addressService;
        private readonly Mock<IAddressRepository> _repositoryMock = new Mock<IAddressRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public AddressServiceTests()
        {
            _addressService = new AddressService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Theory(DisplayName = "AddressService.Create()")]
        [InlineData("Cidade teste", "Bairro teste", "Rua teste", 666, 12312311)]
        public void CreateAddress(string city, string district, string street, int number, int postalCode)
        {
            //Arrange
            AddressDTO addressDTO = new AddressDTO
            {
                City = city,
                District = district,
                Street = street,
                Number = number,
                PostalCode = postalCode
            };

            Address address = new Address
            {
                City = city,
                District = district,
                Street = street,
                Number = number,
                PostalCode = postalCode
            };

            ReadAddressDTO readAddressDTO = new ReadAddressDTO
            {
                City = city,
                District = district,
                Street = street,
                Number = number,
                PostalCode = postalCode
            };

            _mapperMock.Setup(mapper => mapper.Map<Address>(addressDTO)).Returns(address);

            _repositoryMock.Setup(repository => repository.Add(address));

            _mapperMock.Setup(mapper => mapper.Map<ReadAddressDTO>(address)).Returns(readAddressDTO);

            //Act
            ReadAddressDTO result = _addressService.Create(addressDTO);

            //Assert
            Assert.Equal(city, result.City);
            Assert.Equal(district, result.District);
            Assert.Equal(street, result.Street);
            Assert.Equal(number, result.Number);
            Assert.Equal(postalCode, result.PostalCode);
        }

        [Theory(DisplayName = "AddressService.GetAll()")]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(50)]
        public void GetAllAddresses(int listSize)
        {
            //Arrange
            Address address;
            ReadAddressDTO readAddressDTO;
            List<Address> addressList = new List<Address>();
            List<ReadAddressDTO> readAddressDTOList = new List<ReadAddressDTO>();

            for(int i = 0; i < listSize; i++)
            {
                address = new Address
                {
                    City = "Cidade",
                    District = "Bairro",
                    Street = "Rua",
                    Number = 12,
                    PostalCode = 12312312
                };

                readAddressDTO = new ReadAddressDTO
                {
                    City = "Cidade",
                    District = "Bairro",
                    Street = "Rua",
                    Number = 12,
                    PostalCode = 12312312
                };

                addressList.Add(address);
                readAddressDTOList.Add(readAddressDTO);
            }

            _repositoryMock.Setup(repository => repository.GetAll()).Returns(addressList);

            _mapperMock.Setup(mapper => mapper.Map<List<ReadAddressDTO>>(addressList)).Returns(readAddressDTOList);

            //Act
            List<ReadAddressDTO> result = _addressService.GetAll();

            //Assert
            Assert.Equal(listSize, result.Count);
        }

        [Theory(DisplayName = "AddressService.GetById()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetAddressById(int id)
        {
            //Arrange
            Address address = new Address
            {
                Id = id,
            };

            ReadAddressDTO readAddressDTO = new ReadAddressDTO
            {
                Id = id,
            };

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(address);

            _mapperMock.Setup(mapper => mapper.Map<ReadAddressDTO>(address)).Returns(readAddressDTO);

            //Act
            ReadAddressDTO result = _addressService.GetById(id);

            //Assert
            Assert.Equal(id, result.Id);
        }

        [Fact(DisplayName = "AddressService.GetById() throwing ElementNotFoundException.")]
        public void GetAddressByIdWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadAddressDTO result = _addressService.GetById(id);
            });
        }

        [Theory(DisplayName = "AddressService.Update()")]
        [InlineData(1, "cidade teste 1")]
        [InlineData(2, "cidade teste 2")]
        [InlineData(3, "cidade teste 3")]
        [InlineData(4, "cidade teste 4")]
        [InlineData(5, "cidade teste 5")]
        public void UpdateAddress(int id, string newCity)
        {
            //Arrange
            AddressDTO addressDTO = new AddressDTO
            {
                City = newCity
            };
            
            Address address = new Address
            {
                City = "cidade"
            };

            ReadAddressDTO readAddressDTO = new ReadAddressDTO
            {
                City = newCity
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(address);

            _mapperMock.Setup(mapper => mapper.Map(addressDTO, address));

            _repositoryMock.Setup(repository => repository.Update(address));

            _mapperMock.Setup(mapper => mapper.Map<ReadAddressDTO>(address))
                .Returns(readAddressDTO);

            //Act
            ReadAddressDTO result = _addressService.Update(id, addressDTO);

            //Assert
            Assert.Equal(newCity, result.City);
        }

        [Fact(DisplayName = "AddressService.Update() throwing ElementNotFoundException.")]
        public void UpdateAddressWithAnInexistentId()
        {
            //Arrange
            int id = 1;
            AddressDTO addressDTO = new AddressDTO();

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadAddressDTO result = _addressService.Update(id, addressDTO);
            });
        }

        [Theory(DisplayName = "AddressService.Delete()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void DeleteAddress(int id)
        {
            //Arrange
            Address address = new Address
            {
                City = "cidade",
                District = "bairro",
                Street = "rua",
                Number = 12,
                PostalCode = 1231231
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(address);

            _repositoryMock.Setup(repository => repository.Delete(address));

            //Act
            _addressService.Delete(id);
        }

        [Fact(DisplayName = "AddressService.Delete() throwing ElementNotFoundException.")]
        public void DeleteAddressWithAnInexistentId()
        {
            //Arrange
            int id = 1;
            Address address = new Address();

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                _addressService.Delete(id);
            });
        }
    }
}
