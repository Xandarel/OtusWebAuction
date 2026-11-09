using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.DB.Models;
using Riok.Mapperly.Abstractions;

namespace OtusWebAuction.DB.Mapper
{
    [Mapper(AllowNullPropertyAssignment = false)]
    public static partial class CategoryMapper
    {
        [MapperIgnoreTarget(nameof(Category.ParentId))]
        [MapperIgnoreTarget(nameof(Category.ParentCategory))]
        [MapperIgnoreTarget(nameof(Category.IsActive))]
        public static partial CategoryDto ToCategoryDto(Category category);
    }
}
