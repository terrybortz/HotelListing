using Api.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static readonly List<Hotel> hotels =
        [
            new Data.Hotel { Id = 1, Name = "Grand Plaza", Address = "123 Main St", Rating = 4.5 },
            new Data.Hotel { Id = 2, Name = "Oceans View", Address = "456 Elm St", Rating = 4.0 },
            new Data.Hotel { Id = 3, Name = "Hotel C", Address = "789 Oak St", Rating = 3.5 }
        ];

        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get() => Ok(hotels);

        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return NotFound();
            return Ok(hotel);
        }

        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel hotel)
        {
            if (hotels.Any(h => h.Id == hotel.Id)) return BadRequest("Hotel with the same ID already exists.");
            hotels.Add(hotel);
            return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel == null) return NotFound();

            existingHotel.Name = updatedHotel.Name;
            existingHotel.Address = updatedHotel.Address;      
            existingHotel.Rating = updatedHotel.Rating;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if (hotel == null) return NotFound();
            hotels.Remove(hotel);
            
            return NoContent();
        }

    }
}
