using System.ComponentModel.DataAnnotations;
using TypeGen.Core.TypeAnnotations;

namespace WebApi.WebDto
{
    [ExportTsClass]
    public class CarImageModel 
    {
        public Guid? Id { get; set; }
        [Required]
        // Max 5MiB image size
        [MaxLength(6400000)]
        public required string Base64ImageData { get; set; }
    }
}