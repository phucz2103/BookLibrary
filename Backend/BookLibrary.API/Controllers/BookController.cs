using BookLibrary.API.Features.BookManagement;
using BookLibrary.API.Features.ListBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BookLibrary.API.Controllers
{
    [Route("book/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("list-book")]
        public async Task<IActionResult> getListBook(CancellationToken cancellation)
        {
            try
            {
                var result = await _mediator.Send(new GetListBookCommand());
                return Ok(result);
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

        [HttpGet("book-detail/{bookId:int}")]
        public async Task<IActionResult> getBookById([FromRoute] int bookId, CancellationToken cancellation)
        {
            try
            {
                var result = await _mediator.Send(new GetBookDetailCommand(bookId));
                return Ok(result);
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

        [HttpPost("create-book")]
        public async Task<IActionResult> CreateBook([FromForm] CreateBookCommand request, CancellationToken cancellation)
        {
            try
            {
                var result = await _mediator.Send(request);
                return result ? Ok("Tạo sách thành công") : BadRequest("Tạo sách thất bại");
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { success = false, message = ex.Message, Innermessage = ex.InnerException });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { success = false, message = ex.Message });
            }
        }
    }
}
