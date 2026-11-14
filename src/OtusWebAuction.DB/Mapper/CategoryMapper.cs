using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.DB.Models;
using Riok.Mapperly.Abstractions;

namespace OtusWebAuction.DB.Mapper
{
    [Mapper(AllowNullPropertyAssignment = false)]
    public static partial class CategoryMapper
    {
        [MapperIgnoreTarget(nameof(Category.ParentId))]
        [MapperIgnoreTarget(nameof(Category.Parent))]
        [MapperIgnoreTarget(nameof(Category.IsActive))]
        public static partial CategoryDto ToCategoryDto(Category category);

        [MapValue(nameof(Category.IsActive), true)]
        [MapperIgnoreTarget(nameof(Category.Id))]
        [MapperIgnoreTarget(nameof(Category.Parent))]
        [MapProperty(nameof(CategoryRequest.Subcategories), nameof(Category.Children), Use = nameof(ToChildren))]
        public static partial Category ToCategory(CategoryRequest request);
        private static ICollection<Category> ToChildren(IReadOnlyCollection<string>? names)
        {
            if (names is null)
            {
                return [];
            }

            return names
                .Select(name => new Category { Name = name, IsActive = true })
                .ToList();
        }
    }
}
