using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restore.API.Extensions;
using Restore.Common.DTOs;
using Restore.Common.Utilities;


namespace Restore.API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("not-found")]
    public ActionResult<object> GetNotFound()
    {
        var result = Result.NotFound<object>("Test - Not Found Error");
        return this.ToActionResult(result);
    }

    [HttpGet("bad-request")]
    public ActionResult<object> GetBadRequest()
    {
        //return BadRequest("This is not a good request");
        //throw new BusinessException("This is not a good request");
        var result = Result.BadRequest<object>("This is not a good request");
        return this.ToActionResult(result);
    }

    [HttpGet("unauthorized")]
    public ActionResult<object> GetUnauthorised()
    {
        //return Unauthorized();
        //throw new UnauthorizedException("Unauthorized");
        var result = Result.InvalidCredentials<object>("Test - Unauthorized Error");
        return this.ToActionResult(result);
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

