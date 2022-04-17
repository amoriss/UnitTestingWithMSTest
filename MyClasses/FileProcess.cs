using System;
using System.IO;

namespace MyClasses
{
    public class FileProcess
    {
        //Unit Testing with Paul Sheriff
        public bool FileExists (string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");
            }

            return File.Exists(fileName);
        }

    }
}
