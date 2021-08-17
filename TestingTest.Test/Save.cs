﻿using Moq;
using System;
using TestingTest.Interfaces;
using TestingTest.Services;
using Xunit;
using FluentAssertions;

namespace TestingTest.Test
{
    public class Save
    {
        private Mock<IRepository> repositoryMock;
        private UserService userService;

        public Save() {
            repositoryMock = new Mock<IRepository>();
            userService = new UserService(repositoryMock.Object);
        }

        [Fact]
        public void ThrowNullException_When_UserIsNull() {
            //Arrange
            var user = new User();

            //Act
            Action a = () => userService.Save(user);

            //Assert
            a.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void True_If_UserIsValid() {
            //Arrange
            var user = new User { LastName = "laatste", EmailAddress = "valid@email.com", FirstName = "Henk" };

            //Act
            var result = userService.Validate(user);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyAddUser_When_UserIsValid() {
            //Arrange
            var user = new User { LastName = "laatste", EmailAddress = "valid@email.com", FirstName = "Henk"};

            //Act
            userService.Save(user);

            //Assert
            repositoryMock.Verify(r => r.Add(user), Times.Once);
        }   

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowNullException_When_LastNameIsNullOrEmpty(string input) {
            //Arrange
            var user = new User { LastName = input };

            //Act
            Action a = () => userService.Save(user);
            //Assert
            a.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowNullException_When_EmailAdressIsNullOrEmpty(string input) {
            //Arrange
            var user = new User { EmailAddress = input };

            //Act
            Action a = () => userService.Save(user);
            //Assert
            a.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("first@email.com")]
        [InlineData("second@email.coml")]
        public void ThrowArgumentException_When_EmailAddressExists(string email) {
            //Arrange
            var user = new User { LastName = "laatste", EmailAddress = email };

            repositoryMock.Setup(a => a.AlreadyExists(user)).Returns(true);

            //Act
            Action a = () => userService.Save(user);
            //Assert
            a.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("notavalidaddress")]
        [InlineData("stillnotavalid.email")]
        [InlineData("stillnotvalid@")]
        public void ThrowArgumentException_When_EmailAddressIsNotCorrect(string email) {
            //Arrange
            var user = new User{ LastName = "laatste", EmailAddress = email };

            //Act
            Action a = () => userService.Save(user);

            //Assert
            Assert.Throws<ArgumentException>(() => userService.Save(user));
            
        }
    }
}
