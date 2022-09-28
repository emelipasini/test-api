using Microsoft.EntityFrameworkCore;

using FluentAssertions;

using WebAPI.Services;
using WebAPI.Models;
using WebAPI;

using Tests.Fixtures;

namespace Tests.System.Services
{
    public class StreetServiceTest
    {
        private readonly APIDbContext _context;
        public StreetServiceTest()
        {
            var options = new DbContextOptionsBuilder<APIDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new APIDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async void GetAll_WhenCalled_ReturnsList()
        {
            _context.Streets.AddRange(StreetFixture.GetTestStreets());
            _context.SaveChanges();

            var sut = new StreetService(_context);
            var result = await sut.GetAll();

            result.Should().HaveCount(StreetFixture.GetTestStreets().Count);
            Assert.IsType<List<Street>>(result);
        }

        [Fact]
        public async Task GetAll_WhenCollectionIsEmpty_ReturnsEmptyList()
        {
            var sut = new StreetService(_context);
            var result = await sut.GetAll();

            result.Should().HaveCount(0);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnsExpectedObject()
        {
            _context.Streets.AddRange(StreetFixture.GetTestStreets());
            _context.SaveChanges();

            int id = 2;
            var sut = new StreetService(_context);
            var result = await sut.GetById(id);

            Assert.IsType<Street>(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async void GetById_WhenIdDoesNotExist_ReturnsEmptyObject()
        {
            var sut = new StreetService(_context);
            var result = await sut.GetById(0);

            result.Name.Should().Be(null);
            result.City.Should().Be(null);
            Assert.IsType<Street>(result);
        }

        [Fact]
        public async void Create_OnSuccess_ReturnsExpectedObject()
        {
            var street = StreetFixture.GetTestStreetById();

            var sut = new StreetService(_context);
            var result = await sut.Create(street);

            Assert.IsType<Street>(result);
            Assert.Equal(street.Name, result.Name);
        }

        [Fact]
        public async void Create_WhenIdAlreadyExists_ReturnsEmptyObject()
        {
            var street = StreetFixture.GetTestStreetById();
            _context.Streets.AddRange(street);
            _context.SaveChanges();

            var sut = new StreetService(_context);
            var result = await sut.Create(street);

            result.Name.Should().Be(null);
            result.City.Should().Be(null);
            Assert.IsType<Street>(result);
        }

        [Fact]
        public async void Create_OnSuccess_ImpactsTheDatabase()
        {
            var street = StreetFixture.GetTestStreetById();

            var sut = new StreetService(_context);
            var result = await sut.Create(street);

            var newStreet = _context.Streets.FirstOrDefault(x => x.Id == street.Id);

            Assert.Equal(street, newStreet);
        }

        [Fact]
        public async void Update_OnSuccess_ReturnsExpectedObject()
        {
            _context.Streets.AddRange(StreetFixture.GetTestStreets());
            _context.SaveChanges();

            var streetToEdit = StreetFixture.GetTestStreets()[0];
            streetToEdit.Name = "Pellegrini";

            var sut = new StreetService(_context);
            var result = await sut.Update(streetToEdit);

            Assert.IsType<Street>(result);
            Assert.Equal(streetToEdit.Id, result.Id);
        }

        [Fact]
        public async void Update_WhenIdDoesNotExist_ReturnsEmptyObject()
        {
            var street = StreetFixture.GetTestStreetById();
            var sut = new StreetService(_context);
            var result = await sut.Update(street);

            result.Name.Should().Be(null);
            result.City.Should().Be(null);
            Assert.IsType<Street>(result);
        }

        [Fact]
        public async void Update_OnSuccess_ImpactsTheDatabase()
        {
            _context.Streets.AddRange(StreetFixture.GetTestStreets());
            _context.SaveChanges();

            var streetToEdit = StreetFixture.GetTestStreets()[0];
            streetToEdit.Name = "Pellegrini";

            var sut = new StreetService(_context);
            var result = await sut.Update(streetToEdit);

            var editedStreet = _context.Streets.FirstOrDefault(x => x.Id == streetToEdit.Id);

            Assert.Equal(streetToEdit.Id, editedStreet.Id);
            Assert.Equal(streetToEdit.Name, editedStreet.Name);
            Assert.Equal(streetToEdit.City, editedStreet.City);
        }

        [Fact]
        public async void Delete_WhenIdExists_ReturnsTrue()
        {
            _context.Streets.AddRange(StreetFixture.GetTestStreets());
            _context.SaveChanges();

            var streetToDelete = StreetFixture.GetTestStreets()[0];

            var sut = new StreetService(_context);
            var result = await sut.Delete(streetToDelete.Id);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public async void Delete_WhenIdDoesNotExist_ReturnsFalse()
        {
            var streetToDelete = StreetFixture.GetTestStreets()[0];

            var sut = new StreetService(_context);
            var result = await sut.Delete(streetToDelete.Id);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public async void Delete_OnSuccess_ImpactsTheDatabase()
        {
            _context.Streets.AddRange(StreetFixture.GetTestStreets());
            _context.SaveChanges();

            var streetToDelete = StreetFixture.GetTestStreets()[0];

            var sut = new StreetService(_context);
            var result = await sut.Delete(streetToDelete.Id);

            var deleted = _context.Streets.FirstOrDefault(s => s.Id == streetToDelete.Id); 

            Assert.Null(deleted);
        }

        internal void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

