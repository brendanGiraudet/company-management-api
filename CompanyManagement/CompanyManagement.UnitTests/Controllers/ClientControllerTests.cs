using CompanyManagement.API.Controllers;
using CompanyManagement.API.Models;
using CompanyManagement.API.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CompanyManagement.UnitTests.Controllers
{
    public class ClientControllerTests
    {
        private ClientController CreateClientController(IClientService clientService) => new ClientController(clientService);

        #region Create
        [Fact]
        public async Task ShouldHaveCreatedStatusCodeWhenCreate()
        {
            // Arrange
            var service = new Mock<IClientService>();
            service.Setup(s => s.CreateAsync(It.IsAny<IEnumerable<ClientModel>>()))
                .ReturnsAsync((StatusCodes.Status201Created, Enumerable.Empty<ClientModel>()));
            
            var controller = CreateClientController(service.Object);
            var clients = Enumerable.Empty<ClientModel>();
            var expectedStatusCode = StatusCodes.Status201Created;

            // Act
            var response = await controller.CreateAsync(clients);

            // Assert
            var result = response as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }
        
        [Fact]
        public async Task ShouldHaveInternalServerErrorStatusCodeWhenCreateWithClientServiceProbleme()
        {
            // Arrange
            var service = new Mock<IClientService>();
            service.Setup(s => s.CreateAsync(It.IsAny<IEnumerable<ClientModel>>()))
                .ReturnsAsync((StatusCodes.Status500InternalServerError, Enumerable.Empty<ClientModel>()));
            
            var controller = CreateClientController(service.Object);
            var clients = Enumerable.Empty<ClientModel>();
            var expectedStatusCode = StatusCodes.Status500InternalServerError;

            // Act
            var response = await controller.CreateAsync(clients);

            // Assert
            var result = response as ObjectResult;
            Assert.NotNull(result);
            Assert.Equal(expectedStatusCode, result.StatusCode);
        }
        #endregion
    }
}
