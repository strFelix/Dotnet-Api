using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using api.Http.Exceptions;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        try
        {
            List<User> users = await _context.Users.ToListAsync();
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
            User users = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (users == null)
                return NotFound();
            return Ok(users);
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
    public async Task<IActionResult> PostUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Created();
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
    public async Task<IActionResult> PutUserAsync(User user, int id)
    {
        try
        {
            User userExists = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userExists == null)
                return NotFound();

            userExists.Name = user.Name;
            userExists.Email = user.Email;
            userExists.PhoneNumber = user.PhoneNumber;

            _context.Users.Update(userExists);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (BaseException ex)
        {
            return ex.GetResponse();
        }
    }
}

