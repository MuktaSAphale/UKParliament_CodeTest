using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public interface IPersonService
    {
        public List<Person> GetAllPeople();

        public Person GetPersonById(int id);

        public void AddPerson(Person person);

        public void UpdatePerson(int id, Person updatedPerson);

        public void DeletePerson(int id);

    }
}