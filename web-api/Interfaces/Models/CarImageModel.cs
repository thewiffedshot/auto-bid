using System.ComponentModel.DataAnnotations;
using WebApi.DataContext.Models;

namespace WebApi.Interfaces.Models
{
    public class CarImageModel : IEntityModel<CarImage>
    {
        [Required]
        // Max 5MiB image size
        [MaxLength(6400000)]
        public required string Base64ImageData { get; set; }

        public CarImage ToEntity(Guid? existingRecordId = null)
        {
            if (string.IsNullOrWhiteSpace(Base64ImageData))
            {
                throw new ArgumentException("Base64ImageData is required.");
            }

            return new CarImage
            {
                Id = existingRecordId,
                Base64ImageData = Base64ImageData
            };
        }
    }
}