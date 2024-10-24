using AutoBid.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Data.Models;
using WebApi.WebDto;

public class CarImageSet {
    private readonly AutoBidDbContext _context;
    private readonly IConfiguration _configuration;

    public CarImageSet(AutoBidDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    
    public async Task<List<Guid>> Create(Guid carOfferId, List<CarImageModel> carImages)
    {
        var imageEntities = new List<CarImage>();

        foreach (var carImage in carImages) {
            var imageFileId = Guid.NewGuid();
            var imageBytes = Convert.FromBase64String(carImage.Base64ImageData);

            imageEntities.Add(new CarImage
            {
                Id = imageFileId,
                CarOfferId = carOfferId
            });

            var imageFilePath = Path.Combine(_configuration["ImageStorage:Path"] ?? "images", "carOffers");
            Directory.CreateDirectory(imageFilePath);
            
            await File.WriteAllBytesAsync(Path.Combine(imageFilePath, imageFileId.ToString()), imageBytes);
        }

        _context.CarImages.AddRange(imageEntities);

        await _context.SaveChangesAsync();
        return imageEntities.Select(ci => ci.Id).ToList();
    }

    public async Task<List<CarImageModel>> GetByCarOfferId(Guid carOfferId)
    {
        var carImages = await _context.CarImages.Where(ci => ci.CarOfferId == carOfferId).ToListAsync();

        return carImages.Select(ci => new CarImageModel
        {
            Base64ImageData = Convert.ToBase64String(
                File.ReadAllBytes(
                    Path.Combine(_configuration["ImageStorage:Path"] ?? "images", $"carOffers/{ci.Id}")
                )
            )
        }).ToList();
    }

    public async Task<CarImageModel?> Get(Guid id)
    {
        var carImage = await _context.CarImages.FindAsync(id);

        if (carImage == null)
        {
            return null;
        }

        var imageFilePath = Path.Combine(_configuration["ImageStorage:Path"] ?? "images", $"carOffers/{carImage.Id}");
        var imageBytes = await File.ReadAllBytesAsync(imageFilePath);

        return new CarImageModel
        {
            Base64ImageData = Convert.ToBase64String(imageBytes)
        };
    }

    public async Task DeleteByCarOfferId(Guid carOfferId)
    {
        var carImages = await _context.CarImages.Where(ci => ci.CarOfferId == carOfferId).ToListAsync();

        foreach (var carImage in carImages)
        {
            var imageFilePath = Path.Combine(_configuration["ImageStorage:Path"] ?? "images", $"carOffers/{carImage.Id}");
            File.Delete(imageFilePath);
        }

        _context.CarImages.RemoveRange(carImages);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteById(Guid id)
    {
        var carImage = await _context.CarImages.FindAsync(id);

        if (carImage == null)
        {
            throw new Exception("Car image not found.");
        }

        var imageFilePath = Path.Combine(_configuration["ImageStorage:Path"] ?? "images", $"carOffers/{carImage.Id}");
        File.Delete(imageFilePath);

        _context.CarImages.Remove(carImage);
        await _context.SaveChangesAsync();
    }
}