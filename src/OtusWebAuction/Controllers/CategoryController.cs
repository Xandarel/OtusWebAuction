using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Catrgory.Create;
using OtusWebAuction.BL.Catrgory.Dto;
using OtusWebAuction.BL.Interfaces.Categories;
using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(ICategoryHandler categoryHandler, ILogger<CategoryController> logger) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyCollection<CategoryDto>>> GetAllPerentCategory(CancellationToken ct)
        {
            CategoryHandlerDto<IReadOnlyCollection<CategoryDto>> responce = await categoryHandler.GetAllParentCategory(ct);
            logger.LogInformation("Get all parent category responce: {responce}", responce.Status);
            return responce.Status switch
            {
                HandlerStatus.Success => Ok(responce.Responce),
                HandlerStatus.Error => BadRequest(responce.Responce),
                _ => BadRequest(responce.Responce),
            };
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<CategoryDto>> Get([FromRoute] int id, CancellationToken ct)
        {
            CategoryHandlerDto<CategoryDto> responce = await categoryHandler.GetCategoryById(id, ct);
            logger.LogInformation("Get category by id {id} with status {status}", id, responce.Status);
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
        public async Task<ActionResult> CreateCategory(CategoryRequest request, CancellationToken ct)
        {
            await categoryHandler.CreateCategory(request, ct);

            logger.LogInformation("Category created successfully.");
            return Ok();
        }
    }
}
