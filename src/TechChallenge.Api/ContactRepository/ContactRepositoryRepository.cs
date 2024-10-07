using TechChallenge.Api.Interfaces;
using TechChallenge.Api.Models;

namespace TechChallenge.Api.Services;

public class ContactRepositoryRepository : IContactRepository
{
    public IEnumerable<Controllers.Contact> GetContactsAsync()
    {
        //throw new NotImplementedException();
        IEnumerable<Controllers.Contact> contactList = new List<Controllers.Contact>();
        return  contactList;
    }

    public Task<Contact> GetContactByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Contact> AddContactAsync(Contact contact)
    {
        throw new NotImplementedException();
    }

    public void UpdateContact(Contact contact)
    {
        throw new NotImplementedException();
    }

    public void RemoveContact(int id)
    {
        throw new NotImplementedException();
    }
}