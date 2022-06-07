using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses.PersonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassesTest
{
    [TestClass]
    public class CollectionAssertClassTest : TestBase
    {
        [TestMethod]
        public void AreCollectionsEqual()
        {        
            PersonManager mgr = new PersonManager();
            List<Person> peopleExpected;
            List<Person> peopleActual;

            peopleActual = mgr.GetPeople();
            peopleExpected = peopleActual;

            // NOTE: By default it compares the person objects to see if they are Equal (they refer to the same object)
            CollectionAssert.AreEqual(peopleExpected, peopleActual);
        }

        [TestMethod]
        public void IsCollectionOfTypeTest()
        {
            PersonManager mgr = new PersonManager();
            List<Person> peopleActual = new List<Person>();

            peopleActual = mgr.GetSupervisors();

            CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));
        }

    }
}
