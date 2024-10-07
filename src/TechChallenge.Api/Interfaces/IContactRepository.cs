using TechChallenge.Api.Models;

namespace TechChallenge.Api.Interfaces;

public interface IContactRepository
{
    public IEnumerable<Controllers.Contact> GetContactsAsync();
    public Task<Contact> GetContactByIdAsync(int id);
    public Task<Contact> AddContactAsync(Contact contact);
    public void UpdateContact(Contact contact);
    public void RemoveContact(int id);
}