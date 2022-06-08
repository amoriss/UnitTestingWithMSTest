using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using MyClasses;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessDataDriven : TestBase
    {
        private const string CONNECT_STRING = "Server=Localhost;Database=Sandbox;Integrated Security=Yes";

        [TestMethod()]
        public void FileExistsTestFromDB()
        {
            FileProcess fp = new FileProcess();
            bool fromCall = false;
            bool testFailed = false;
            string fileName;
            bool expectedValue;
            bool causesException;
            string sql = "SELECT * FROM tests.FileProcessTest";
            string conn = CONNECT_STRING;

            //Load data from SQL Server table 
            LoadDataTable(sql, conn);

            if (TestDataTable != null)
            {
                //Loop through all rows in table
                foreach (DataRow row in TestDataTable.Rows)
                {
                    fileName = row["FileName"].ToString();
                    expectedValue = Convert.ToBoolean(row["ExpectedValue"]);
                    causesException = Convert.ToBoolean(row["CausesException"]);

                    try
                    {
                        // See if file exists
                        fromCall = fp.FileExists(fileName);
                    }
                    catch (ArgumentNullException)
                    {
                        // See if a null value was expected
                        if (!causesException)
                        {

                        }
                    }
                }
            }

        }
    }
}
