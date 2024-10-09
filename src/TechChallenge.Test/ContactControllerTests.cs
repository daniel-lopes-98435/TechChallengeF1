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
}