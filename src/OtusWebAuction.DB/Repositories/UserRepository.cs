using OtusWebAuction.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OtusWebAuction.DB.Repositories
{
    // Infrastructure — реализация, ссылается на Application
    public sealed class UserRepository() : IUserRepository
    {
        public Task GetByIdAsync(Guid id, CancellationToken ct) => throw new NotImplementedException();
    }
}
