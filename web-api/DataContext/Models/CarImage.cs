using System.ComponentModel.DataAnnotations;
using WebApi.Interfaces;
using WebApi.Interfaces.Models;

namespace WebApi.DataContext.Models
{
    public class CarImage : IEntity<CarImageModel>
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        // Max 5MiB image size
        [MaxLength(6400000)]
        public required string Base64ImageData { get; set; }

        public CarImageModel ToModel()
        {
            return new CarImageModel
            {
                Base64ImageData = Base64ImageData
            };
        }
    }
}