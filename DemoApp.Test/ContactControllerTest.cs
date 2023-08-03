using AutoMapper;
using DemoApp.Contracts;
using DemoApp.Entities.Models;
using DemoApp.Web.Controllers;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace DemoApp.Test
{


    public class ContactControllerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<ContactController>> _mockLogger;
        private readonly Mock<BaseController> _mockBaseController;
        private readonly ContactController _controller;

        private List<Contact> contacts;
        private List<ContactModel> contactModels;

        public ContactControllerTest()
        {
            var sharedUser = new SharedUserModel { UserName = "testuser", UserId = Guid.NewGuid() };
            // Initialize the list of contacts for the repository mock
            contacts = new List<Contact>()
        {
            new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test1",
                LastName = "Test1ov",
                Email = "test1@gmail.com",
                PhoneNumber = "9945511111",
                Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now,
                Tombstoned = false
            },
            new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test2",
                LastName = "Test2ov",
                Email = "test2@gmail.com",
                PhoneNumber = "9945522222",
                Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now,
                Tombstoned = false
            },
            new Contact()
            {
                Id = Guid.NewGuid(),
                FirstName = "Test3",
                LastName = "Test3ov",
                Email = "test3@gmail.com",
                PhoneNumber = "9945533333",
                Created = DateTimeOffset.Now,
                Modified = DateTimeOffset.Now,
                Tombstoned = false
            }
        };
            _mockRepo = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();

         
            _mockBaseController = new Mock<BaseController>() { CallBase = true };

            // Mock the behavior of UserFromCookie method on the BaseController
            _mockBaseController.Setup(c => c.UserFromCookie).Returns(sharedUser);

            _mockLogger = new Mock<ILogger<ContactController>>();

            _controller = new ContactController(_mockRepo.Object, _mockMapper.Object, _mockLogger.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() // Set the HttpContext for the controller
                }
            };

          

        }

        [Fact]
        public async Task Index_ActionExecute_ReturnView()
        {
            // Arrange
            var mockContactRepository = new Mock<IRepositoryBase<Contact>>();
            _mockRepo.Setup(repo => repo.Contact).Returns(mockContactRepository.Object);


            // Act
            var actionResult = await _controller.Index();
            var viewResult = Assert.IsType<ViewResult>(actionResult);

            // Assert
            Assert.NotNull(viewResult);
        }

 

        [Fact]
        public async Task Index_ActionExecute_ReturnContactModelList()
        {
            // Arrange
       
            var mockContactRepository = new Mock<IRepositoryBase<Contact>>();
            mockContactRepository.Setup(repo => repo.FindByAsync(It.IsAny<Expression<Func<Contact, bool>>>()))
                                .ReturnsAsync(contacts);
            _mockRepo.Setup(repo => repo.Contact).Returns(mockContactRepository.Object);

            // Act
            var actionResult = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(actionResult);
            var contactList = Assert.IsAssignableFrom<List<ContactModel>>(viewResult.Model);

            Assert.Equal(3, contactList.Count);
            _mockMapper.Verify(mapper => mapper.Map<List<ContactModel>>(It.IsAny<List<Contact>>()), Times.Once); // Verify that the mapping was called once
        }

        [Fact]
        public void Create_WithValidModel_RedirectsToIndex()
        {
            // Arrange
            var model = new ContactModel
            {
                // Set properties of the model with valid data for testing
                FirstName = "Murad",
                LastName = "Ibrahimovic",
                Email = "murad@gmail.com",
                PhoneNumber = "1234567890"
            };

            var mappedContact = new Contact
            {
                // Set properties of the mapped contact object based on the model
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            _mockMapper.Setup(mapper => mapper.Map<Contact>(model)).Returns(mappedContact);
            _mockRepo.Setup(repo => repo.Contact.CreateAsync(It.IsAny<Contact>())).Verifiable();
            _mockRepo.Setup(repo => repo.SaveAsync()).Verifiable();

            // Act
            var result = _controller.Create(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);

            _mockRepo.Verify(repo => repo.Contact.CreateAsync(It.IsAny<Contact>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveAsync(), Times.Once);
        }


    }


}
