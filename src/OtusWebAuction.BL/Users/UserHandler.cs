using Microsoft.Extensions.Logging;
using OtusWebAuction.BL.Interfaces;
using OtusWebAuction.BL.Interfaces.User;
using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.BL.Users.Login;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.BL.Users.Update;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Users
{
    public sealed class UserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ILogger<UserHandler> logger) : IUserHandler
    {
        public async Task<AuthUserDto> LoginUser(LoginUserRequest request, CancellationToken ct)
        {
            LoginUserDto? user = await userRepository.GetUserByEmail(request.Email, ct);
            if (user is null || !passwordHasher.Verify(request.Email, request.Password, user.Password))
            {
                logger.LogWarning("Login failed for {email}", request.Email);

                return new AuthUserDto
                {
                    Status = UserStatus.LoginFailed,
                    Message = $"Login failed for user with email {request.Email}. Invalid email or password."
                };
            }

            logger.LogInformation("User {userId} logged in", user.Id);

            return new AuthUserDto
            {
                Status = UserStatus.Login,
                Responce = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Role = user.Role
                }
            };
        }

        public async Task<AuthUserDto> RegisterUser(RegisterUserRequest request, CancellationToken ct)
        {
            if (await userRepository.GetUserByEmail(request.Email, ct) is not null)
            {
                logger.LogWarning("Registration failed: {email} already exists", request.Email);

                return new AuthUserDto
                {
                    Status = UserStatus.EmailAlreadyExists,
                    Message = $"User with email {request.Email} already exists."
                };
            }

            string passwordHash = passwordHasher.Hash(request.Email, request.Password);
            UserDto user = await userRepository.CreateUser(request, passwordHash, ct);
            logger.LogInformation("User {userId} registered", user.Id);

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
                logger.LogWarning("Update failed: user {userId} not found", userId);
                return new AuthUserDto
                {
                    Status = UserStatus.UserNotFound,
                    Message = $"User with id {userId} was not found."
                };
            }

            logger.LogInformation("User {userId} updated", userId);

            return new AuthUserDto
            {
                Status = UserStatus.Updated,
                Responce = user
            };
        }
    }
}
