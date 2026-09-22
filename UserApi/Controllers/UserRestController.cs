using Microsoft.AspNetCore.Mvc;
using UserApi.Dtos;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers;


[ApiController]
[Route("api/users")]
public class UserRestController(UserService userService) : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterForm form)
    {
        try
        {
            var user = userService.AddUser(form);
            return CreatedAtAction(
                nameof(Register),
                new { username = user.Username },
                ToResponse(user));
        }
        catch (ArgumentNullException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException)
        {
            return Conflict(new { message = "Username already taken." });
        }
    }

    [HttpPut]
    public IActionResult Update([FromBody] UserUpdateForm form)
    {
        try
        {
            var user = userService.UpdateUser(form);
            if (user is null) return NotFound();
            return Ok(ToResponse(user));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] UserDeleteForm form)
    {
        try
        {
            userService.DeleteUser(form);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginForm form)
    {
        var user = userService.Login(form);
        if (user is null) return Unauthorized();
        return Ok(ToResponse(user));
    }

    private static object ToResponse(User user) => new
    {
        user.Username,
        user.Age
    };
}