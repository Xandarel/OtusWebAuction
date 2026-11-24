using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtusWebAuction.BL.Interfaces;
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
    public class UserController(IUserHandler userHandler,
        JwtTokenFactory jwtTokenFactory,
        ILogger<UserController> logger) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<AuthResponse>> Register(RegisterUserRequest request, CancellationToken ct)
        {
            AuthUserDto result = await userHandler.RegisterUser(request, ct);
            if (result.Status != UserStatus.Created)
            {
                logger.LogWarning("Пользователь {user} не создан по причине {message}",
                    request.Email,
                    result.Message);

                return ToError(result);
            }

            logger.LogInformation("Пользователь {user} создан", request.Email);

            return Ok(new AuthResponse(result.Responce!, jwtTokenFactory.Create(result.Responce!)));
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserRequest request, CancellationToken ct)
        {
            AuthUserDto result = await userHandler.LoginUser(request, ct);
            if (result.Status != UserStatus.Login)
            {
                logger.LogWarning("Пользователь {user} не смог войти в систему по причине {message}",
                    request.Email,
                    result.Message);

                return ToError(result);
            }

            logger.LogInformation("Пользователь {user} авторизован", request.Email);

            return Ok(new AuthResponse(result.Responce!, jwtTokenFactory.Create(result.Responce!)));
        }

        [Authorize]
        [HttpPatch("UpdateUser")]
        public async Task<ActionResult<UserDto>> UpdateUser(UpdateUserRequest request, CancellationToken ct)
        {
            if (User.GetUserId() is not { } userId)
            {
                logger.LogWarning("UpdateUser отклонён: в токене нет корректного sub");

                return Unauthorized();
            }

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