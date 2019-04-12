using System;
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
            var fileProcessor = new FileDataProcessor( args, null );

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
                .With.Matches< ArgumentNullException>(
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

    }
}
