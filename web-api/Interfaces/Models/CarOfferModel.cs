using System.ComponentModel.DataAnnotations;
using WebApi.DataContext.Models;

namespace WebApi.Interfaces.Models
{
    public class CarOfferModel : IEntityModel<CarOffer>
    {
        [Required]
        public string? Make { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string? Model { get; set; }

        [Required]
        [Range(1886, 2100)] // Assuming cars are not older than 1886 and not from the future
        public uint? Year { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal? Price { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid odometer value")]
        public string? Odometer { get; set; }

        [Required]
        public bool? OdometerInMiles { get; set; }

        [Required]
        public bool? IsAutomatic { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one image is required")]
        public List<CarImageModel>? Images { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public required UserModel Owner { get; set; }

        public CarOffer ToEntity(Guid? existingRecordId = null)
        {
            if (string.IsNullOrWhiteSpace(Make))
            {
                throw new ArgumentException("Make is required.");
            }

            if (string.IsNullOrWhiteSpace(Model))
            {
                throw new ArgumentException("Model is required.");
            }

            if (!Year.HasValue)
            {
                throw new ArgumentException("Year is required.");
            }

            if (!Price.HasValue)
            {
                throw new ArgumentException("Price is required.");
            }

            if (string.IsNullOrWhiteSpace(Odometer))
            {
                throw new ArgumentException("Odometer is required.");
            }

            if (!Images?.Any() ?? true)
            {
                throw new ArgumentException("At least one image is required.");
            }

            return new CarOffer
            {
                Id = existingRecordId,
                Make = Enum.TryParse<CarMake>(Make, out var make) 
                    ? make 
                    : throw new ArgumentException("Invalid car make."),
                Model = Model,
                Year = Year.Value,
                Price = Price.Value,
                Odometer = Odometer,
                OdometerInMiles = OdometerInMiles ?? false,
                IsAutomatic = IsAutomatic ?? false,
                Images = !existingRecordId.HasValue 
                    ? Images!.Select(i => i.ToEntity()).ToList()
                    : new List<CarImage>(),
                Description = Description,
                Owner = !existingRecordId.HasValue 
                    ? Owner.ToEntity()
                    : null
            };
        }
    }
}