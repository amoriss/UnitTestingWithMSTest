using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.IO;

namespace MyClassesTest 
{
    [TestClass] //attribute for the Class
    public class FileProcessTest : TestBase
    {

        private const string BAD_FILE_NAME = @"C:\NotExists.bad";

        [ClassInitialize()]
        public static void ClassInitialize(TestContext tc)
        {
            //TODO: Initialize for all tests in class
            tc.WriteLine("In ClassInitialize() method");
        }

        [ClassCleanup()]
        public static void ClassCleanup()
        {
            //TODO: Cleanup after all tests in class
        }

        [TestMethod] //attribute for each method
        public void FileNameDoesExist()
        {
            //Arrange  (declare all variables at top of method)
         
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

             //Check to make sure file name is not null or empty
            if (!string.IsNullOrEmpty(_GoodFileName))
            {
                //Creating the 'Good' file
                File.AppendAllText(_GoodFileName, "Some Text");
            }

            TestContext.WriteLine("Checking File" + _GoodFileName);
             
            fromCall = fp.FileExists(_GoodFileName);

            //Delete file
            if (File.Exists(_GoodFileName))
            {
                File.Delete(_GoodFileName);
            }

            //Act

            //Assert

            Assert.IsTrue(fromCall);
            
            //placeholder to run tests before 
            // Assert.Inconclusive();
        }
        [TestMethod]
        public void FileNameDoesNotExist()
        {
            //Arrange  (declare all variables at top of method)

            FileProcess fp = new FileProcess();
            bool fromCall;

            TestContext.WriteLine(@"Checking File " + BAD_FILE_NAME);

            //Act

            fromCall = fp.FileExists(BAD_FILE_NAME);

            //Assert

            Assert.IsFalse(fromCall);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullorEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();

            TestContext.WriteLine("Checking for a null File");

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullorEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                TestContext.WriteLine("Checking for a null File");
                fp.FileExists("");
            }
            catch (ArgumentNullException)
            {
                //Test was a success
                return;
            }

            //Fail the test
            Assert.Fail("Call to FileExists() did NOT throw an ArgumentNullException.");
        }
    }
}
