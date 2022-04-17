using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;

namespace MyClassesTest
{
    [TestClass] //attribute for the Class
    public class FileProcessTest
    {
        [TestMethod] //attribute for each method
        public void FileNameDoesExist()
        {
            //Arrange  (declare all variables at top of method)
         
            FileProcess fp = new FileProcess();
            bool fromCall;

            //Act

            fromCall = fp.FileExists(@"C:\Windows\System\Speech\xml.xsd");

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

            //Act

            fromCall = fp.FileExists(@"C:\Windows\System\Speech\nothing.exe");

            //Assert

            Assert.IsFalse(fromCall);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullorEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();
            
            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameNullorEmpty_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("File");
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
