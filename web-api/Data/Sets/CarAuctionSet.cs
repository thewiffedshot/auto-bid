using System.Linq;
using AutoBid.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Models;
using WebApi.Interfaces.Mappers;
using WebApi.Interfaces.Models;

public class CarAuctionSet
{
    private readonly AutoBidDbContext _context;
    private readonly IMapper<CarOfferModel, CarOffer> _carOfferMapper;

    public CarAuctionSet(AutoBidDbContext context, IMapper<CarOfferModel, CarOffer> carOfferMapper)

    {
        _context = context;
        _carOfferMapper = carOfferMapper;
    } 

    public async Task<List<CarAuctionModel>> All()
    {
        return await _context.CarAuctions
            .Where(auction => auction.Id != null)
            .Select(auction => auction.ToModel())
            .ToListAsync();
   }

    public async Task<CarAuction?> Get(Guid id)
    {
        return await _context.CarAuctions.FindAsync(id);
    }

    public async Task<Guid> CreateAuction(CarOfferModel carOffer, decimal startPrice)
    {
        var user = await _context.Users.SingleOrDefaultAsync(e => e.Username == carOffer.OwnerUsername);

        if (user == null)
        {
            throw new Exception("Offer's owner not found.");
        }

        var carOfferEntity = _carOfferMapper.Map(carOffer);
        carOfferEntity.Owner = user;
        _context.CarOffers.Add(carOfferEntity);

        carOfferEntity.CarAuction = new CarAuction
        {
            Id = Guid.NewGuid(),
            StartingPrice = startPrice
        };

        await _context.SaveChangesAsync();

        return carOfferEntity.CarAuction.Id!.Value;
    }

    public async Task<CarAuction> GetByCarOfferId(Guid carOfferId)
    {
        var offer = await _context.CarOffers
            .FindAsync(carOfferId) ?? throw new ArgumentException("Car offer not found for the given car offer ID", nameof(carOfferId));

        return await _context.CarAuctions
            .Where(auction => auction.Id == offer.CarAuctionId)
            .SingleOrDefaultAsync() ?? throw new ArgumentException("Car auction not found for the given car offer ID", nameof(carOfferId));
    }

    public async Task Create(CarAuction carAuction, Guid carOfferId)
    {
        var carOffer = await _context.CarOffers.SingleAsync(x => x.Id == carOfferId);

        if (carOffer == null)
        {
            throw new ArgumentException("Car offer not found", nameof(carOfferId));
        }

        _context.CarAuctions.Add(carAuction);

        // Ensure the auction has a valid ID
        // if (carAuction.Id == null || carAuction.Id == Guid.Empty)
        // {
        //     carAuction.Id = Guid.NewGuid();
        // }

        await _context.SaveChangesAsync();
    }

    public async Task Update(CarAuction carAuction)
    {
        _context.CarAuctions.Update(carAuction);
        await _context.SaveChangesAsync();
    }
}