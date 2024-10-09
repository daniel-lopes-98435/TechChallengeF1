

using Dapper.Contrib.Extensions;

namespace TechChallenge.Api.Models;

/// <summary>
/// This class is used to map User Contacts
/// </summary>
public class Contact
{

    public Contact()
    {
        
    }
/// <summary>
/// This constructor is used to allow only contacts that informs FirstName, LastName and Email
/// </summary>
/// <param name="firstName">The person first name</param>
/// <param name="lastName">The person las name</param>
/// <param name="email">The person email</param>
    public Contact(string firstName, string lastName,  string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string AreaCode { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    
}