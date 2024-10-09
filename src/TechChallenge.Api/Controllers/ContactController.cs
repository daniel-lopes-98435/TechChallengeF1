using System.Runtime.Versioning;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge.Api.Interfaces;
using TechChallenge.Api.Loggin;
using TechChallenge.Api.Models;


namespace TechChallenge.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;
    private readonly ILogger<ContactController> _logger;
    
    public ContactController(
        IContactRepository contactRepository, 
        ILogger<ContactController> logger)
    {
        _contactRepository = contactRepository;
        _logger = logger;
    }

    [HttpGet]
    [Route("/contact-list")]
    public  async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
    {
        CustomLogger.ToFile = true;
        _logger.LogInformation("Information test log");

        var response = await _contactRepository.GetAllContactsAsync();
        
        if (!response.Success)
        {
            return NotFound(response); 
        }
        else
        {
            return Ok(response);
             
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Contact>> GetContactById(int id)
    {
       var response = await _contactRepository.GetContactByIdAsync(id);
        if (!response.Success)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    
    [HttpPost]
    [Route("/contact-add")]
    public async Task<ActionResult<Contact>> AddContact(Contact contact)
    {
        var response = await _contactRepository.AddContactAsync(contact);
        if (!response.Success)
        {
            return BadRequest(JsonSerializer.Serialize(response));
        }
        
        return CreatedAtAction(nameof(GetContactById), new { id = response.Data.Id }, response);
    }

    [HttpPut]
    [Route("/contact-update")]
    public async Task<ActionResult<Contact>> UpdateContact(int id, Contact contact)
    {
        if (id != contact.Id)
        {
            return BadRequest(new ContactResponse<Contact>
            {
                Success = false,
                Message = $"There is no Contact  Id {id} informed is diferent from the Id {contact.Id} in the payload."
            });
        }
        var response = await _contactRepository.UpdateContact(contact);
        if (!response.Success)
        {
            return NotFound(response); 
        }
        else
        {
            return Ok(response);
             
        }
    }

    [HttpDelete]
    [Route("/contact-delete")]
    public async Task<ActionResult<Contact>> DeleteContact(int id)
    {
         var response = await _contactRepository.RemoveContact(id);
         if (!response.Success)
         {
             return NotFound(response); 
         }
         else
         {
             return Ok(response);
             
         }
    }
}

