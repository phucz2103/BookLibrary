using BookLibrary.API.Features.Auth.ForgotPassword;
using BookLibrary.API.Features.Auth.GetRefreshToken;
using BookLibrary.API.Features.Auth.Login;
using BookLibrary.API.Features.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return result ? Ok("Đăng ký tài khoản thành công!") : BadRequest("Đăng ký tài khoản thất bại!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> RefresNewToken([FromBody] RefreshTokenCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("request-reset-password")]
        public async Task<IActionResult> RequestOTP([FromBody] RequestResetPasswordCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return result ==true ? Ok("thông báo đã được gửi về email của bạn") : BadRequest("Lỗi");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { success = false, message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand request)
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(new { success = true, message = result });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { success = false, message = ex.Message });
            }
        }
    }
}
