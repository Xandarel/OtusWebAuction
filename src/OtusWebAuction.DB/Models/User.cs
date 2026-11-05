using System.ComponentModel.DataAnnotations;

namespace OtusWebAuction.DB.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Name { get; set; }
        public required string Surname { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}