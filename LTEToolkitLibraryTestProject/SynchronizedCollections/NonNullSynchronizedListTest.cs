using System;
using System.Collections.Generic;
using Erwine.Leonard.T.Toolkit.Collections.Synchronized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LTEToolkitLibraryTestProject.SynchronizedCollections
{ 
    /// <summary>
    ///This is a test class for NonNullSynchronizedListTest and is intended
    ///to contain all NonNullSynchronizedListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NonNullSynchronizedListTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod]
        public void ConstructorTestMethod()
        {
            NonNullSynchronizedList<HelperObject2> target = new NonNullSynchronizedList<HelperObject2>();
            HelperObject2 item = new HelperObject2(40);
            target.Add(item);
            Assert.AreEqual(1, target.Count);
            Assert.IsNotNull(target[0]);
            Assert.AreSame(item, target[0]);

            List<HelperObject2> list = new List<HelperObject2>();
            list.Add(item);
            list.Add(null);
            try
            {
                target = new NonNullSynchronizedList<HelperObject2>(list);
            }
            catch (ArgumentOutOfRangeException) { }
        }

        [TestMethod]
        public void ApplyNullTestMethod()
        {
            NonNullSynchronizedList<HelperObject2> target = new NonNullSynchronizedList<HelperObject2>();
            HelperObject2 item = new HelperObject2(40);
            target.Add(item);
            Assert.AreEqual(1, target.Count);
            Assert.IsNotNull(target[0]);
            Assert.AreSame(item, target[0]);

            try
            {
                target.Add(null);
            }
            catch (ArgumentNullException) { }

            Assert.AreEqual(1, target.Count);
            Assert.IsNotNull(target[0]);
            Assert.AreSame(item, target[0]);

            try
            {
                target.Insert(0, null);
            }
            catch (ArgumentNullException) { }

            Assert.AreEqual(1, target.Count);
            Assert.IsNotNull(target[0]);
            Assert.AreSame(item, target[0]);

            try
            {
                target[0] = null;
            }
            catch (ArgumentNullException) { }

            Assert.AreEqual(1, target.Count);
            Assert.IsNotNull(target[0]);
            Assert.AreSame(item, target[0]);
        }
    }
}
