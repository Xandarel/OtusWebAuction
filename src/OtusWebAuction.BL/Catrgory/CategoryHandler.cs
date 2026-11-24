using Microsoft.Extensions.Logging;
using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.BL.Interfaces.Categories;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Catrgory
{
    public sealed class CategoryHandler(ICategoryRepository repository, ILogger<CategoryHandler> logger) : ICategoryHandler
    {
        public async Task CreateCategory(CategoryRequest request, CancellationToken ct)
        {
            int categoryId = await repository.CreateCategory(request, ct);
            logger.LogInformation("Category {categoryId} created", categoryId);
        }

        public Task<CategoryHandlerDto<IReadOnlyCollection<CategoryDto>>> GetAllParentCategory(CancellationToken ct)
            => ExecuteAsync(
                async token => (IReadOnlyCollection<CategoryDto>?)await repository.GetAllParentCategory(token),
                ct,
                "GetAllParentCategory");

        public Task<CategoryHandlerDto<CategoryDto>> GetCategoryById(int id, CancellationToken ct)
            => ExecuteAsync(token => repository.GetById(id, token), ct, "GetCategoryById", id);

        private async Task<CategoryHandlerDto<T>> ExecuteAsync<T>(
            Func<CancellationToken, Task<T?>> action,
            CancellationToken ct,
            string operation,
            int? categoryId = null) where T : class
        {
            try
            {
                T? response = await action(ct);
                return new CategoryHandlerDto<T>
                {
                    Status = HandlerStatus.Success,
                    Message = string.Empty,
                    Responce = response
                };
            }
            catch (Exception ex)
            {
                if (categoryId is null)
                {
                    logger.LogError(ex, "Category {operation} failed", operation);
                }
                else
                {
                    logger.LogError(ex, "Category {operation} failed for {categoryId}", operation, categoryId);
                }

                return new CategoryHandlerDto<T>
                {
                    Status = HandlerStatus.Error,
                    Message = ex.Message,
                    Responce = default
                };
            }
        }
    }
}
