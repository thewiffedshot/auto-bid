using AutoBid.WebApi.Data;
using WebApi.Data.Models;
using WebApi.Interfaces.Mappers;
using WebApi.Interfaces.Models;

public class AutoBidContext {
    public CarOfferSet CarOffers { get; set; }
    public UserSet Users { get; set; }
    public CarImageSet CarImages { get; set; }
    public CarAuctionSet CarAuctions { get; set; }

    private readonly AutoBidDbContext _context;

    public AutoBidContext(AutoBidDbContext context, IMapper<CarOfferModel, CarOffer> carOfferMapper, IConfiguration configuration)
    {
        _context = context;
        CarOffers = new CarOfferSet(this, context, carOfferMapper);
        Users = new UserSet(context);
        CarImages = new CarImageSet(context, configuration);
        CarAuctions = new CarAuctionSet(context, carOfferMapper);
    }
}