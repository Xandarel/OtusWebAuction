using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Users.Register;

namespace OtusWebAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(RegisterUserHandler registerUserHandler) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<RegisterUserResponce>> Register(RegisterUserRequest request, CancellationToken ct)
        {
            try
            { 
                RegisterUserResponce responce = await registerUserHandler.RegisterUser(request, ct);
                
                return Ok(responce);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
