using System;
using Moq;
using NUnit.Framework;
using ThirdPartyTools;

namespace FileData.test
{
    [TestFixture]
    public class FileDataTest
    {

        [Test]
        [Category("File version number")]
        public void FileData_constructor_should_set_Args_property()
        {
            // Arrange 
            string[] args = { "-v", "c:/test.txt" };

            // Act 
            var fileProcessor = new FileDataProcessor(args, null);

            // Assert 
            Assert.That(fileProcessor.Args[0], Is.EqualTo("-v"));
            Assert.That(fileProcessor.Args[1], Is.EqualTo("c:/test.txt"));
            Assert.That(fileProcessor.Args.Length, Is.EqualTo(2));

        }

        [Test]
        [Category("File version number")]
        [TestCaseSource(typeof(FileVersionTestData), "FileDetailsTestCases")]
        public void FileData_constructor_should_set_CommandParameter_property(string command,
            string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            // Act 
            var fileProcessor = new FileDataProcessor(args);

            // Assert
            Assert.That(fileProcessor.CommandArguement, Is.EqualTo(command));

        }
        [Test]
        [Category("File version number")]
        public void FileData_constructor_should_set_FileLocation_property()
        {
            // Arrange
            string[] args = { "-v", "c:/test.txt" };

            // Act 
            var fileProcessor = new FileDataProcessor(args);

            // Assert
            Assert.That(fileProcessor.FileLocation, Is.EqualTo("c:/test.txt"));

        }

        [Test]
        [Category("File version number")]
        [TestCaseSource(typeof(FileVersionTestData), "FileVersionCommandNullTestCases")]
        public void Should_throw_error_when_command_is_nullorEmpty(string command,
            string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            // Act 
            // Assert
            Assert.That(() => new FileDataProcessor(args), Throws.TypeOf<ArgumentNullException>()
                .With.Matches<ArgumentNullException>(
                    ex => ex.ParamName == "CommandArguement" &&
                            ex.Message.Contains(FileDataConstants.commandEmptyErrorMessage)
                ));


        }
        [Test]
        [Category("File version number")]
        [TestCaseSource(typeof(FileVersionTestData), "FileVersionFileLocationNullTestCases")]
        public void Should_throw_error_when_fileLocation_is_nullorEmpty(string command,
            string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            // Act 
            // Assert
            Assert.That(() => new FileDataProcessor(args), Throws.TypeOf<ArgumentNullException>()
                .With.Matches<ArgumentNullException>(
                    ex => ex.ParamName == "FileLocation" &&
                            ex.Message.Contains(FileDataConstants.FileLocationEmptyErrorMessage)
                ));

        }

        [Test]
        [Category("File version number")]
        public void Should_throw_error_when_arguement_is_null()
        {
            // Arrange
            string[] args = null;

            // Act
            // Assert
            Assert.That(() => new FileDataProcessor(args), Throws.TypeOf<NullReferenceException>()
                .With.Matches<NullReferenceException>(
                    nEx => nEx.Message == "Object reference not set to an instance of an object."
                ));

        }

        [Test]
        [Category("Use third party library")]
        [TestCaseSource(typeof(FileVersionTestData), "FileDetailsTestCases")]
        public void Should_get_version_number_or_size_of_the_file(string command,
            string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            // create SUT
            var sut = new FileDataProcessor(args, new FileDetails());

            // Act 
            string result = sut.GetFileData();

            // Assert
            Assert.That(result, Is.TypeOf<string>());

        }

        [Test]
        [Category("Use third party library")]
        [TestCase("-c", "c:/test.txt")]
        public void Should_error_when_wrong_switch_command_used(string command,
           string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            // create SUT
            var sut = new FileDataProcessor(args, new FileDetails());

            // Act 
            // Assert
            Assert.That(() => sut.GetFileData(), Throws.TypeOf<InvalidOperationException>()
                 .With.Matches<InvalidOperationException>(
                     nOpx => nOpx.Message == "Command not valid!"
                 ));

        }

        [Test]
        [Category("Use third party library")]
        [TestCase("-v", "c:/test.txt")]
        public void Should_error_when_FileDetail_not_instantiated(string command,
          string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            // create SUT
            var sut = new FileDataProcessor(args);

            // Act 
            // Assert
            Assert.That(() => sut.GetFileData(), Throws.TypeOf<NullReferenceException>());

        }
        [Test]
        [Category("Use third party library")]
        [TestCase("-v", "c:/test.txt")]
        public void Should_get_version_number_of_the_file(string command,
           string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };
            var mockFileDetaidls = new Mock<IFileDetails> { DefaultValue = DefaultValue.Mock };
            var mockIAuthUser = mockFileDetaidls.As<IAuthUser>();

            mockFileDetaidls.Setup(x => x.Version(It.IsAny<string>()))
                .Returns("1.20.87");

            // create SUT
            var sut = new FileDataProcessor(args, mockFileDetaidls.Object);

            // Act 
            string result = sut.GetFileData();

            // Assert
            Assert.That(result, Is.EqualTo("File Version number: 1.20.87"));

        }

