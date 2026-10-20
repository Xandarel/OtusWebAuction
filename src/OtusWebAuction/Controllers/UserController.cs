using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Users.Login;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.Domain.Enums;
using OtusWebAuction.Security;

namespace OtusWebAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(RegisterUserHandler registerUserHandler, JwtTokenFactory jwtTokenFactory) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterUserRequest request, CancellationToken ct)
        {
            RegisterUserResult result = await registerUserHandler.RegisterUser(request, ct);
            return result.Status switch
            {
                RegisterUserStatus.Created => Ok(new AuthResponse(
                    result.Responce!,
                    jwtTokenFactory.Create(result.Responce!))),
                _ => BadRequest(result.Message)
            };

        }

        public async Task<ActionResult<AuthResponse>> Login(LoginUserRequest request, CancellationToken ct) =>
            // Implement login logic here
            // For example, validate user credentials and return JWT token if valid
            BadRequest("Login not implemented yet.");

    }
}