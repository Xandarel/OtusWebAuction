using OtusWebAuction.BL.Users.Register;

namespace OtusWebAuction.BL.Interfaces
{
    public interface IUserRepository
    {
        Task<RegisterUserResponce> CreateUser(RegisterUserRequest request, CancellationToken ct);
        Task GetByIdAsync(Guid id, CancellationToken ct);
        Task<bool> IsUserExists(string email, CancellationToken ct);
    }
}
