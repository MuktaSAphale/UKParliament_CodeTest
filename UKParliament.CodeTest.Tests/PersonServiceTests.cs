using UKParliament.CodeTest.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Services;

namespace UKParliament.CodeTest.Tests
{
    public class PersonServiceTests : IDisposable
    {
        private readonly DbContextOptions<PersonManagerContext> _options;
        private readonly PersonManagerContext _context;

        public PersonServiceTests()
        {
            // Set up an in-memory database for testing
            _options = new DbContextOptionsBuilder<PersonManagerContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database name for each test
                .Options;

            _context = new PersonManagerContext(_options);
            SeedData(); // Add initial data to the in-memory database
        }

        // Helper method to populate the in-memory database with initial data
        private void SeedData()
        {
            _context.People.AddRange(
                new Person { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = "1990-01-01" },
                new Person { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = "1985-06-06" }
            );
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Clean up the in-memory database after tests
            _context.Dispose();
        }

        [Fact]
        public void GetAllPeople_ReturnsAllPeople()
        {
            // Arrange
            var personService = new PersonService(_context);

            // Act
            var result = personService.GetAllPeople();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("John", result[0].FirstName);
            Assert.Equal("Doe", result[0].LastName);
            Assert.Equal("1990-01-01", result[0].DateOfBirth);

            Assert.Equal("Jane", result[1].FirstName);
            Assert.Equal("Smith", result[1].LastName);
            Assert.Equal("1985-06-06", result[1].DateOfBirth);

        }

        [Fact]
        public void GetPersonById_ReturnsPerson()
        {
            // Arrange
            var personService = new PersonService(_context);

            // Act
            var result = personService.GetPersonById(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Smith", result.LastName);
            Assert.Equal("1985-06-06", result.DateOfBirth);
        }

        [Fact]
        public void AddPerson_AddsNewPerson()
        {
            // Arrange
            var personService = new PersonService(_context);
            var newPerson = new Person { FirstName = "Alice", LastName = "Johnson", DateOfBirth = "1995-03-15" };

            // Act
            personService.AddPerson(newPerson);

            // Assert
            var addedPerson = _context.People.Find(newPerson.Id);
            Assert.NotNull(addedPerson);
            Assert.Equal("Alice", addedPerson.FirstName);
            Assert.Equal("Johnson", addedPerson.LastName);
            Assert.Equal("1995-03-15", addedPerson.DateOfBirth);
        }

        [Fact]
        public void UpdatePerson_UpdatesExistingPerson()
        {
            // Arrange
            var personService = new PersonService(_context);
            var updatedPerson = new Person { Id = 1, FirstName = "UpdatedFirstName", LastName = "UpdatedLastName", DateOfBirth = "2000-02-20" };

            // Act
            personService.UpdatePerson(1, updatedPerson);

            // Assert
            var existingPerson = _context.People.Find(1);
            Assert.NotNull(existingPerson);
            Assert.Equal("UpdatedFirstName", existingPerson.FirstName);
            Assert.Equal("UpdatedLastName", existingPerson.LastName);
            Assert.Equal("2000-02-20", existingPerson.DateOfBirth);

        }

        [Fact]
        public void DeletePerson_RemovesPerson()
        {
            // Arrange
            var personService = new PersonService(_context);

            // Act
            personService.DeletePerson(1);

            // Assert
            var deletedPerson = _context.People.Find(1);
            Assert.Null(deletedPerson);
        }
    }
}