using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OtusWebAuction.BL.Interfaces.User;
using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.BL.Users.Update;
using OtusWebAuction.DB.Context;
using OtusWebAuction.DB.Mapper;
using OtusWebAuction.DB.Models;

namespace OtusWebAuction.DB.Repositories
{
    public sealed class UserRepository(AuctionDbContext dbContext, ILogger<UserRepository> logger) : IUserRepository
    {
        public async Task<UserDto> CreateUser(RegisterUserRequest request, string passwordHash, CancellationToken ct)
        {
            var now = DateTime.UtcNow;
            User user = UserMapper.ToUser(request);
            user.Password = passwordHash;
            user.CreatedDate = now;
            user.LastUpdatedDate = now;
            await dbContext.Users.AddAsync(user, ct);
            await dbContext.SaveChangesAsync(ct);
            logger.LogInformation("User created successfully with email {email}.", user.Email);
            return UserMapper.ToUserDto(user);
        }
        public async Task<UserDto?> UpdateUser(Guid userId, UpdateUserRequest request, CancellationToken ct)
        {
            User? user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);
            if (user is null)
            {
                logger.LogWarning("Update failed: user with id {userId} not found.", userId);
                return null;
            }

            UserMapper.Apply(request, user);
            user.LastUpdatedDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(ct);
            return UserMapper.ToUserDto(user);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);
            if (user is null)
            {
                logger.LogWarning("GetByIdAsync failed: user with id {userId} not found.", id);
                return null;
            }

            return UserMapper.ToUserDto(user);
        }

        public async Task<LoginUserDto?> GetUserByEmail(string email, CancellationToken ct)
        {
            var user = await dbContext.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, ct);

            if (user is null)
            {
                logger.LogWarning("GetUserByEmail failed: user with email {email} not found.", email);
                return null;
            }

            return UserMapper.ToLoginUserDto(user);
        }
    }
}
