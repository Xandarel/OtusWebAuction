using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Users.Dto
{
    public sealed class AuthUserDto
    {
        public UserStatus Status { get; set; }

        public UserDto? Responce { get; set; }
        public string? Message { get; set; }
    }
}
