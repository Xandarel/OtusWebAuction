using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Catrgory;
using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(CategoryHandler categoryHandler) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyCollection<CategoryDto>>> GetAllPerentCategory(CancellationToken ct)
        {
            CategoryHandlerDto<IReadOnlyCollection<CategoryDto>> responce = await categoryHandler.GetAllParentCategory(ct);

            return responce.Status switch
            {
                HandlerStatus.Success => Ok(responce.Responce),
                HandlerStatus.Error => BadRequest(responce.Responce),
                _ => BadRequest(responce.Responce),
            };
        }

        [HttpGet("{id: int}")]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> Get([FromRoute] int id, CancellationToken ct)
        {
            CategoryHandlerDto<CategoryDto> responce = await categoryHandler.GetCategoryById(id, ct);

            return responce.Status switch
            {
                HandlerStatus.Success => Ok(responce.Responce),
                HandlerStatus.Error => BadRequest(responce.Responce),
                _ => BadRequest(responce.Responce),
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyCollection<CategoryDto>>> GetAllSubcategory(int parentId) => throw new NotImplementedException();

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Admin))]
        public async Task<ActionResult> CreateCategory(CategoryRequest request)
        {
            await categoryHandler.CreateCategory(request);

            return Ok();
        }
    }
}
