using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RWA_Web_Api.Interfaces;

namespace RWA_Web_Api.Controllers;

[Authorize(Policy = "AdminOnly")]
[Controller]
[Route("api/[controller]/[action]")]
public class NewsletterController : ControllerBase
{
    private readonly ILogger<NewsletterController> _logger;
    private readonly INewsLetterRepository _newsLetterRepository;

    public NewsletterController(ILogger<NewsletterController> logger, INewsLetterRepository newsLetterRepository)
    {
        _logger = logger;
        _newsLetterRepository = newsLetterRepository;
    }

    [AllowAnonymous]
    [HttpPost("/newsletter/add/{email}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult AddSubscription(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest();
        }

        var result = _newsLetterRepository.AddSubscription(email);

        return result ? Ok("") : BadRequest("Email already exists.");
    }
}