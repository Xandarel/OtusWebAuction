using OtusWebAuction.BL.Interfaces;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Users.Register
{
    public sealed class RegisterUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        public async Task<RegisterUserResult> RegisterUser(RegisterUserRequest request, CancellationToken ct)
        {
            bool isUserExists = await userRepository.IsUserExists(request.Email, ct);
            if (isUserExists)
            {
                return new RegisterUserResult
                {
                    Status = RegisterUserStatus.EmailAlreadyExists,
                    Responce = null,
                    Message = $"User with email {request.Email} already exists."
                };
            }

            request.Password = passwordHasher.Hash(request.Email, request.Password);

            RegisterUserResponce user = await userRepository.CreateUser(request, ct);

            return new RegisterUserResult
            {
                Status = RegisterUserStatus.Created,
                Responce = user
            };
        }
    }
}
