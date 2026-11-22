using Microsoft.Extensions.Logging;
using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.BL.Interfaces.Categories;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Catrgory
{
    public sealed class CategoryHandler(ICategoryRepository repository, ILogger<CategoryHandler> logger) : ICategoryHandler
    {
        public async Task CreateCategory(CategoryRequest request, CancellationToken ct) => await repository.CreateCategory(request, ct);
        public Task<CategoryHandlerDto<IReadOnlyCollection<CategoryDto>>> GetAllParentCategory(CancellationToken ct)
            => ExecuteAsync(repository.GetAllParentCategory, ct);
        public Task<CategoryHandlerDto<CategoryDto>> GetCategoryById(int id, CancellationToken ct)
            => ExecuteAsync(ct => repository.GetById(id, ct), ct);

        private async Task<CategoryHandlerDto<T>> ExecuteAsync<T>(
            Func<CancellationToken, Task<T?>> action,
            CancellationToken ct) where T : class
        {
            try
            {
                T? response = await action(ct);
                logger.LogInformation("CategoryHandler executed succes");
                return new CategoryHandlerDto<T>
                {
                    Status = HandlerStatus.Success,
                    Message = string.Empty,
                    Responce = response
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CategoryHandler executed with error");
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
