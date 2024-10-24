using AutoBid.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Models;
using WebApi.Interfaces.Mappers;
using WebApi.Interfaces.Models;
using WebApi.WebDto;

public class CarOfferSet
{
    private readonly AutoBidContext _context;
    private readonly AutoBidDbContext _dbContext;
    private readonly IMapper<CarOfferModel, CarOffer> _carOfferMapper;

    public CarOfferSet(AutoBidContext context, AutoBidDbContext dbContext, IMapper<CarOfferModel, CarOffer> carOfferMapper)
    {
        _context = context;
        _dbContext = dbContext;
        _carOfferMapper = carOfferMapper;
    }

    public async Task<Guid> Create(CarOfferModel carOffer, List<CarImageModel> carImages)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(e => e.Username == carOffer.OwnerUsername);

        if (user == null)
        {
            throw new Exception("Offer's owner not found.");
        }

        var carOfferEntity = _carOfferMapper.Map(carOffer);
        carOfferEntity.Owner = user;
        _dbContext.CarOffers.Add(carOfferEntity);

        await _dbContext.SaveChangesAsync();

        var imageIds = await _context.CarImages.Create(carOfferEntity.Id!.Value, carImages);
        
        return carOfferEntity.Id!.Value;
    }

    public async Task Update(Guid id, CarOfferModel carOffer, List<CarImageModel>? newImages, List<Guid>? imagesToDelete)
    {
        var existingCarOffer = _dbContext.CarOffers
            .Include(e => e.Owner)
            .SingleOrDefault(e => e.Id == id);

        if (existingCarOffer == null)
        {
            throw new Exception("Car offer not found.");
        }

        _carOfferMapper.Map(carOffer, existingCarOffer);

        await _dbContext.SaveChangesAsync();

        if (newImages != null) {
            await _context.CarImages.Create(id, newImages);
        }

        if (imagesToDelete != null)
        {
            foreach (var imageId in imagesToDelete)
            {
                await _context.CarImages.DeleteById(imageId);
            }
        }
    }

    public async Task Delete(Guid id)
    {
        var carOffer = await _dbContext.CarOffers.FindAsync(id);

        if (carOffer == null)
        {
            throw new Exception("Car offer not found.");
        }

        await _context.CarImages.DeleteByCarOfferId(id);

        _dbContext.CarOffers.Remove(carOffer);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<CarOfferModel?> Get(Guid id)
    {
        return await _dbContext.CarOffers
            .Include(e => e.Owner)
            .SingleOrDefaultAsync(e => e.Id == id)
            .ContinueWith(e => e.Result?.ToModel()); 
    }
}