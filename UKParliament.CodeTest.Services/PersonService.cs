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
        }

        public List<Person> GetAllPeople()
        {
            if(_context.People == null)
            {
                return new List<Person>();
            }
            return _context.People.ToList();
        }

        public Person? GetPersonById(int id)
        {
            if(_context.People == null)
            {
                return null;
            }
            return _context.People.FirstOrDefault(p => p.Id == id);
        }

        public void AddPerson(Person person)
        {
            if (_context.People != null)
            {
                _context.People.Add(person);
                _context.SaveChanges();
            }
        }

        public void UpdatePerson(int id, Person updatedPerson)
        {
            if (_context.People!= null)
            {
                var existingPerson = _context.People.FirstOrDefault(p => p.Id == id);
                if (existingPerson != null)
                {
                    existingPerson.FirstName = updatedPerson.FirstName;
                    existingPerson.LastName = updatedPerson.LastName;
                    existingPerson.Age = updatedPerson.Age;
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