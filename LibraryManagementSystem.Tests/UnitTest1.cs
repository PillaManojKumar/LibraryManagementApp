using Moq;
using NUnit.Framework;
using System;
using Unity;
using System.IO;

namespace LibraryManagementSystem.Tests
{
    [TestFixture]
    public class LibraryManagementSystemTests
    {
        private Mock<ILibraryManagementSystem> libraryMock;

        [SetUp]
        public void Setup()
        {
            libraryMock = new Mock<ILibraryManagementSystem>();
        }

        [Test]
        public void ValidateUser_InvalidCredentials_Returnstrue()
        {
            var username = "invalidUsername";
            var password = "invalidPassword";

            libraryMock.Setup(x => x.ValidateUser(username, password)).Returns(true);

            var result = libraryMock.Object.ValidateUser(username, password);

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateUser_InvalidCredentials_Returnsfalse()
        {
            var username = "invalidUsername";
            var password = "invalidPassword";

            libraryMock.Setup(x => x.ValidateUser(username, password)).Returns(false);

            var result = libraryMock.Object.ValidateUser(username, password);

            Assert.IsFalse(result);
        }
        [Test]
        public void AddBook_WhenCalled_ShouldReturnTrue()
        {
            string title = "book";
            string author = "John";
            string publication = "XYZ";

            libraryMock.Setup(l => l.AddBook()).Returns(true);

            bool result = libraryMock.Object.AddBook();

            Assert.IsTrue(result);
        }

        [Test]
        public void EditBook_WhenBookExists_ShouldReturnTrue()
        {
            int bookId = 123;
            string title = "New Title";
            string author = "New Author";
            string publication = "New Publication";

            libraryMock.Setup(l => l.EditBook()).Returns(true);

            bool result = libraryMock.Object.EditBook();

            libraryMock.Verify(l => l.EditBook(), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void EditBook_WhenBookNotFound_ShouldReturnFalse()
        {
            int bookId = 123;
            string title = "New Title";
            string author = "New Author";
            string publication = "New Publication";

            libraryMock.Setup(l => l.EditBook()).Returns(false);

            bool result = libraryMock.Object.EditBook();

            libraryMock.Verify(l => l.EditBook(), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void DeleteBook_WhenBookExists_ShouldReturnTrue()
        {
            int bookId = 123;

            libraryMock.Setup(l => l.DeleteBook()).Returns(true);

            bool result = libraryMock.Object.DeleteBook();

            libraryMock.Verify(l => l.DeleteBook(), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteBook_WhenBookNotFound_ShouldReturnFalse()
        {
            // Arrange
            int bookId = 123;

            libraryMock.Setup(l => l.DeleteBook()).Returns(false);

            // Act
            bool result = libraryMock.Object.DeleteBook();

            // Assert
            libraryMock.Verify(l => l.DeleteBook(), Times.Once);
            Assert.IsFalse(result);
        }

        [Test]
        public void AddStudent_WhenCalled_ReturnsTrue()
        {
            // Arrange
            string name = "John Doe";
            long phoneNumber = 1234567890;

            libraryMock.Setup(x => x.AddStudent()).Returns(true);

            // Act
            bool result = libraryMock.Object.AddStudent();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void EditStudent_StudentExists_StudentDetailsUpdated()
        {
            // Arrange
            int studentId = 1;
            string name = "John";
            long phoneNumber = 1234567890;

            libraryMock.Setup(mock => mock.EditStudent()).Returns(true);

            // Act
            bool result = libraryMock.Object.EditStudent();

            // Assert
            Assert.IsTrue(result);
            libraryMock.Verify(mock => mock.EditStudent(), Times.Once);
        } 

        [Test]
        public void EditStudent_StudentNotFound_StudentDetailsNotUpdated()
        {
            // Arrange
            int studentId = 1;
            string name = "John";
            long phoneNumber = 1234567890;

            libraryMock.Setup(mock => mock.EditStudent()).Returns(false);

            ILibraryManagementSystem libraryManagementSystem = libraryMock.Object;

            // Act
            bool result = libraryManagementSystem.EditStudent();

            // Assert
            Assert.IsFalse(result);
            libraryMock.Verify(mock => mock.EditStudent(), Times.Once);
        }

        [Test]
        public void DeleteStudent_StudentExists_ReturnsTrue()
        {
            // Arrange
            int studentId = 123;
            libraryMock.Setup(x => x.DeleteStudent()).Returns(true);

            // Act
            bool result = libraryMock.Object.DeleteStudent();

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteStudent_StudentNotFound_ReturnsFalse()
        {
            // Arrange
            int studentId = 456;
            libraryMock.Setup(x => x.DeleteStudent()).Returns(false);

            // Act
            bool result = libraryMock.Object.DeleteStudent();

            // Assert
            Assert.IsFalse(result);
        }

    }
} 


