using TechChallenge.Api.Models;

namespace TechChallenge.Api.Interfaces;

public interface IContactRepository
{
    Task<ContactResponse<IEnumerable<Contact>>> GetAllContactsAsync();
    Task<ContactResponse<Contact>> GetContactByIdAsync(int id);
    Task<ContactResponse<Contact>> AddContactAsync(Contact contact);
    Task<ContactResponse<Contact>> UpdateContact(Contact contact);
    Task<ContactResponse<object>>  RemoveContact(int id);
}