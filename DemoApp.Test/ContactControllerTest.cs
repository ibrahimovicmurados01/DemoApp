using AutoMapper;
using DemoApp.Contracts;
using DemoApp.Entities.Models;
using DemoApp.Repository;
using DemoApp.Web.Controllers;
using DemoApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Test
{
    public class ContactControllerTest
    {
        private readonly Mock<IRepositoryWrapper> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;

        private readonly ContactController _controller;

        private List<Contact> contacts;
        public ContactControllerTest()
        {
            _mockRepo = new Mock<IRepositoryWrapper>();
            _mockMapper = new Mock<IMapper>();
            _controller = new ContactController(_mockRepo.Object, _mockMapper.Object);

            contacts = new List<Contact>() {

                new Contact(){
                 Id=Guid.NewGuid(),
                 FirstName="Test1",
                 LastName="Test1ov",
                 Email="test1@gmail.com",
                 PhoneNumber="9945511111",
                 Created=DateTimeOffset.Now,
                 Modified=  DateTimeOffset.Now,
                 Tombstoned=false
                },
                 new Contact(){
                 Id=Guid.NewGuid(),
                 FirstName="Test2",
                 LastName="Test2ov",
                 Email="test2@gmail.com",
                 PhoneNumber="9945522222",
                 Created=DateTimeOffset.Now,
                 Modified=  DateTimeOffset.Now,
                 Tombstoned=false
                },
                new Contact(){
                 Id=Guid.NewGuid(),
                 FirstName="Test3",
                 LastName="Test3ov",
                 Email="test3@gmail.com",
                 PhoneNumber="9945533333",
                 Created=DateTimeOffset.Now,
                 Modified=  DateTimeOffset.Now,
                 Tombstoned=false
                }
            };
        }

        [Fact]
        public void Index_ActionExecute_ReturnView() {


            // Arrange
            var mockContactRepository = new Mock<IRepositoryBase<Contact>>();
            _mockRepo.Setup(repo => repo.Contact).Returns(mockContactRepository.Object);

            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
     
        }
        [Fact]
        public void Index_ActionExecute_ReturnConactModelList() {

            // Arrange
            var mockContactRepository = new Mock<IRepositoryBase<Contact>>();
             mockContactRepository.Setup(repo => repo.GetAll()).Returns(contacts.AsQueryable());
            _mockRepo.Setup(repo => repo.Contact).Returns(mockContactRepository.Object);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult= Assert.IsType<ViewResult>(result);
            var contactList = Assert.IsAssignableFrom<List<Contact>>(viewResult.Model);
           
           Assert.Equal<int>(3, contactList.Count());
        }

    }
}
