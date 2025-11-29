/* using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restore.Common.Exceptions;

namespace Restore.API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("not-found")]
    public IActionResult GetNotFound()
    {
        throw new NotFoundException("Not Found");
        //return NotFound();
    }

    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        //return BadRequest("This is not a good request");
        throw new BusinessException("This is not a good request");
    }

    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorised()
    {
        //return Unauthorized();
        throw new UnauthorizedException("Unauthorized");
    }

    [HttpGet("validation-error")]
    public IActionResult GetValidationError()
    {
        ModelState.AddModelError("Problem1", "This is the first error");
        ModelState.AddModelError("Problem2", "This is the second error");
        return Ok("Should not reach here");
        //return ValidationProblem();
    }
    [HttpPost("validation-error")]
    public IActionResult PostValidationError(UserDto userDto)
    {
        return Ok("Should not reach here");
        //return ValidationProblem();
    }

    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
        throw new Exception("This is a server error");
    }
}



public class UserDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Name { get; set; } = default!;
}

 */