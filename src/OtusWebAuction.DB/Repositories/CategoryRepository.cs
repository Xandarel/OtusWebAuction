using Microsoft.EntityFrameworkCore;
using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.BL.Interfaces.Categories;
using OtusWebAuction.DB.Context;
using OtusWebAuction.DB.Mapper;

namespace OtusWebAuction.DB.Repositories
{
    public class CategoryRepository(AuctionDbContext dbContext) : ICategoryRepository
    {
        public async Task<int> CreateCategory(CategoryRequest request, CancellationToken ct)
        {
            var category = CategoryMapper.ToCategory(request);
            await dbContext.Categories.AddAsync(category, ct);
            await dbContext.SaveChangesAsync(ct);
            return category.Id;
        }

        public async Task<IReadOnlyCollection<CategoryDto>> GetAllParentCategory(CancellationToken ct)
        {
            return await dbContext.Categories
                .AsNoTracking()
                .Where(x => x.IsActive && x.ParentId == null)
                .Select(x => CategoryMapper.ToCategoryDto(x))
                .ToListAsync(ct);
        }

        public async Task<CategoryDto?> GetById(int id, CancellationToken ct)
        {
            var item = await dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive, ct);

            return item is null ? null : CategoryMapper.ToCategoryDto(item);
        }
    }
}
