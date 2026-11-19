using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Users.Update
{
    public sealed record UpdatResponce
    {
        public UserStatus Status { get; set; }
        public string? Message { get; set; }
        public UserDto? Responce { get; internal set; }
    }
}
