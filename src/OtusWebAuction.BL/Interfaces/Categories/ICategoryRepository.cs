using OtusWebAuction.BL.Catrgory.Dto;

namespace OtusWebAuction.BL.Interfaces.Categories
{
    public interface ICategoryRepository
    {
        Task<IReadOnlyCollection<CategoryDto>> GetAllParentCategory(CancellationToken ct);
        Task<CategoryDto?> GetById(int id, CancellationToken ct);
    }
}
