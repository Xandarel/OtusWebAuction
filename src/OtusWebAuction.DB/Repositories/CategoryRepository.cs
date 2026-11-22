using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.BL.Interfaces.Categories;
using OtusWebAuction.DB.Context;
using OtusWebAuction.DB.Mapper;

namespace OtusWebAuction.DB.Repositories
{
    public class CategoryRepository(AuctionDbContext dbContext, ILogger<CategoryRepository> logger) : ICategoryRepository
    {
        public async Task CreateCategory(CategoryRequest request, CancellationToken ct)
        {
            var category = CategoryMapper.ToCategory(request);
            await dbContext.Categories.AddAsync(category, ct);
            await dbContext.SaveChangesAsync(ct);
            logger.LogInformation("Category created successfully with id {id}", category.Id);
        }

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
                logger.LogInformation("Retrieved all parent categories successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving parent categories.");
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
                    logger.LogInformation("Category with id {id} not found.", id);
                    return null;
                }

                logger.LogInformation("Category with id {id} retrieved successfully.", id);
                return CategoryMapper.ToCategoryDto(item);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving category with id {id}.", id);
                throw;
            }
        }
    }
}
