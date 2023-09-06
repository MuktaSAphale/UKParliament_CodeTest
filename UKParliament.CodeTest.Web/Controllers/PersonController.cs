using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var people = _personService.GetAllPeople();
                return Ok(people);
            }
            catch (Exception ex) 
            {                
                return BadRequest(ex.Message);
            }   
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            try
            {
                var person = _personService.GetPersonById(id);
                if (person == null)
                {
                    return NotFound();
                }
                return Ok(person);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Person> AddPerson([FromBody] Person person)
        {
            try { 
                _personService.AddPerson(person);
                return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Person updatedPerson)
        {
            try
            {
                _personService.UpdatePerson(id, updatedPerson);
                return NoContent();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
}

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}