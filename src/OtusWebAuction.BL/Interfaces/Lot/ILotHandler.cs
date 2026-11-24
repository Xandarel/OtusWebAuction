using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;

namespace OtusWebAuction.BL.Interfaces.Lot
{
    public interface ILotHandler
    {
        Task<LotResultDto> ApproveModerate(long id, CancellationToken ct);
        Task<LotResultDto> CreateLot(CreateLotRequest request, Guid sellerId, CancellationToken ct);
        Task<LotResultDto> DeleteLot(long id, Guid sellerId, CancellationToken ct);
        Task<LotResultDto> GetLot(long id, CancellationToken ct);
        Task<LotResultDto> RejectLot(long id, CancellationToken ct);
        Task<LotResultDto> SendToModerate(long id, Guid sellerId, CancellationToken ct);
        Task<LotResultDto> UpdateLot(long id, UpdateLotRequest request, Guid sellerId, CancellationToken ct);
    }
}