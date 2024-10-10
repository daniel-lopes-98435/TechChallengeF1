using System.Xml.XPath;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TechChallenge.Api.Controllers;
using TechChallenge.Api.Interfaces;
using TechChallenge.Api.Models;

namespace TechChallenge.Test;

public class ContactControllerTests
{

    
    private readonly Mock<IContactRepository> _mockRepository;
    private readonly Mock<ILogger<ContactController>> _mockLogger;
    private readonly ContactController _controller;

    public ContactControllerTests()
    {
        _mockRepository = new Mock<IContactRepository>();
        _mockLogger = new Mock<ILogger<ContactController>>();
        _controller = new ContactController(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetContactById_ReturnsOk_WhenContactExists()
    {
        
        //Arrange
        int contactId = 1;
        var contact = new Contact
        {
            Id = contactId,
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "12",
            Phone = "912345678",
            Email = "johndoe@gmail.com",
        };
        var contactResponse = new ContactResponse<Contact>
            { Success = true, Message = "Contact found", Data = contact };

        _mockRepository.Setup(repo => repo.GetContactByIdAsync(contactId)).ReturnsAsync(contactResponse);
        
        //Act
        var result = await _controller.GetContactById(contactId);
        
        //Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<Contact>>(okResult.Value);
        Assert.True(returnValue.Success);
        Assert.Equal(contactId, returnValue.Data.Id );
    }

    [Fact]
    public async Task GetContactById_ReturnsNotFound_WhenContactDoesNotExist()
    {
        // Arrange
        var contactId = 1;
        var contactResponse = new ContactResponse<Contact>
        {
            Success = false,
            Message = "Contact not found.",
            Data = null
        };

        _mockRepository.Setup(repo => repo.GetContactByIdAsync(contactId))
            .ReturnsAsync(contactResponse);

        // Act
        var result = await _controller.GetContactById(contactId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<Contact>>(notFoundResult.Value);
        Assert.False(returnValue.Success);
        Assert.Equal("Contact not found.", returnValue.Message);
    }

    [Fact]
    public async Task GetAllContacts_ReturnsOk_WhenContactsExist()
    {

        var contactList = new List<Contact>
        {
            new Contact { Id = 1, FirstName = "John", LastName = "Doe", AreaCode = "12", Phone = "912345678", Email = "johndoe@gmail.com" },
            new Contact { Id = 2, FirstName = "Peter", LastName = "Doe", AreaCode = "12", Phone = "912345678", Email = "peterdoe@gmail.com" },
        };
        var contactResponse = new ContactResponse<IEnumerable<Contact>>
        {
            Success = true,
            Message = "Contact list",
            Data = contactList,
        };

        _mockRepository.Setup(repo => repo.GetAllContactsAsync())
            .ReturnsAsync(contactResponse);
        
        //Act
        var result = await _controller.GetAllContacts();
        //Arrange
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<IEnumerable<Contact>>>(okResult.Value);
        Assert.True(returnValue.Success);
        Assert.Equal(2, returnValue.Data.Count());
    }

    [Fact]
    public async Task GetAllContacts_ReturnsNotFound_WhenContactsDoesNotExist()
    {
        var contactResponse = new ContactResponse<IEnumerable<Contact>>
        {
            Success = false,
            Message = "No contacts found",
            Data = null
        };
        _mockRepository.Setup(repo => repo.GetAllContactsAsync())
            .ReturnsAsync(contactResponse);
        
        var result = await _controller.GetAllContacts();
        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<IEnumerable<Contact>>>(notFoundResult.Value);
        Assert.False(returnValue.Success);
        Assert.Equal("No contacts found", returnValue.Message);
    }

    [Fact]
    public async Task AddContact_ReturnsCreated_WhenContactIsAddedSuccessfully()
    {
        var newContact = new Contact
        {
            Id = 1, 
            FirstName = "John", 
            LastName = "Doe", 
            AreaCode = "12", 
            Phone = "912345678",
            Email = "johndoe@gmail.com"
        };

        var contactResponse = new ContactResponse<Contact>
        {
            Success = true,
            Message = "Contact added successfully.",
            Data = new Contact
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                AreaCode = "12",
                Phone = "912345678",
                Email = "johndoe@gmail.com"
            }
        };
        _mockRepository.Setup(repo => repo.AddContactAsync(newContact))
            .ReturnsAsync(contactResponse);
        
        var result = await _controller.AddContact(newContact);
        
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<Contact>>(createdResult.Value);
        Assert.True(returnValue.Success);
        Assert.Equal(1, returnValue.Data.Id);
        Assert.Equal("Contact added successfully.", returnValue.Message);
        
    }

    [Fact]
    public async Task UpdateContact_ReturnsOk_WhenContactIsUpdatedSuccessfully()
    {
        int id = 1;
        var contact = new Contact
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "12",
            Phone = "912345678",
            Email = "johndoe@gmail.com"
        };
        var contactResponse = new ContactResponse<Contact>
        {
            Success = true,
            Message = "Contact updated successfully.",
            Data = contact
        };
        
        _mockRepository.Setup(repo => repo.UpdateContact(contact))
            .ReturnsAsync(contactResponse);
        
        var result = await _controller.UpdateContact(id, contact);
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<Contact>>(okResult.Value);
        Assert.Equal("Contact updated successfully.", returnValue.Message);
        Assert.Equal(contact.Id, returnValue.Data.Id);
        
    }

    [Fact]
    public async Task UpdateContact_ReturnsBadRequest_WhenContactDoesNotExist()
    {
        int id = 1;
        var contact = new Contact
        {
            Id = 2,
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "12",
            Phone = "912345678",
            Email = "johndoe@gmail.com"
        };
        
        
        var contactResponse = new ContactResponse<Contact>
        {
            Success = true,
            Message = $"The Contact Id {id} informed is different from the Id {contact.Id} in the payload.",
            Data = contact
        };
        var errorMessage =
            $"The Contact Id {id} informed is different from the Id {contact.Id} in the payload.";
        _mockRepository.Setup(repo => repo.UpdateContact(contact))
            .ReturnsAsync(contactResponse);
        
        var result = await _controller.UpdateContact(id, contact);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<Contact>>(badRequestResult.Value);
        Assert.False(returnValue.Success);
        Assert.Equal(errorMessage, returnValue.Message);
        
    }
    
    [Fact]
    public async Task UpdateContact_ReturnsNotFound_WhenContactDoesNotExist()
    {
        int id = 1;
        var contact = new Contact
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            AreaCode = "12",
            Phone = "912345678",
            Email = "johndoe@gmail.com"
        };
        
        
        var contactResponse = new ContactResponse<Contact>
        {
            Success = false,
            Message = $"The Contact Id {id} informed is different from the Id {contact.Id} in the payload.",
            Data = contact
        };
        
        _mockRepository.Setup(repo => repo.UpdateContact(contact))
            .ReturnsAsync(contactResponse);
        
        var result = await _controller.UpdateContact(id, contact);
        
        var notFoundtResult = Assert.IsType<NotFoundObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<Contact>>(notFoundtResult.Value);
        Assert.False(returnValue.Success);

        
    }

    [Fact]
    public async Task DeleteContact_ReturnsOk_WhenContactIsDeletedSuccessfully()
    {
        // Arrange
        int contactId = 1;

        var contactResponse = new ContactResponse<object>
        {
            Success = true,
            Message = "Contact deleted successfully.",
        };
        
        _mockRepository.Setup(repo => repo.RemoveContact(contactId))
            .ReturnsAsync(contactResponse);

        // Act
        var result = await _controller.DeleteContact(contactId);
        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<ContactResponse<object>>(okResult.Value);
        Assert.True(returnValue.Success);

    }

}