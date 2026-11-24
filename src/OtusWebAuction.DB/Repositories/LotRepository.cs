using Microsoft.EntityFrameworkCore;
using OtusWebAuction.BL.Interfaces.Lot;
using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;
using OtusWebAuction.DB.Context;
using OtusWebAuction.DB.Mapper;
using OtusWebAuction.DB.Models;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.DB.Repositories
{
    public sealed class LotRepository(AuctionDbContext dbContext) : ILotRepository
    {
        public async Task<LotDto?> GetByIdAsync(long id, CancellationToken ct)
        {
            var lot = await dbContext.Lots
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                return null;
            }

            return LotMapper.ToLotDto(lot);
        }

        // Удалённые лоты отсекаются глобальным query filter из LotConfiguration.
        public Task<bool> ExistsAsync(long id, CancellationToken ct) =>
            dbContext.Lots.AnyAsync(x => x.Id == id, ct);

        public async Task<LotDto> CreateLotAsync(CreateLotRequest request, Guid sellerId, CancellationToken ct)
        {
            DateTime now = DateTime.UtcNow;
            Lot lot = LotMapper.ToLot(request);
            lot.SellerId = sellerId;
            lot.Status = LotStatus.Draft;
            lot.CreatedDate = now;
            lot.LastUpdatedDate = now;
            await dbContext.Lots.AddAsync(lot, ct);
            await dbContext.SaveChangesAsync(ct);

            return LotMapper.ToLotDto(lot);
        }

        public async Task<LotDto?> UpdateLotAsync(long id, UpdateLotRequest request, CancellationToken ct)
        {
            Lot? lot = await dbContext.Lots
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                return null;
            }

            LotMapper.Apply(request, lot);
            lot.LastUpdatedDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(ct);

            return LotMapper.ToLotDto(lot);
        }

        public async Task<bool> DeleteLotAsync(long id, CancellationToken ct)
        {
            Lot? lot = await dbContext.Lots
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                return false;
            }

            lot.DeleteDate = DateTime.UtcNow;
            lot.LastUpdatedDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(ct);

            return true;
        }

        public async Task<LotDto?> SendToModerateAsync(long id, CancellationToken ct)
        {
            Lot? lot = await dbContext.Lots
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                return null;
            }

            lot.ModerationStatus = ModerationStatus.Moderating;
            lot.LastUpdatedDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(ct);

            return LotMapper.ToLotDto(lot);
        }

        public async Task<LotDto?> ApproveModerate(long id, CancellationToken ct)
        {
            Lot? lot = await dbContext.Lots
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                return null;
            }

            lot.ModerationStatus = ModerationStatus.Approved;
            lot.LastUpdatedDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(ct);

            return LotMapper.ToLotDto(lot);
        }

        public async Task<LotDto?> RejectLotAsync(long id, CancellationToken ct)
        {
            Lot? lot = await dbContext.Lots
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                return null;
            }

            lot.ModerationStatus = ModerationStatus.Rejected;
            lot.LastUpdatedDate = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(ct);

            return LotMapper.ToLotDto(lot);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId, CancellationToken ct) =>
            await dbContext.Categories.AnyAsync(x => x.Id == categoryId, ct);
    }
}
