using Microsoft.EntityFrameworkCore;
using OtusWebAuction.BL.Interfaces;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.DB.Context;
using OtusWebAuction.DB.Mapper;

namespace OtusWebAuction.DB.Repositories
{
    public sealed class UserRepository(AuctionDbContext dbContext) : IUserRepository
    {

        public async Task<RegisterUserResponce> CreateUser(RegisterUserRequest request, CancellationToken ct)
        {
            var date = DateTime.UtcNow;
            //TODO: хранить пароль как кеш
            var user = UserMapper.ToUser(request);
            user.CreatedDate = date;
            user.LastUpdatedDate = date;

            await dbContext.Users.AddAsync(user, ct);
            await dbContext.SaveChangesAsync(ct);

            return UserMapper.ToResponse(user);
        }

        public Task GetByIdAsync(Guid id, CancellationToken ct) =>
            dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

        public Task<bool> IsUserExists(string email, CancellationToken ct) =>
            dbContext.Users.AsNoTracking().Select(x => x.Email).AnyAsync(x => x == email, ct);
    }
}
