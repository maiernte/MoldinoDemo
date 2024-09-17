using Microsoft.AspNetCore.Mvc;
using TextValidator;

namespace Worker.Controllers;

[ApiController]
[Route("[controller]")]
public class TextValidationController: ControllerBase
{
    private readonly ILogger<TextValidationController> _logger;
    
    public TextValidationController(ILogger<TextValidationController> logger)
    {
        _logger = logger;
    }
    
    [HttpGet("version", Name = "version")]
    public string GetVersion()
    {
        return "1.0.0";
    }
    
    [HttpPost("count")]
    public IActionResult CountWords([FromBody] CountWordRequest request)
    {
        try
        {
            var response = TextValidation.CountWords(request);
            return Ok(response);
        }
        catch (ArgumentException e)
        {
            _logger.LogError(e, $"{nameof(TextValidationController)}::{nameof(CountWords)}");
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(TextValidationController)}::{nameof(CountWords)}");
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("isemail")]
    public IActionResult IsEmail([FromBody] CountWordRequest request)
    {
        try
        {
            var response = TextValidation.IsEmail(request.Text);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(TextValidationController)}::{nameof(IsEmail)}");
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPost("isbase64")]
    public IActionResult IsBase64([FromBody] CountWordRequest request)
    {
        try
        {
            var response = TextValidation.IsBase64Coding(request.Text);
            return Ok(response);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{nameof(TextValidationController)}::{nameof(IsBase64)}");
            return StatusCode(500, e.Message);
        }
    }
}