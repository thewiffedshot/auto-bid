using System.ComponentModel.DataAnnotations;
using WebApi.Interfaces;
using WebApi.Interfaces.Models;

namespace WebApi.Data.Models
{
    public class User : IEntity<UserModel>
    {
        [Key]
        public Guid? Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; } = default!;

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = default!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public UserModel ToModel()
        {
            return new UserModel
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username
            };
        }
    }
}