using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OtusWebAuction.DB.Models
{
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор родительской категории
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Родительская категория
        /// </summary>
        [ForeignKey("ParentId")]
        public required virtual Category ParentCategory { get; set; }

        /// <summary>
        /// Наименование категории
        /// </summary>
        public required string Name { get; set; }
    }
}
