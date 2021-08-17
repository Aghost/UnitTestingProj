using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingTest;
using TestingTest.Interfaces;
using Xunit;
using Moq;
using FluentAssertions;

namespace TestingTest.Test
{
    public class FluentSave
    {
        private Mock<IRepository> repositoryMock;
        private UserService userService;

        public FluentSave() {
            repositoryMock = new Mock<IRepository>();
            userService = new UserService(repositoryMock.Object);
        }

        [Fact]
        public void ThrowNullException_When_UserIsNull() {
            //Arrange
            var user = new User();

            //Assert
            FluentActions.Invoking(() => userService.Save(user)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void True_If_UserIsValid() {
            //Arrange
            var user = new User { LastName = "laatste", EmailAddress = "valid@email.com", FirstName = "Henk" };

            //Assert
            FluentActions.Invoking(() => userService.Validate(user)).Should().Equals(true);
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
        [InlineData("")]
        [InlineData(null)]
        public void ThrowNullException_When_LastNameIsNullOrEmpty(string input) {
            //Arrange
            var user = new User { LastName = input };

            //Assert
            FluentActions.Invoking(() => userService.Save(user)).Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowNullException_When_EmailAdressIsNullOrEmpty(string input) {
            //Arrange
            var user = new User { EmailAddress = input };

            //Assert
            FluentActions.Invoking(() => userService.Save(user)).Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("first@email.com")]
        [InlineData("second@email.coml")]
        public void ThrowArgumentException_When_EmailAddressExists(string email) {
            //Arrange
            var user = new User { LastName = "laatste", EmailAddress = email };

            repositoryMock.Setup(a => a.AlreadyExists(user)).Returns(true);

            //Assert
            FluentActions.Invoking(() => userService.Save(user)).Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("notavalidaddress")]
        [InlineData("stillnotavalid.email")]
        [InlineData("stillnotvalid@")]
        public void ThrowArgumentException_When_EmailAddressIsNotCorrect(string email) {
            //Arrange
            var user = new User{ LastName = "laatste", EmailAddress = email };

            //Assert
            FluentActions.Invoking(() => userService.Save(user)).Should().Throw<ArgumentException>();
        }

    }
}
