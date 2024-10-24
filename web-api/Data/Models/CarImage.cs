using System.ComponentModel.DataAnnotations;

namespace WebApi.Data.Models
{
    public class CarImage
    {
        [Key]
        public Guid Id { get; set;}

        public Guid CarOfferId { get; set; }
    }
}