using System;
using System.Collections.Generic;
using System.Text;

namespace OtusWebAuction.BL.Interfaces
{
    public interface IUserRepository
    {
        Task GetByIdAsync(Guid id, CancellationToken ct);
    }
}
