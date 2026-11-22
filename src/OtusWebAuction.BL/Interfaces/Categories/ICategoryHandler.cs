using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;

namespace OtusWebAuction.BL.Interfaces.Categories
{
    public interface ICategoryHandler
    {
        Task CreateCategory(CategoryRequest request, CancellationToken ct);
        Task<CategoryHandlerDto<IReadOnlyCollection<CategoryDto>>> GetAllParentCategory(CancellationToken ct);
        Task<CategoryHandlerDto<CategoryDto>> GetCategoryById(int id, CancellationToken ct);
    }
}