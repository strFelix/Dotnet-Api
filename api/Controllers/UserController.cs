using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Http.Exceptions;
using api.Dtos;
using api.Services.UserServices;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            List<UserDto> users = await _userService.GetAllAsync();
            return Ok(users);
        }
        catch (BaseException ex)
        {
            return ex.GetResponse();
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        try
        {
            UserDto user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        catch (BaseException ex)
        {
            return ex.GetResponse();
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(User))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostUserAsync(UserDto userDto)
    {
        try
        {
            await _userService.PostUserAsync(userDto);
            return Created(nameof(UserController), userDto);
        }
        catch (BaseException ex)
        {
            return ex.GetResponse();
        }
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutUserAsync(UserDto userDto, int id)
    {
        try
        {
            await _userService.PutUserAsync(id, userDto);
            return NoContent();
        }
        catch (BaseException ex)
        {
            return ex.GetResponse();
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        try
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
        catch (BaseException ex)
        {
            return ex.GetResponse();
        }
    }

}

