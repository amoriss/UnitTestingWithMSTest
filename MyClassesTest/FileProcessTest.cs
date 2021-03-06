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

        [TestInitialize]
        public void TestInitialize()
        {
            TestContext.WriteLine("In TestInitialize() method");


            WriteDescription(this.GetType());
            if (TestContext.TestName.StartsWith("FileNameDoesExist"))
            {
                SetGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine("Creating file " + _GoodFileName);
                    //Create the 'Good' file.
                    File.AppendAllText(_GoodFileName, "Some Text");
                }
            }
        }
        [TestCleanup]
        public void TestCleanup()
        {
            TestContext.WriteLine("In TestCleanup() method");

            if (TestContext.TestName.StartsWith("FileNameDoesExist"))
            {
                //Delete file
                if (File.Exists(_GoodFileName))
                {
                    TestContext.WriteLine("Deleting file " + _GoodFileName);
                    File.Delete(_GoodFileName);
                }

            }
        }


        [TestMethod]
        [DataRow(1, 1, DisplayName = "First Test (1,1")]
        [DataRow(42, 42, DisplayName = "Second Test (42,42)")]
        public void AreNumbersEqual (int num1, int num2)
        {
            Assert.AreEqual(num1, num2);
        }

        [TestMethod]
        [DeploymentItem("FileToDeploy.txt")]
        [DataRow(@"C:\Windows\Regedit.exe", DisplayName="Regedit.exe")]
        [DataRow("FileToDeploy.txt", DisplayName ="Deployment Item: FileToDeploy.txt")]
        public void FileNameUsingDataRow(string fileName)
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            if (!fileName.Contains(@"\"))
            {
                fileName = TestContext.DeploymentDirectory + @"\" + fileName;
            }
            TestContext.WriteLine("Checking File " + fileName);
            fromCall = fp.FileExists(fileName);
            Assert.IsTrue(fromCall);
        }

        [TestMethod] //attribute for each method
        [Description("Check to see if a file exists.")]
        [Owner("PaulS")]
        [Priority(1)]
        [TestCategory("NoException")]
        //[Ignore]
        public void FileNameDoesExist()
        {
            //Arrange  (declare all variables at top of method)
            FileProcess fp = new FileProcess();
            bool fromCall;

            //Act
            TestContext.WriteLine("Checking File " + _GoodFileName);
             
            fromCall = fp.FileExists(_GoodFileName);

            //Assert
            Assert.IsTrue(fromCall);
            
            //placeholder to run tests before 
            // Assert.Inconclusive();
        }

        [TestMethod]
        [Timeout(3000)]
        public void SimulateTimeout()
        {
            System.Threading.Thread.Sleep(4000);
        }

        [TestMethod]
        [Description("Check to see if a file does not exist.")]
        [Owner("PaulS")]
        [Priority(2)]
        [TestCategory("NoException")]

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
        [Description("Check for a thrown ArgumentNullException using ExpectedException.")]
        [Owner("SteveN")]
        [Priority(3)]
        [TestCategory("Exception")]
        public void FileNameNullorEmpty_UsingAttribute()
        {
            FileProcess fp = new FileProcess();

            TestContext.WriteLine("Checking for a null File");

            fp.FileExists("");
        }

        [TestMethod]
        [Description("Check for a thrown ArgumentNullException using try...catch.")]
        [Owner("SteveN")]
        [Priority(3)]
        [TestCategory("Exception")]
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
