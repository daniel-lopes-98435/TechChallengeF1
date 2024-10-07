using Microsoft.AspNetCore.Mvc;
using TechChallenge.Api.Interfaces;

namespace TechChallenge.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactRepository _contactRepository;

    public ContactController(IContactRepository contactRepository)
    {
        _contactRepository = contactRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Contact>> GetContactsAsync()
    {
        return  _contactRepository.GetContactsAsync();
    }
}

public class Contact
{
}