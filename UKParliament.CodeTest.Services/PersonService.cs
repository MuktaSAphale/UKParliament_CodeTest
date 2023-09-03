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
                existingPerson.Age = updatedPerson.Age;
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