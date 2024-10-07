using System.Collections.Immutable;
using TechChallenge.Api.Models;

namespace TechChallenge.Test;

public class CheckValidContacts
{
    [Theory]
    [InlineData(null,"Silva","silva@gmail.com", true)]
    [InlineData("João","Silva","silva@gmail.com", false)]
    public void ReturnValidationForFirstNameIfIsNullOrNot(string firstName, string lastName, string email, bool isValid)
    {
        var contact = new Contact(firstName, lastName, email);
        
        var IsValidname = contact.FirstName == null;
        
        Assert.Equal(IsValidname, isValid);
    }

    [Theory]
    [InlineData("Maria", null, "silva@gmail.com", true)]
    [InlineData("João", "Silva", "silva@gmail.com", false)]
    public void ReturnValidationForLastNameIfIsNullOrNot(string firstName, string lastName, string email, bool isValid)
    {
        var contact = new Contact(firstName, lastName, email);

        var isValidEmail = contact.FirstName == null;
        Assert.Equal(isValidEmail, isValid);
    }
    
    [Theory]
    [InlineData("Maria", "Silva", null, true)]
    [InlineData("João", "Silva", "silva@gmail.com", false)]
    public void ReturnValidationForEmailIsNullOrNot(string firstName, string lastName, string email, bool isValid)
    {
        var contact = new Contact(firstName, lastName, email);
        
        var IsValidEmail = contact.Email == null;
        
        Assert.Equal(IsValidEmail, isValid);
    }
    

}