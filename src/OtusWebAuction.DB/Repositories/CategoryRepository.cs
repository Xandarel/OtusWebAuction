using Microsoft.EntityFrameworkCore;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.BL.Interfaces.Categories;
using OtusWebAuction.DB.Context;
using OtusWebAuction.DB.Mapper;

namespace OtusWebAuction.DB.Repositories
{
    public class CategoryRepository(AuctionDbContext dbContext) : ICategoryRepository
    {
        public async Task<IReadOnlyCollection<CategoryDto>> GetAllParentCategory(CancellationToken ct)
        {
            List<CategoryDto> responce = new();
            try
            {
                responce = await dbContext.Categories
                .AsNoTracking()
                .Where(x => x.IsActive && x.ParentId == null)
                .Select(x => CategoryMapper.ToCategoryDto(x))
                .ToListAsync();
            }
            catch
            {
                throw;
            }

            return responce;
        }

        public async Task<CategoryDto?> GetById(int id, CancellationToken ct)
        {
            try
            {
                var item = await  dbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

                if (item is null)
                {
                    return null;
                }

                return CategoryMapper.ToCategoryDto(item);
            }
            catch
            {
                throw;
            }
        }
    }
}
