using OtusWebAuction.BL.Users.Dto;

namespace OtusWebAuction.BL.Users.Register
{
    public sealed record AuthResponse(UserDto User, string AccessToken);
}
