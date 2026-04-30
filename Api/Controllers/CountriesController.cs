using Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private static readonly List<Country> countries =
        [
            new Data.Country { Id = 1, Name = "United States", Code = "US" },
            new Data.Country { Id = 2, Name = "Canada", Code = "CA" },
            new Data.Country { Id = 3, Name = "United Kingdom", Code = "UK" }
        ];

        [HttpGet]
        public ActionResult<IEnumerable<Country>> Get() => Ok(countries);

        [HttpGet("{id}")]
        public ActionResult<Country> Get(int id)
        {
            var country = countries.FirstOrDefault(c => c.Id == id);
            if (country == null) return NotFound();
            return Ok(country);
        }

        [HttpPost]
        public ActionResult<Country> Post([FromBody] Country country)
        {
            if (countries.Any(c => c.Id == country.Id)) return BadRequest("Country with the same ID already exists.");
            countries.Add(country);
            return CreatedAtAction(nameof(Get), new { id = country.Id }, country);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Country updatedCountry)
        {
            var existingCountry = countries.FirstOrDefault(c => c.Id == id);
            if (existingCountry == null) return NotFound();

            existingCountry.Name = updatedCountry.Name;
            existingCountry.Code = updatedCountry.Code;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var country = countries.FirstOrDefault(c => c.Id == id);
            if (country == null) return NotFound();
            countries.Remove(country);
            return NoContent();
        }
    }
}
