using Microsoft.AspNetCore.Mvc;
using System;

[Route("api/[controller]")]
[ApiController]
public class ErrorHandlingController : ControllerBase
{
    [HttpGet("division")]
    public IActionResult GetDivisionResult(int numerator, int denominator)
    {
        try
        {
            var result = numerator / denominator;
            return Ok("Here is the result: " + result);

        }
        catch (DivisionByZeroException)
        {
            Console.writeLine("Error: Division by Zero is not allowed.");
            return BadRequest("Cannot divide by zero.");
        }
    }
}