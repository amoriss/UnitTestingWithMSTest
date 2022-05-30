using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;

namespace MyClassesTest
{
    [TestClass] //attribute for the Class
    public class FileProcessTest
    {
        protected string _GoodFileName;
        private const string BAD_FILE_NAME = @"C:\NotExists.bad";
        public TestContext TestContext { get; set; }

        protected void SetGoodFileName()
        {
            _GoodFileName = TestContext.Properties["GoodFileName"].ToString();
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod] //attribute for each method
        public void FileNameDoesExist()
        {
            //Arrange  (declare all variables at top of method)
         
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();

            TestContext.WriteLine("Checking File" + _GoodFileName);

            //Act

            fromCall = fp.FileExists(_GoodFileName);

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
