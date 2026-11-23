using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Lots.Dto
{
    public class LotDto
    {
        /// <summary>Уникальный идентификатор</summary>
        public required long Id { get; init; }

        /// <summary>Наименование лота</summary>
        public required string Name { get; init; }

        /// <summary>Подробное описание лота</summary>
        public string? Description { get; init; }

        /// <summary>Категория лота</summary>
        public int CategoryId { get; init; }

        /// <summary>Продавец</summary>
        public Guid SellerId { get; init; }

        /// <summary>Стартовая цена</summary>
        public decimal StartPrice { get; init; }

        /// <summary>Цена немедленного выкупа</summary>
        public decimal? BuyoutPrice { get; init; }

        /// <summary>Минимальное повышение ставки</summary>
        public decimal? MinStep { get; init; }

        /// <summary>Статус лота</summary>
        public LotStatus Status { get; init; }

        /// <summary>Статус модерации лота. null - лот ещё не отправлен на модерацию</summary>
        public ModerationStatus? ModerationStatus { get; init; }

        /// <summary>Дата создания лота</summary>
        public DateTime CreatedDate { get; init; }
    }
}
