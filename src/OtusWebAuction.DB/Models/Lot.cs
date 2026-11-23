using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.DB.Models
{
    public class Lot
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; init; }

        /// <summary>
        /// Наименование лота
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Подробное описание лота
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Идентификатор категории лота
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Категория лота
        /// </summary>
        public virtual Category Category { get; init; } = null!;

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        public Guid SellerId { get; set; }

        /// <summary>
        /// Продавец
        /// </summary>
        [ForeignKey("SellerId")]
        public virtual User User { get; set; } = null!;

        /// <summary>
        /// Стартовая цена
        /// </summary>
        public decimal? StartPrice { get; set; }

        /// <summary>
        /// Цена немедленного выкупа
        /// </summary>
        public decimal? BuyoutPrice { get; set; }

        /// <summary>
        /// Минимальное повышение ставки
        /// </summary>
        public decimal? MinStep { get; set; }

        /// <summary>
        /// Статус лота
        /// </summary>
        public LotStatus Status { get; set; }

        /// <summary>
        /// Статус модерации лота. null - лот ещё не отправлен на модерацию
        /// </summary>
        public ModerationStatus? ModerationStatus { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime LastUpdatedDate { get; set; }

        /// <summary>
        /// Дата удаления
        /// </summary>
        public DateTime? DeleteDate { get; set; }

        /// <summary>
        /// Список ставок по Лоту
        /// </summary>
        public virtual List<Bet>? Bets { get; init; }
    }
}
