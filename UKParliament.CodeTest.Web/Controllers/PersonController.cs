using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;

namespace UKParliament.CodeTest.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;    
        }

        [HttpGet]
        public ActionResult<List<Person>> GetAllPeople()
        {
            var people = _personService.GetAllPeople();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = _personService.GetPersonById(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public ActionResult<Person> AddPerson([FromBody] Person person)
        {
            _personService.AddPerson(person);
            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
        {
            _personService.UpdatePerson(id, updatedPerson);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            _personService.DeletePerson(id);
            return NoContent();
        }
    }
}