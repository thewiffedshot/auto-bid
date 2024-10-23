using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoBid.WebApi.DataContext;
using WebApi.DataContext.Models;
using WebApi.Interfaces.Models;

namespace AutoBid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarOfferController : ControllerBase
    {
        private readonly AutoBidDbContext _context;

        public CarOfferController(AutoBidDbContext context)
        {
            _context = context;
        }

        // POST: api/CarOffer
        [HttpPost]
        public async Task<ActionResult<CarOffer>> CreateCarOffer(CarOfferModel carOffer)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.SingleOrDefaultAsync(e => e.Username == carOffer.Owner.Username);

            if (user == null)
            {
                return BadRequest("Offer's owner not found.");
            }

            var carOfferEntity = carOffer.ToEntity();
            _context.CarOffers.Add(carOfferEntity);

            await _context.SaveChangesAsync();

            return Created("Car offer created.", carOfferEntity.Id);
        }

        // PUT: api/CarOffer/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCarOffer(Guid id, CarOfferModel carOffer)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var existingCarOffer = await _context.CarOffers.FindAsync(id);

            if (existingCarOffer == null)
            {
                return NotFound("Car offer not found.");
            }

            _context.CarOffers.Update(carOffer.ToEntity(id));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarOfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Car offer updated.");
        }

        // DELETE: api/CarOffer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarOffer(Guid id)
        {
            var carOffer = await _context.CarOffers.FindAsync(id);

            if (carOffer == null)
            {
                return NotFound("Car offer not found.");
            }

            _context.CarOffers.Remove(carOffer);
            await _context.SaveChangesAsync();

            return Ok("Car offer deleted.");
        }

        private bool CarOfferExists(Guid id)
        {
            return _context.CarOffers.Any(e => e.Id == id);
        }

        // GET: api/CarOffer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarOffer>> GetCarOffer(Guid id)
        {
            var carOffer = await _context.CarOffers.FindAsync(id);

            if (carOffer == null)
            {
                return NotFound("Car offer not found.");
            }

            return Ok(carOffer.ToModel());
        }
    }
}