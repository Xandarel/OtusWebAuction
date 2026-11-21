using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;
using OtusWebAuction.DB.Models;
using Riok.Mapperly.Abstractions;

namespace OtusWebAuction.DB.Mapper
{
    [Mapper(AllowNullPropertyAssignment = false)]
    public static partial class LotMapper
    {
        [MapperIgnoreTarget(nameof(Lot.Id))]
        [MapperIgnoreTarget(nameof(Lot.SellerId))]
        [MapperIgnoreTarget(nameof(Lot.Status))]
        [MapperIgnoreTarget(nameof(Lot.ModerationStatus))]
        [MapperIgnoreTarget(nameof(Lot.CreatedDate))]
        [MapperIgnoreTarget(nameof(Lot.LastUpdatedDate))]
        [MapperIgnoreTarget(nameof(Lot.DeleteDate))]
        [MapperIgnoreTarget(nameof(Lot.Category))]
        [MapperIgnoreTarget(nameof(Lot.User))]
        [MapperIgnoreTarget(nameof(Lot.Bets))]
        public static partial Lot ToLot(CreateLotRequest request);

        [MapperIgnoreTarget(nameof(Lot.Id))]
        [MapperIgnoreTarget(nameof(Lot.SellerId))]
        [MapperIgnoreTarget(nameof(Lot.Status))]
        [MapperIgnoreTarget(nameof(Lot.ModerationStatus))]
        [MapperIgnoreTarget(nameof(Lot.CreatedDate))]
        [MapperIgnoreTarget(nameof(Lot.LastUpdatedDate))]
        [MapperIgnoreTarget(nameof(Lot.DeleteDate))]
        [MapperIgnoreTarget(nameof(Lot.Category))]
        [MapperIgnoreTarget(nameof(Lot.User))]
        [MapperIgnoreTarget(nameof(Lot.Bets))]
        public static partial void Apply(UpdateLotRequest request, Lot lot);

        [MapperIgnoreSource(nameof(Lot.LastUpdatedDate))]
        [MapperIgnoreSource(nameof(Lot.DeleteDate))]
        [MapperIgnoreSource(nameof(Lot.Bets))]
        [MapperIgnoreSource(nameof(Lot.Category))]
        [MapperIgnoreSource(nameof(Lot.User))]
        public static partial LotDto ToLotDto(Lot lot);
    }
}
