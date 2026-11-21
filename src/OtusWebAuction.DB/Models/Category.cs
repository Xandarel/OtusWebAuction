namespace OtusWebAuction.DB.Models
{
    public class Category
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public ICollection<Category> Children { get; set; } = [];
        public required string Name { get; set; }
        public required bool IsActive { get; set; }
    }
}
