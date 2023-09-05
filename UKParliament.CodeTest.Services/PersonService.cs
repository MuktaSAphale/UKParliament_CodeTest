using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public class PersonService : IPersonService
    {
        private readonly PersonManagerContext _context;

        public PersonService(PersonManagerContext context)
        {
            _context = context;
            if((context == null)|| (context.People==null))
            {
                throw new NullReferenceException();
            }
            else if (_context.People.Count() == 0)
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
            if (_context.People != null)
            {
                return _context.People.ToList();
            }

            return new List<Person>();
        }

        public Person? GetPersonById(int id)
        {
            if (_context.People != null)
            {
                return _context.People.FirstOrDefault(p => p.Id == id);
            }

            return null;
        }

        public void AddPerson(Person person)
        {
            if (_context.People != null)
            {
                _context.People.Add(person);
                _context.SaveChanges();
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void UpdatePerson(int id, Person updatedPerson)
        {
            if (_context.People != null)
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
        }

        public void DeletePerson(int id)
        {
            if (_context.People != null)
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
}