using Microsoft.AspNetCore.Mvc;
using WebApi.WebDto;

namespace AutoBid.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarImageController : ControllerBase
    {
        private readonly AutoBidContext _context;

        public CarImageController(AutoBidContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarImageModel>> Get(Guid id)
        {
            var carImage = await _context.CarImages.Get(id);

            if (carImage == null)
            {
                return NotFound("Car image not found.");
            }

            return Ok(carImage);
        }

        [HttpGet("{offerId}")]
        public async Task<ActionResult<IEnumerable<CarImageModel>>> ForOffer(Guid offerId)
        {
            var carImages = await _context.CarImages
                .GetByCarOfferId(offerId);

            return Ok(carImages);   
        }
    }
}