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
        [MapperIgnoreTarget(nameof(Category.ParentCategory))]
        [MapperIgnoreTarget(nameof(Category.IsActive))]
        public static partial CategoryDto ToCategoryDto(Category category);

        [MapValue(nameof(Category.IsActive), true)]
        [MapperIgnoreTarget(nameof(Category.Id))]
        public static partial Category ToCategory(CategoryRequest categoryRequest);
    }
}
