using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Interfaces.Lot;
using OtusWebAuction.BL.Lots.Dto;
using OtusWebAuction.BL.Lots.Request;
using OtusWebAuction.Domain.Enums;
using OtusWebAuction.Security;

namespace OtusWebAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LotController(ILotHandler lotHandler, ILogger<LotController> logger) : ControllerBase
    {
        [Authorize]
        [HttpPost("CreateLot")]
        public async Task<IActionResult> CreateLot(CreateLotRequest request, CancellationToken ct)
        {
            if (User.GetUserId() is not { } sellerId)
            {
                logger.LogWarning("CreateLot отклонён: в токене нет корректного sub");
                return Unauthorized();
            }

            LotResultDto result = await lotHandler.CreateLot(request, sellerId, ct);
            return result.Status switch
            {
                LotResultStatus.Created => StatusCode(StatusCodes.Status201Created, result.Response),
                _ => ToError(result)
            };
        }

        [Authorize]
        [HttpPatch("UpdateLot/{id:long}")]
        public async Task<IActionResult> UpdateLot(long id, UpdateLotRequest request, CancellationToken ct)
        {
            if (User.GetUserId() is not { } sellerId)
            {
                logger.LogWarning("UpdateLot отклонён: в токене нет корректного sub");
                return Unauthorized();
            }

            LotResultDto result = await lotHandler.UpdateLot(id, request, sellerId, ct);
            return result.Status switch
            {
                LotResultStatus.Updated => Ok(result.Response),
                _ => ToError(result)
            };
        }

        [Authorize]
        [HttpDelete("DeleteLot/{id:long}")]
        public async Task<IActionResult> DeleteLot(long id, CancellationToken ct)
        {
            if (User.GetUserId() is not { } sellerId)
            {
                logger.LogWarning("DeleteLot отклонён: в токене нет корректного sub");
                return Unauthorized();
            }

            LotResultDto result = await lotHandler.DeleteLot(id, sellerId, ct);
            return result.Status switch
            {
                LotResultStatus.Deleted => NoContent(),
                _ => ToError(result)
            };
        }

        [Authorize]
        [HttpPost("SendToModerate/{id:long}")]
        public async Task<IActionResult> SendToModerate(long id, CancellationToken ct)
        {
            if (User.GetUserId() is not { } sellerId)
            {
                logger.LogWarning("SendToModerate отклонён: в токене нет корректного sub");
                return Unauthorized();
            }

            LotResultDto result = await lotHandler.SendToModerate(id, sellerId, ct);
            return result.Status switch
            {
                LotResultStatus.SentToModerate => Ok(result.Response),
                _ => ToError(result)
            };
        }

        [Authorize]
        [HttpPost("ApproveModerate/{id:long}")]
        public async Task<IActionResult> ApproveModerate(long id, CancellationToken ct)
        {
            LotResultDto result = await lotHandler.ApproveModerate(id, ct);
            return result.Status switch
            {
                LotResultStatus.Approved => Ok(result.Response),
                _ => ToError(result)
            };
        }

        [Authorize]
        [HttpGet("GetLot/{id:long}")]
        public async Task<IActionResult> GetLot(long id, CancellationToken ct)
        {
            LotResultDto result = await lotHandler.GetLot(id, ct);
            return result.Status switch
            {
                LotResultStatus.Found => Ok(result.Response),
                _ => ToError(result)
            };
        }

        [Authorize]
        [HttpPost("RejectLot/{id:long}")]
        public async Task<IActionResult> RejectLot(long id, CancellationToken ct)
        {
            LotResultDto result = await lotHandler.RejectLot(id, ct);
            return result.Status switch
            {
                LotResultStatus.Rejected => Ok(result.Response),
                _ => ToError(result)
            };
        }

        private ObjectResult ToError(LotResultDto result) => result.Status switch
        {
            LotResultStatus.NotFound => NotFound(result.Message),
            LotResultStatus.NotOwner => StatusCode(StatusCodes.Status403Forbidden, result.Message),
            _ => BadRequest(result.Message)
        };
    }
}
