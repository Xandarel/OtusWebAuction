using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.DB.Models
{
    public class Role
    {
        public long Id { get; set; }

        public required UserRole RoleName { get; set; }
    }
}
