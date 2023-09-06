using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Web.ViewModels;

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
                var peopleDto = _personService.GetAllPeople();

                // Map PersonDto to PersonViewModel
                var peopleViewModel = peopleDto.Select(personDto => new PersonViewModel
                {
                    Id = personDto.Id,
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    DateOfBirth = personDto.DateOfBirth,
                }).ToList();

                return Ok(peopleViewModel);
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
                var personDto = _personService.GetPersonById(id);

                if (personDto == null)
                {
                    return NotFound();
                }

                // Map PersonDto to PersonViewModel in the web project
                var personViewModel = new PersonViewModel
                {
                    Id = personDto.Id,
                    FirstName = personDto.FirstName,
                    LastName = personDto.LastName,
                    DateOfBirth = personDto.DateOfBirth,
                };

                return Ok(personViewModel);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Person> AddPerson([FromBody] PersonViewModel personViewModel)
        {
            try 
            {
                if (personViewModel == null)
                {
                    return BadRequest();
                }

                // Map PersonViewModel to PersonDto 
                var personDto = new PersonDto
                {
                    FirstName = personViewModel.FirstName,
                    LastName = personViewModel.LastName,
                    DateOfBirth= personViewModel.DateOfBirth,
                };

                _personService.AddPerson(personDto);
                return CreatedAtAction(nameof(GetPersonById), new { id = personDto.Id }, personViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] PersonViewModel personViewModel)
        {
            try
            {
                if (personViewModel == null || id != personViewModel.Id)
                {
                    return BadRequest();
                }

                // Map PersonViewModel to PersonDto
                var personDto = new PersonDto
                {
                    Id = personViewModel.Id,
                    FirstName = personViewModel.FirstName,
                    LastName = personViewModel.LastName,
                    DateOfBirth = personViewModel.DateOfBirth,
                };

                _personService.UpdatePerson(id, personDto);
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