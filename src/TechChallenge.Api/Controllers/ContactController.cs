using Microsoft.AspNetCore.Mvc;
using TechChallenge.Api.Interfaces;
using TechChallenge.Api.Models;


namespace TechChallenge.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    //private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactController> _logger;
    
    public ContactController(
        //IContactRepository contactRepository, 
        ILogger<ContactController> logger)
    {
        //_contactRepository = contactRepository;
        _logger = logger;
    }

    [HttpGet]
    public  async Task<ActionResult<IEnumerable<Contact>>> GetAllContactsx()
    {
        _logger.LogInformation("Information test log");
        var contactList = new List<Contact>();
        
        var contact = new Contact
        {
            Id = 1,
            FirstName = "José",
            LastName = "Silva",
            Email = "jos.silva@test.com",
        
        };
        contactList.Add(contact);
        
        
        return Ok(contactList);
    }
}

