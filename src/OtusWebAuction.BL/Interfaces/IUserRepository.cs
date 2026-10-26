using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.BL.Users.Update;

namespace OtusWebAuction.BL.Interfaces
{
    public interface IUserRepository
    {

        Task<UserDto> CreateUser(RegisterUserRequest request, string passwordHash, CancellationToken ct);
        Task<LoginUserDto?> GetUserByEmail(string email, CancellationToken ct);
        Task<UserDto?> UpdateUser(Guid userId, UpdateUserRequest request, CancellationToken ct);
        Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
