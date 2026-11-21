using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Lots.Dto
{
    public sealed class LotResultDto
    {
        public LotResultStatus Status { get; set; }

        public LotDto? Response { get; set; }
        public string? Message { get; set; }
    }
}
