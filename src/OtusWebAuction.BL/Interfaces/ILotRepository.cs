using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;

namespace OtusWebAuction.BL.Interfaces
{
    public interface ILotRepository
    {
        Task<LotDto?> GetByIdAsync(long id, CancellationToken ct);
        Task<bool> ExistsAsync(long id, CancellationToken ct);
        Task<LotDto> CreateLotAsync(CreateLotRequest request, Guid sellerId, CancellationToken ct);
        Task<LotDto?> UpdateLotAsync(long id, UpdateLotRequest request, CancellationToken ct);
        Task<bool> DeleteLotAsync(long id, CancellationToken ct);
        Task<LotDto?> SendToModerateAsync(long id, CancellationToken ct);
        Task<LotDto?> ApproveModerate(long id, CancellationToken ct);
        Task<LotDto?> RejectLotAsync(long id, CancellationToken ct);
        Task<bool> CategoryExistsAsync(int categoryId, CancellationToken ct);
    }
}
