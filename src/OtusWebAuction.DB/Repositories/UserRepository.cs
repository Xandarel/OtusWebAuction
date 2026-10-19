using Microsoft.EntityFrameworkCore;
using OtusWebAuction.BL.Interfaces;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.DB.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace OtusWebAuction.DB.Repositories
{
    public sealed class UserRepository(AuctionDbContext dbContext) : IUserRepository
    {
        public Task<RegisterUserResponce> CreateUser(RegisterUserRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task GetByIdAsync(Guid id, CancellationToken ct) => throw new NotImplementedException();

        public Task<bool> IsUserExists(string email, CancellationToken ct)
        {
            return dbContext.Users.Select(x => x.Email).AnyAsync(x => x == email);
        }

        public Task<RegisterUserResponce> RegisterUser(RegisterUserResponce user, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
