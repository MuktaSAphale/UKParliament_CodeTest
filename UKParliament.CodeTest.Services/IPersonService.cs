using UKParliament.CodeTest.Data;

namespace UKParliament.CodeTest.Services
{
    public interface IPersonService
    {
        public List<PersonDto> GetAllPeople();

        public PersonDto? GetPersonById(int id);

        public int AddPerson(PersonDto personDto);

        public void UpdatePerson(int id, PersonDto updatedPerson);

        public void DeletePerson(int id);

    }
}