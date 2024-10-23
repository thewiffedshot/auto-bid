using System.ComponentModel.DataAnnotations;
using WebApi.DataContext.Models;

namespace WebApi.Interfaces.Models
{
    public class UserModel : IEntityModel<User>
    {
        [Required]
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

        public User ToEntity(Guid? existingRecordId = null)
        {
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                throw new ArgumentException("FirstName is required.");
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                throw new ArgumentException("Email is required.");
            }

            if (string.IsNullOrWhiteSpace(Username))
            {
                throw new ArgumentException("Username is required.");
            }
            
            return new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Username = Username
            };
        }
    }
}