using System;
using System.Globalization;
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
                new Person { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = DateTime.ParseExact("1990-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture) },
                new Person { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = DateTime.ParseExact("1985-06-06", "yyyy-MM-dd", CultureInfo.InvariantCulture) }
            );
            _context.SaveChanges();
            
        }

        public List<PersonDto> GetAllPeople()
        {
            if (_context.People != null)
            {
                return _context.People.Select(p => new PersonDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName, 
                    DateOfBirth = p.DateOfBirth.ToString("yyyy-MM-dd")
                }).ToList();
            }

            return new List<PersonDto>();
        }

        public PersonDto? GetPersonById(int id)
        {
            if (_context.People != null)
            {
                var person = _context.People.FirstOrDefault(p => p.Id == id);

                if (person != null)
                {
                    return new PersonDto
                    {
                        Id = person.Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        DateOfBirth = person.DateOfBirth.ToString("yyyy-MM-dd")
                    };
                }

                return null;
            }

            return null;
        }

        public int AddPerson(PersonDto personDto)
        {
            if (_context.People != null)
            {
                var person = new Person
                {
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    DateOfBirth = DateTime.Parse(personDto.DateOfBirth)
                };

                _context.People.Add(person);
                _context.SaveChanges();

                return person.Id;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public void UpdatePerson(int id, PersonDto updatedPerson)
        {
            if (_context.People != null)
            {
                var existingPerson = _context.People.FirstOrDefault(p => p.Id == updatedPerson.Id);

                if (existingPerson != null)
                {
                    // Update properties of the existing person
                    existingPerson.FirstName = updatedPerson.FirstName;
                    existingPerson.LastName = updatedPerson.LastName;
                    existingPerson.DateOfBirth = DateTime.Parse(updatedPerson.DateOfBirth);

                    _context.People.Update(existingPerson);
                    _context.SaveChanges();
                }
            }
        }

        public void DeletePerson(int id)
        {
            if (_context.People != null)
            {
                var person = _context.People.FirstOrDefault(p => p.Id == id);

                if (person != null)
                {
                    _context.People.Remove(person);
                    _context.SaveChanges();
                }
            }
        }
    }
}