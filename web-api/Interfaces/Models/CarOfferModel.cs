using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;
using WebApi.WebDto;

namespace WebApi.Interfaces.Models
{
    [ExportTsClass]
    public class CarOfferModel
    {
        public Guid? Id { get; set; }

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

        public bool? OdometerInMiles { get; set; }
        public bool? IsAutomatic { get; set; }
        
        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public required string OwnerUsername { get; set; }

        public List<CarImageModel>? CarImagesToAdd { get; set; }

        public List<Guid>? CarImagesToDelete { get; set; }
    }
}