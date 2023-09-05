using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonManagerContext _context;

        public PersonService(PersonManagerContext context)
        {
            _context = context;
            if (_context.People.Count() == 0)
            {
                SeedData();
            }
        }
        private void SeedData() {
           
            _context.People.AddRange(
                new Person { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = "1990-01-01" },
                new Person { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = "1985-06-06" }
            );
            _context.SaveChanges();
            
        }

        public List<Person> GetAllPeople()
        {
            return _context.People.ToList();
        }

        public Person GetPersonById(int id)
        {
            return _context.People.FirstOrDefault(p => p.Id == id);
        }

        public void AddPerson(Person person)
        {
            _context.People.Add(person);
            _context.SaveChanges();
        }

        public void UpdatePerson(int id, Person updatedPerson)
        {
            var existingPerson = _context.People.FirstOrDefault(p => p.Id == id);
            if (existingPerson != null)
            {
                existingPerson.FirstName = updatedPerson.FirstName;
                existingPerson.LastName = updatedPerson.LastName;
                existingPerson.DateOfBirth = updatedPerson.DateOfBirth;
                _context.SaveChanges();
            }
        }

        public void DeletePerson(int id)
        {
            var personToRemove = _context.People.FirstOrDefault(p => p.Id == id);
            if (personToRemove != null)
            {
                _context.People.Remove(personToRemove);
                _context.SaveChanges();
            }
        }
    }
}