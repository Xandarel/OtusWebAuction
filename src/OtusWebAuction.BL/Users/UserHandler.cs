using OtusWebAuction.BL.Interfaces;
using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.BL.Users.Login;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.BL.Users.Update;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Users
{
    public sealed class UserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        public async Task<AuthUserDto> LoginUser(LoginUserRequest request, CancellationToken ct)
        {
            LoginUserDto? user = await userRepository.GetUserByEmail(request.Email, ct);
            if (user is null || !passwordHasher.Verify(request.Email, request.Password, user.Password))
            {
                return new AuthUserDto
                {
                    Status = UserStatus.LoginFailed,
                    Message = "Invalid email or password."
                };
            }

            return new AuthUserDto
            {
                Status = UserStatus.Login,
                Responce = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email
                }
            };
        }

        public async Task<AuthUserDto> RegisterUser(RegisterUserRequest request, CancellationToken ct)
        {
            if (await userRepository.GetUserByEmail(request.Email, ct) is not null)
            {
                return new AuthUserDto
                {
                    Status = UserStatus.EmailAlreadyExists,
                    Message = $"User with email {request.Email} already exists."
                };
            }

            string passwordHash = passwordHasher.Hash(request.Email, request.Password);
            UserDto user = await userRepository.CreateUser(request, passwordHash, ct);
            return new AuthUserDto
            {
                Status = UserStatus.Created,
                Responce = user
            };
        }

        public async Task<AuthUserDto> UpdateUser(Guid userId, UpdateUserRequest request, CancellationToken ct)
        {
            UserDto? user = await userRepository.UpdateUser(userId, request, ct);
            if (user is null)
            {
                return new AuthUserDto
                {
                    Status = UserStatus.UserNotFound,
                    Message = $"User with id {userId} was not found."
                };
            }

            return new AuthUserDto
            {
                Status = UserStatus.Updated,
                Responce = user
            };
        }
    }
}
