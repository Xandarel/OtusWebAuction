using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OtusWebAuction.BL.Interfaces.Lot;
using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;
using OtusWebAuction.DB.Context;
using OtusWebAuction.DB.Mapper;
using OtusWebAuction.DB.Models;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.DB.Repositories
{
    public sealed class LotRepository(AuctionDbContext dbContext, ILogger<LotRepository> logger) : ILotRepository
    {
        public async Task<LotDto?> GetByIdAsync(long id, CancellationToken ct)
        {
            var lot = await dbContext.Lots
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                logger.LogWarning("Lot with id {id} not found", id);
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
            logger.LogInformation("Lot created successfully with id {id} for seller {sellerId}", lot.Id, sellerId);
            return LotMapper.ToLotDto(lot);
        }

        public async Task<LotDto?> UpdateLotAsync(long id, UpdateLotRequest request, CancellationToken ct)
        {
            Lot? lot = await dbContext.Lots
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            if (lot is null)
            {
                logger.LogWarning("UpdateLot отклонён: лот с id {id} не найден", id);
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
                logger.LogWarning("DeleteLot отклонён: лот с id {id} не найден", id);
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
                logger.LogWarning("SendToModerate отклонён: лот с id {id} не найден", id);
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
                logger.LogWarning("ApproveModerate отклонён: лот с id {id} не найден", id);
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
                logger.LogWarning("RejectLot отклонён: лот с id {id} не найден", id);
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
