using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Users.Register;

namespace OtusWebAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<RegisterUserResponce>> Register(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
