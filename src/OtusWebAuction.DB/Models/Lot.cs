using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtusWebAuction.DB.Models
{
    public class Lot
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required long Id { get; init; }

        /// <summary>
        /// Наименование лота
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Подробное описание лота
        /// </summary>
        public string? Discription { get; init; }

        /// <summary>
        /// Идентификатор категории лота
        /// </summary>
        public int CategoryId { get; init; }

        /// <summary>
        /// Категория лота
        /// </summary>
        public required virtual Category Category { get; init; }

        /// <summary>
        /// Идентификатор продавца
        /// </summary>
        public Guid SellerId { get; init; }

        /// <summary>
        /// Продавец
        /// </summary>
        [ForeignKey("SellerId")]
        public required virtual User User { get; set; }

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
        public int Status { get; init; }

        /// <summary>
        /// Статус модерации лота - Одобрен / не одобрен
        /// </summary>
        public int ModerationStatus { get; init; }

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
