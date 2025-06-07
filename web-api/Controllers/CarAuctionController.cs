using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces.Models;
using WebApi.WebDto;

namespace AutoBid.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarAuctionController : ControllerBase
    {
        private readonly AutoBidContext _context;

        public CarAuctionController(AutoBidContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarAuctionModel>> Get(Guid id)
        {
            var carAuction = await _context.CarAuctions.Get(id);

            if (carAuction == null)
            {
                return NotFound("Car auction not found.");
            }

            return Ok(carAuction.ToModel());
        }

        [HttpGet("{offerId}")]
        public async Task<ActionResult<CarAuctionModel?>> ByOffer(Guid offerId)
        {
            var carAuction = await _context.CarAuctions.GetByCarOfferId(offerId);

            return Ok(carAuction?.ToModel() ?? null);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAuctionModel>>> All()
        {
            var carAuctions = await _context.CarAuctions.All();
            return Ok(carAuctions);
        }
    }
}