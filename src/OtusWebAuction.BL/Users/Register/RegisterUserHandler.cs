using OtusWebAuction.BL.Interfaces;

namespace OtusWebAuction.BL.Users.Register
{
    public sealed class RegisterUserHandler(IUserRepository userRepository)
    {
        public async Task<RegisterUserResponce> RegisterUser(RegisterUserRequest request, CancellationToken ct)
        {
            bool isUserExists = await userRepository.IsUserExists(request.Email, ct);
            if (isUserExists)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            RegisterUserResponce user = await userRepository.CreateUser(request, ct);

            return user;
        }
    }
}
