using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Users;
using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.BL.Users.Login;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.BL.Users.Update;
using OtusWebAuction.Domain.Enums;
using OtusWebAuction.Security;

namespace OtusWebAuction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(UserHandler userHandler, JwtTokenFactory jwtTokenFactory) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterUserRequest request, CancellationToken ct)
        {
            AuthUserDto result = await userHandler.RegisterUser(request, ct);
            if (result.Status != UserStatus.Created)
            {
                return ToError(result);
            }

            return Ok(new AuthResponse(result.Responce!, jwtTokenFactory.Create(result.Responce!)));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserRequest request, CancellationToken ct)
        {
            AuthUserDto result = await userHandler.LoginUser(request, ct);
            if (result.Status != UserStatus.Login)
            {
                return ToError(result);
            }

            return Ok(new AuthResponse(result.Responce!, jwtTokenFactory.Create(result.Responce!)));
        }

        [Authorize]
        [HttpPatch("UpdateUser")]
        public async Task<ActionResult<UserDto>> UpdateUser(UpdateUserRequest request, CancellationToken ct)
        {
            var userId = Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
            AuthUserDto result = await userHandler.UpdateUser(userId, request, ct);
            return result.Status == UserStatus.Updated
                ? Ok(result.Responce)
                : ToError(result);
        }

        private ActionResult ToError(AuthUserDto result) => result.Status switch
        {
            UserStatus.EmailAlreadyExists => Conflict(result.Message),
            UserStatus.LoginFailed => Unauthorized(result.Message),
            UserStatus.UserNotFound => NotFound(result.Message),
            _ => BadRequest(result.Message)
        };
    }
}