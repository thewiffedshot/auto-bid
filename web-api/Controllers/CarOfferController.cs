using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces.Models;

namespace AutoBid.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarOfferController : ControllerBase
    {
        private readonly AutoBidContext _context;

        public CarOfferController(AutoBidContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CarOfferModel carOffer)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (carOffer.CarImagesToAdd == null || carOffer.CarImagesToAdd.Count == 0) {
                return BadRequest("At least one image is required.");
            }

            var id = await _context.CarOffers.Create(carOffer, carOffer.CarImagesToAdd);

            return Created("Car offer created.", id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CarOfferModel carOffer) 
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            await _context.CarOffers.Update(id, carOffer, carOffer.CarImagesToAdd, carOffer.CarImagesToDelete);

            return Ok("Car offer updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _context.CarOffers.Delete(id);

            return Ok("Car offer deleted.");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarOfferModel>> Get(Guid id)
        {
            var carOffer = await _context.CarOffers.Get(id);

            return Ok(carOffer);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarOfferModel>>> Get()
        {
            var carOffers = await _context.CarOffers.GetAll();

            return Ok(carOffers);
        }
    }
}