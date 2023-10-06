using CompanyManagement.API.Models;
using CompanyManagement.API.Repositories;
using CompanyManagement.API.Repositories.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CompanyManagement.UnitTests.Repositories;

public class ServiceRepositoryTests
{
    private DatabaseContext _databaseContext;

    private IServiceRepository CreateServiceRepository()
    {
        _databaseContext.Database.EnsureDeleted();
        _databaseContext.Database.EnsureCreated();

        return new ServiceRepository(_databaseContext);
    }

    public ServiceRepositoryTests()
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
        var service = CreateServiceRepository();
        var firstServiceName = "test";
        var secondServiceName = "test2";
        var services = new List<ServiceModel>() {
                new ServiceModel
                {
                    Name = firstServiceName,
                    Price = 10,
                    Unit = "unité"
                },
                new ServiceModel
                {
                    Name = secondServiceName,
                    Price = 10,
                    Unit = "unité"
                }
            };

        // Act
        var response = await service.CreateAsync(services);

        // Assert
        Assert.Equal(StatusCodes.Status201Created, response.statusCode);

        var firstService = await _databaseContext.Services.FirstOrDefaultAsync(c => c.Name == firstServiceName);
        Assert.NotNull(firstService);
        Assert.Equal(firstServiceName, firstService.Name);

        var secondService = await _databaseContext.Services.FirstOrDefaultAsync(c => c.Name == secondServiceName);
        Assert.NotNull(secondService);
        Assert.Equal(secondServiceName, secondService.Name);
    }

    [Fact]
    public async Task ShouldHaveInternalErrorStatusCodeWhenCreateWithMissingpropertyService()
    {
        // Arrange
        var repository = CreateServiceRepository();

        var service = new ServiceModel
        {
            Name = "test",
        };

        var services = new List<ServiceModel>() { service };

        // Act
        var response = await repository.CreateAsync(services);

        // Assert
        Assert.Equal(StatusCodes.Status500InternalServerError, response.statusCode);
    }

    [Fact]
    public async Task ShouldHaveCreatedStatusCodeWhenCreateWithDuplicateService()
    {
        // Arrange
        var repository = CreateServiceRepository();

        var firstServiceName = "test";
        _databaseContext.Services.Add(new ServiceModel
        {
            Name = firstServiceName,
            Price = 10,
            Unit = "unit"
        });

        await _databaseContext.SaveChangesAsync();

        var services = new List<ServiceModel>() {
                new ServiceModel
                {
                    Name = firstServiceName,
                    Price = 10,
                    Unit = "unit"
                }
            };

        // Act
        var response = await repository.CreateAsync(services);

        // Assert
        Assert.Equal(StatusCodes.Status201Created, response.statusCode);

        var firstservice = await _databaseContext.Services.FirstOrDefaultAsync(c => c.Name == firstServiceName);
        Assert.NotNull(firstservice);
        Assert.Equal(firstServiceName, firstservice.Name);

        Assert.Equal(1, _databaseContext.Services.Where(c => c.Name == firstServiceName).Count());
    }

    #endregion
}