using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MyClassesTest
{
    public class TestBase
    {
        protected string _GoodFileName;
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

    }
}
