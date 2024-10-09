using System.Data;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using TechChallenge.Api.Interfaces;
using TechChallenge.Api.Models;

namespace TechChallenge.Api.Services;

public class ContactRepository : IContactRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public ContactRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<ContactResponse<IEnumerable<Contact>>>  GetAllContactsAsync()
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var contacts = await db.GetAllAsync<Contact>();
            return new ContactResponse<IEnumerable<Contact>>
            {
                Success = true,
                Message = "List of contacts.",
                Data = contacts
            };
        }
    }

    public async Task<ContactResponse<Contact>> GetContactByIdAsync(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
           var contact = await db.GetAsync<Contact>(id);
           if (contact == null)
           {
               return new ContactResponse<Contact>
               {
                   Success = false,
                   Message = $"No contact was found for the given id"
               };              
           }
           
           return new ContactResponse<Contact>
           {
               Success = true,
               Message = "Contact founded",
               Data = contact
           };
        }
    }

    public async Task<ContactResponse<Contact>> AddContactAsync(Contact contact)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            await db.InsertAsync(contact);
            return new ContactResponse<Contact>
            {
                Success = true,
                Message = "Contact was successfully added",
                Data = contact
            };
        }
    }

    public async Task<ContactResponse<Contact>> UpdateContact(Contact contact)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var existingContact = await db.GetAsync<Contact>(contact.Id);
            if (existingContact == null)
            {
                return new ContactResponse<Contact>
                {
                    Success = false,
                    Message = $"No contact was found for the given {contact.Id}"
                };                
            }
            await db.UpdateAsync(contact);

            return new ContactResponse<Contact>
            {
                Success = true,
                Message = "Contact successfully updated.",
                Data = contact
                
            };
        }
    }

    public async Task<ContactResponse<object>> RemoveContact(int id)
    {
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var contact = await db.GetAsync<Contact>(id);
            if (contact == null)
            {
                return new ContactResponse<object>
                {
                    Success = false,
                    Message = $"No contact was found with ID {id} to delete."
                };
            }

            await db.DeleteAsync(contact);
            
            return new ContactResponse<object>
            {
                Success = true,
                Message = "Contact successfully deleted."
            };


            
        }
    }
}