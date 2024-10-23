using System.ComponentModel.DataAnnotations;
using WebApi.Interfaces;
using WebApi.Interfaces.Models;

namespace WebApi.DataContext.Models
{
    public class User : IEntity<UserModel>
    {
        [Key]
        public Guid? Id { get; set; }

        [MaxLength(100)]
        public required string FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Username { get; set; }

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