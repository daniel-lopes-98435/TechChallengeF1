using TechChallenge.Api.Models;

namespace TechChallenge.Api.Interfaces;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetContactsAsync();
    Task<Contact> GetContactByIdAsync(int id);
    Task<int> AddContactAsync(Contact contact);
    Task<int> UpdateContact(Contact contact);
    Task<int>  RemoveContact(int id);
}