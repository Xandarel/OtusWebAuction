using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.BL.Interfaces.Categories;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Catrgory
{
    public sealed class CategoryHandler(ICategoryRepository repository)
    {
        public async Task<CategoryHandlerDto<IReadOnlyCollection<CategoryDto>>> GetAllParentCategory(CancellationToken ct)
        {
            HandlerStatus status = HandlerStatus.Success;
            string message = string.Empty;
            IReadOnlyCollection<CategoryDto>? responce = null;
            try
            {
                responce = await repository.GetAllParentCategory(ct);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                status = HandlerStatus.Error;
            }

            return new CategoryHandlerDto<IReadOnlyCollection<CategoryDto>>
            {
                Message = message,
                Status = status,
                Responce = responce
            };
        }

        public async Task<CategoryHandlerDto<CategoryDto>> GetCategoryById(int id, CancellationToken ct)
        {

            HandlerStatus status = HandlerStatus.Success;
            string message = string.Empty;
            CategoryDto? item = null;
            try
            {
                item = await repository.GetById(id, ct);
            }
            catch (Exception ex)
            {
                message = ex.Message;
                status = HandlerStatus.Error;
            }

            return new CategoryHandlerDto<CategoryDto>
            {
                Message = message,
                Status = status,
                Responce = item
            };
        }
    }
}
