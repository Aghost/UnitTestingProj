using Moq;
using System;
using TestingTest.Core;
using TestingTest.Core.Interfaces;
using TestingTest.Core.Services;
using Xunit;
using FluentAssertions;

namespace TestingTest.Test
{
    public class BanaanSave
    {
        private Mock<IRepository> repositoryMock;
        private BanaanService banaanService;

        public BanaanSave() {
            repositoryMock = new Mock<IRepository>();
            banaanService = new BanaanService(repositoryMock.Object);
        }

        [Fact]
        public void ThrowNullException_When_UserIsNull() {
            //Arrange
            var banaan = new Banaan();

            //Act
            Action a = () => banaanService.Save(banaan);

            //Assert
            a.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowNullException_When_LastNameIsNullOrEmpty(string input) {
            //Arrange
            var banaan = new Banaan { LastName = input };

            //Act
            Action a = () => banaanService.Save(banaan);
            //Assert
            a.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowNullException_When_EmailAdressIsNullOrEmpty(string input) {
            //Arrange
            var banaan = new Banaan { EmailAddress = input };

            //Act
            Action a = () => banaanService.Save(banaan);
            //Assert
            a.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData("first@email.com")]
        [InlineData("second@email.coml")]
        public void ThrowArgumentException_When_EmailAddressExists(string email) {
            //Arrange
            var banaan = new Banaan { LastName = "laatste", EmailAddress = email };

            repositoryMock.Setup(a => a.AlreadyExists(banaan)).Returns(true);

            //Act
            Action a = () => banaanService.Save(banaan);
            //Assert
            a.Should().Throw<ArgumentException>();
        }

        [Theory]
        [InlineData("notavalidaddress")]
        [InlineData("stillnotvalid@")]
        [InlineData("stillnotavalid.email")]
        public void ThrowArgumentException_When_EmailAddressIsNotCorrect(string email) {
            //Arrange
            var banaan = new Banaan{ LastName = "laatste", EmailAddress = email };

            //Act
            Action a = () => banaanService.Save(banaan);

            //Assert
            Assert.Throws<ArgumentException>(() => banaanService.Save(banaan));
            
        }

        [Fact]
        public void ReturnTrue_If_UserIsValid() {
            //Arrange
            var banaan = new Banaan { LastName = "laatste", EmailAddress = "valid@email.com", FirstName = "Henk" };

            //Act
            var result = banaanService.Validate(banaan);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void VerifyAddUser_When_UserIsValid() {
            //Arrange
            var banaan = new Banaan { LastName = "laatste", EmailAddress = "valid@email.com", FirstName = "Henk"};

            //Act
            banaanService.Save(banaan);

            //Assert
            repositoryMock.Verify(r => r.Add(banaan), Times.Once);
        }   
    }
}