        [Test]
        [Category("Use third party library")]
        [TestCase("-s", "c:/test.txt")]
        public void Should_get_file_size_of_the_file(string command,
           string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };
            var mockFileDetaidls = new Mock<IFileDetails>();
            var mockIAuthUser = mockFileDetaidls.As<IAuthUser>();

            mockFileDetaidls.SetupAllProperties();

            mockFileDetaidls.Setup(x => x.Size(fileLocation))
                .Returns(285);

            // create SUT
            var sut = new FileDataProcessor(args, mockFileDetaidls.Object);

            // Act 
            string result = sut.GetFileData();

            // Assert
            Assert.That(result, Is.EqualTo("File size: 285"));

        }

        [Test]
        [Category("Use third party library")]
        [TestCase("-v", "c:\test.txt")]
        public void Should_return_filepath_with_version_when_authorised(string command,
           string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            var mockFileDetaidls = new Mock<IFileDetails>();
            var mockIAuthUser = mockFileDetaidls.As<IAuthUser>();

            mockFileDetaidls.SetupAllProperties();
            // Setting up a mock property to return a specific value.
            mockIAuthUser.Setup(x => x.IsAuthorised)
               .Returns(true);

            mockFileDetaidls.Setup(x => x.Version(It.IsAny<string>()))
                .Returns("1.20.87");

            // create SUT
            var sut = new FileDataProcessor(
                args,
                mockFileDetaidls.Object
            );

            // Act 
            string result = sut.GetFileData();

            // Assert
            Assert.That(result, Is.EqualTo("File Version number: 1.20.87, FilePath for authorised users : c:\test.txt"));

        }

        [Test]
        [Category("Use third party library")]
        [TestCase("-v", "c:\test.txt")]
        public void Should_verify_version_called_with_fileLcation_parameter_and_IsAuthorised_getter(string command,
           string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            var mockFileDetaidls = new Mock<IFileDetails>();
            var mockIAuthUser = mockFileDetaidls.As<IAuthUser>();

            // create SUT
            var sut = new FileDataProcessor(
                args,
                mockFileDetaidls.Object
            );

            // Act 
            string result = sut.GetFileData();

            // Assert
            mockFileDetaidls.Verify(x => x.Version(fileLocation));
            mockIAuthUser.VerifyGet(x => x.IsAuthorised);
            mockFileDetaidls.VerifyNoOtherCalls();
        }

        [Test]
        [Category("Use third party library")]
        [TestCase("-s", "c:\test.txt")]
        public void Should_verify_size_called_with_fileLcation_parameter_and_IsAuthorised_getter(string command,
           string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            var mockFileDetaidls = new Mock<IFileDetails>();
            var mockIAuthUser = mockFileDetaidls.As<IAuthUser>();

            // create SUT
            var sut = new FileDataProcessor(
                args,
                mockFileDetaidls.Object
            );

            // Act 
            string result = sut.GetFileData();

            // Assert
            mockFileDetaidls.Verify(x => x.Size(fileLocation));
            mockIAuthUser.VerifyGet(x => x.IsAuthorised);
            mockFileDetaidls.VerifyNoOtherCalls();

        }

        // The following characters are invalid in a path:
        // Char    Hex Value
        // ",      0022
        // <,      003C
        // >,      003E
        // |,      007C

        [Test]
        [Category("Use third party library")]
        [TestCase("-v", "c:\te<st.txt")]
        public void Should_throw_argumentException_when_version_called(string command,
         string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            var mockFileDetails = new Mock<IFileDetails>();
            var mockIAuthUser = mockFileDetails.As<IAuthUser>();

            mockFileDetails.Setup(f => f.Version(fileLocation))
                .Throws<ArgumentException>();

            // create SUT
            var sut = new FileDataProcessor(
                args,
                mockFileDetails.Object
            );

            // Act 
            // Assert
            Assert.That(() => sut.GetFileData(), Throws.TypeOf<ArgumentException>()
                 .With.Matches<ArgumentException>(
                     nOpx => nOpx.Message == "Value does not fall within the expected range."
                 ));

            Assert.That(sut.HasError, Is.True);

        }

        [Test]
        [Category("Use third party library")]
        [TestCase("-v", "c:\test.txt")]
        public void Should_call_event_AuthorisedAllFileData_and_set_CanGetFileSizeData_flag(string command,
         string fileLocation)
        {
            // Arrange
            string[] args = { command, fileLocation };

            var mockFileDetails = new Mock<IFileDetails>();
            var mockIAuthUser = mockFileDetails.As<IAuthUser>();

            mockIAuthUser.Setup(f => f.IsAuthorised)
                .Returns(true);

            // create SUT
            var sut = new FileDataProcessor(
                args,
                mockFileDetails.Object
            );

            // Act 
            // Manually raise even.
            mockIAuthUser.Raise(x => x.AuthorisedAllFileData += null, true);

            // Assert
            Assert.That(sut.CanGetFileSizeData, Is.True);

        }
    }


}
