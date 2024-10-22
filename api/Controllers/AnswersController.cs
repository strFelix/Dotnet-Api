using api.Data;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnswersController : ControllerBase
{
    private readonly DataContext _context;

    public AnswersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetQuestions()
    {
        try
        {
            List<Answer> answers = await _context.Answers
                .Include(a => a.Options)
                .ToListAsync();

            return Ok(answers);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuestionById(int id)
    {
        try
        {
            Answer? answer = await _context.Answers
                .Include(a => a.Options)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (answer == null)
                return NotFound();

            return Ok(answer);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{userId}/{answerId}/{optionNumber}")]
    public async Task<IActionResult> PostReponse(int userId, int answerId, int optionNumber)
    {
        try
        {
            Answer? answer = await _context.Answers
                .Include(a => a.Options)
                .FirstOrDefaultAsync(a => a.Id == answerId);

            UserResponse response = new UserResponse
            {
                UserId = userId,
                AnswerId = answerId
            };
            
            if (optionNumber == answer?.CorrectOption)
                response.IsCorrect = true;
            else
                response.IsCorrect = false;

            await _context.UserResponses.AddAsync(response);
            await _context.SaveChangesAsync();

            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}


