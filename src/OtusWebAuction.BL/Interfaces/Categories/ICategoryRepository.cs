using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;

namespace OtusWebAuction.BL.Interfaces.Categories
{
    public interface ICategoryRepository
    {
        Task<int> CreateCategory(CategoryRequest request, CancellationToken ct);
        Task<IReadOnlyCollection<CategoryDto>> GetAllParentCategory(CancellationToken ct);
        Task<CategoryDto?> GetById(int id, CancellationToken ct);
    }
}
