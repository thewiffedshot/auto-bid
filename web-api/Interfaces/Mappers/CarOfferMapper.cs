using WebApi.Data.Models;
using WebApi.Interfaces.Models;

namespace WebApi.Interfaces.Mappers
{
    public class CarOfferMapper : BaseMapper<CarOfferModel, CarOffer>
    {
        public CarOfferMapper() : base()
        {
        }

        public override void Map(CarOfferModel source, CarOffer destination)
        {
            if (Enum.TryParse<CarMake>(source.Make, out var make))
            {
                destination.Make = make;
            }
            else
            {
                throw new ArgumentException("Invalid car make.");
            }

            destination.Model = source.Model ?? throw new ArgumentException("Model is required.");
            destination.Year = source.Year ?? throw new ArgumentException("Year is required.");
            destination.Price = source.Price ?? throw new ArgumentException("Price is required.");
            destination.Odometer = source.Odometer ?? throw new ArgumentException("Odometer is required.");
            destination.OdometerInMiles = source.OdometerInMiles ?? false;
            destination.IsAutomatic = source.IsAutomatic ?? false;

           destination.Description = source.Description;
        }
    }
}