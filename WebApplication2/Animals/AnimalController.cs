using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Animals
{
    [ApiController]
    [Route("/api/animals")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllAnimals([FromQuery] string orderBy)
        {
            var animals = _animalService.GetAllAnimals(orderBy);
            return Ok(animals);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddAnimal([FromBody] Animal animal)
        {
            if (animal == null)
            {
                return BadRequest("Animal object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _animalService.AddAnimal(animal);
            return success ? StatusCode(StatusCodes.Status201Created) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateAnimal(int id, [FromBody] Animal animal)
        {
            if (animal == null || animal.Id != id)
            {
                return BadRequest("Invalid animal object");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = _animalService.UpdateAnimal(id, animal);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteAnimal(int id)
        {
            var success = _animalService.DeleteAnimal(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
