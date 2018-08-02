using System;
using System.Diagnostics;
using System.IO;
using ChessLib.controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChessTest
{
    [TestClass]
    public class FileReaderTester
    {
        /*
         * Test-Driven Development
         * -----------------------
         * Arrange
         * Act
         * Assert
        */

        private readonly string invalidCommandsFilePath = Path.GetFullPath("../../test_data/invalid_commands.txt");
        private readonly string validCommandsFilePath = Path.GetFullPath("../../test_data/valid_commands.txt");

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void InvalidCommandInFileThrowsExceptionTest()
        {
            FileReader fileReader = new FileReader(invalidCommandsFilePath);
        }

        [TestMethod]
        public void ValidCommandsInFileDoesNotThrowException()
        {
            try
            {
                FileReader fileReader = new FileReader(validCommandsFilePath);
            }
            catch (FormatException e)
            {
                Trace.Write(e);
                Assert.Fail();
            }
        }

        [TestMethod]
        public void NumberOfCommandsReadFromFileIsCorrect()
        { 
            FileReader fileReader = new FileReader(validCommandsFilePath);
            int count = TestData.InvalidCommands.Split('\n').Length;
            Assert.AreEqual(fileReader.Commands.ToArray().Length, count);
        }

    }
}
