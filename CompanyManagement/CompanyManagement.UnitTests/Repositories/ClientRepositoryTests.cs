using CompanyManagement.API.Models;
using CompanyManagement.API.Repositories;
using CompanyManagement.API.Repositories.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CompanyManagement.UnitTests.Repositories
{
    public class ClientRepositoryTests
    {
        private IClientRepository CreateClientRepository()
        {
            _databaseContext.Database.EnsureDeleted();
            _databaseContext.Database.EnsureCreated();

            return new ClientRepository(_databaseContext);
        }

        private DatabaseContext _databaseContext;

        public ClientRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
               .UseInMemoryDatabase(databaseName: "TestDatabase")
               .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
               .Options;

            _databaseContext = new DatabaseContext(options);
        }

        #region Create
        [Fact]
        public async Task ShouldHaveCreatedStatusCodeWhenCreate()
        {
            // Arrange
            var service = CreateClientRepository();
            var firstClientName = "test";
            var secondClientName = "test2";
            var clients = new List<ClientModel>() {
                new ClientModel
                {
                    Email = firstClientName,
                    Name = firstClientName,
                    PhoneNumber = firstClientName,
                },
                new ClientModel
                {
                    Email = secondClientName,
                    Name = secondClientName,
                    PhoneNumber = secondClientName,
                }
            };

            // Act
            var response = await service.CreateAsync(clients);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.statusCode);

            var firstClient = await _databaseContext.Clients.FirstOrDefaultAsync(c => c.Name == firstClientName);
            Assert.NotNull(firstClient);
            Assert.Equal(firstClientName, firstClient.Name);

            var secondClient = await _databaseContext.Clients.FirstOrDefaultAsync(c => c.Name == secondClientName);
            Assert.NotNull(secondClient);
            Assert.Equal(secondClientName, secondClient.Name);
        }

        [Fact]
        public async Task ShouldHaveInternalErrorStatusCodeWhenCreateWithExistedClient()
        {
            // Arrange
            var service = CreateClientRepository();

            var client = new ClientModel
            {
                Email = "test",
                Name = "test",
                PhoneNumber = "test",
            };

            _databaseContext.Clients.Add(client);
            await _databaseContext.SaveChangesAsync();

            var clients = new List<ClientModel>() { client };

            // Act
            var response = await service.CreateAsync(clients);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.statusCode);
        }
        
        [Fact]
        public async Task ShouldHaveInternalErrorStatusCodeWhenCreateWithMissingpropertyClient()
        {
            // Arrange
            var service = CreateClientRepository();

            var client = new ClientModel
            {
                Email = "test",
            };

            var clients = new List<ClientModel>() { client };

            // Act
            var response = await service.CreateAsync(clients);

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, response.statusCode);
        }

        [Fact]
        public async Task ShouldHaveCreatedStatusCodeWhenCreateWithAddress()
        {
            // Arrange
            var service = CreateClientRepository();

            var addressTypeId = Guid.NewGuid().ToString();
            
            _databaseContext.AddressTypes.Add(new AddressTypeModel
            {
                Id = addressTypeId,
                Label = "Facturation"
            });
            await _databaseContext.SaveChangesAsync();

            var firstClientName = "test";
            var addressClientName = "testaddress";
            var clients = new List<ClientModel>() {
                new ClientModel
                {
                    Email = firstClientName,
                    Name = firstClientName,
                    PhoneNumber = firstClientName,
                    Addresses = new HashSet<AddressModel>()
                    {
                        new AddressModel
                        {
                            City = addressClientName,
                            Street = addressClientName,
                            ZipCode = addressClientName,
                            AddressTypeId = addressTypeId
                        }
                    }
                },
            };

            // Act
            var response = await service.CreateAsync(clients);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.statusCode);

            var firstClient = await _databaseContext.Clients.FirstOrDefaultAsync(c => c.Name == firstClientName);
            Assert.NotNull(firstClient);
            Assert.Equal(firstClientName, firstClient.Name);

            var address = await _databaseContext.Addresses.FirstOrDefaultAsync(a => a.ClientId == firstClient.Id);
            Assert.NotNull(address);
            Assert.Equal(addressClientName, address.City);
        }
        
        [Fact]
        public async Task ShouldHaveCreatedStatusCodeWhenCreateWithDuplicateClient()
        {
            // Arrange
            var service = CreateClientRepository();

            var addressTypeId = Guid.NewGuid().ToString();
            
            _databaseContext.AddressTypes.Add(new AddressTypeModel
            {
                Id = addressTypeId,
                Label = "Facturation"
            });

            await _databaseContext.SaveChangesAsync();

            var firstClientName = "test";
            var addressClientName = "testaddress";
            var clients = new List<ClientModel>() {
                new ClientModel
                {
                    Email = firstClientName,
                    Name = firstClientName,
                    PhoneNumber = firstClientName,
                    Addresses = new HashSet<AddressModel>()
                    {
                        new AddressModel
                        {
                            City = addressClientName,
                            Street = addressClientName,
                            ZipCode = addressClientName,
                            AddressTypeId = addressTypeId
                        }
                    }
                },
                new ClientModel
                {
                    Email = firstClientName,
                    Name = firstClientName,
                    PhoneNumber = firstClientName,
                    Addresses = new HashSet<AddressModel>()
                    {
                        new AddressModel
                        {
                            City = addressClientName,
                            Street = addressClientName,
                            ZipCode = addressClientName,
                            AddressTypeId = addressTypeId
                        }
                    }
                },
            };

            // Act
            var response = await service.CreateAsync(clients);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.statusCode);

            var firstClient = await _databaseContext.Clients.FirstOrDefaultAsync(c => c.Name == firstClientName);
            Assert.NotNull(firstClient);
            Assert.Equal(firstClientName, firstClient.Name);

            var address = await _databaseContext.Addresses.FirstOrDefaultAsync(a => a.ClientId == firstClient.Id);
            Assert.NotNull(address);
            Assert.Equal(addressClientName, address.City);

            Assert.Equal(1, _databaseContext.Clients.Where(c => c.Name == firstClientName).Count());
        }
        
        [Fact]
        public async Task ShouldHaveCreatedStatusCodeWhenCreateWithExistedClient()
        {
            // Arrange
            var service = CreateClientRepository();

            var addressTypeId = Guid.NewGuid().ToString();
            
            _databaseContext.AddressTypes.Add(new AddressTypeModel
            {
                Id = addressTypeId,
                Label = "Facturation"
            });

            var firstClientName = "test";
            var addressClientName = "testaddress";

            _databaseContext.Clients.Add(new ClientModel
            {
                Email = firstClientName,
                Name = firstClientName,
                PhoneNumber = firstClientName,
                Addresses = new HashSet<AddressModel>()
                    {
                        new AddressModel
                        {
                            City = addressClientName,
                            Street = addressClientName,
                            ZipCode = addressClientName,
                            AddressTypeId = addressTypeId
                        }
                    }
            });

            await _databaseContext.SaveChangesAsync();

            var clients = new List<ClientModel>() { 
                new ClientModel
                {
                    Email = firstClientName,
                    Name = firstClientName,
                    PhoneNumber = firstClientName,
                    Addresses = new HashSet<AddressModel>()
                    {
                        new AddressModel
                        {
                            City = addressClientName,
                            Street = addressClientName,
                            ZipCode = addressClientName,
                            AddressTypeId = addressTypeId
                        }
                    }
                }
            };

            // Act
            var response = await service.CreateAsync(clients);

            // Assert
            Assert.Equal(StatusCodes.Status201Created, response.statusCode);

            var firstClient = await _databaseContext.Clients.FirstOrDefaultAsync(c => c.Name == firstClientName);
            Assert.NotNull(firstClient);
            Assert.Equal(firstClientName, firstClient.Name);

            var address = await _databaseContext.Addresses.FirstOrDefaultAsync(a => a.ClientId == firstClient.Id);
            Assert.NotNull(address);
            Assert.Equal(addressClientName, address.City);

            Assert.Equal(1, _databaseContext.Clients.Where(c => c.Name == firstClientName).Count());
        }
        #endregion
    }
}
