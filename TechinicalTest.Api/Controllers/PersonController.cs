using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Infrastructure.Repositories;

namespace TechinicalTest.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]

        public IActionResult GetPersons()
        {
            var person = new PersonServices().GetAllPerson();
            return Ok(person);
        }
    }
}
