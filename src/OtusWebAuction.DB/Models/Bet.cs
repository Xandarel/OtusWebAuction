using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtusWebAuction.DB.Models
{
    /// <summary>
    /// Ставка
    /// </summary>
    public class Bet
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required long Id { get; init; }

        /// <summary>
        /// Идентификатор лота
        /// </summary>
        public required long LotId { get; init; }

        /// <summary>
        /// Лот
        /// </summary>
        [ForeignKey("LotId")]
        public required virtual Lot Lot { get; set; }

        /// <summary>
        /// Идентификатор покупателя
        /// </summary>
        public Guid CustomerId { get; init; }

        /// <summary>
        /// Покупатель
        /// </summary>
        [ForeignKey("CustomerId")]
        public required virtual User Customer { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Значение ставки
        /// </summary>
        public decimal AmountRate { get; set; }       
    }
}
