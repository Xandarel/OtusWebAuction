using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.BL.Users.Login;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.BL.Users.Update;

namespace OtusWebAuction.BL.Interfaces
{
    public interface IUserHandler
    {
        Task<AuthUserDto> LoginUser(LoginUserRequest request, CancellationToken ct);
        Task<AuthUserDto> RegisterUser(RegisterUserRequest request, CancellationToken ct);
        Task<AuthUserDto> UpdateUser(Guid userId, UpdateUserRequest request, CancellationToken ct);
    }
}