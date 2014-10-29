using System;
using System.Collections;
using System.Linq;
using Erwine.Leonard.T.Toolkit.Collections.Synchronized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LTEToolkitLibraryTestProject.SynchronizedCollections
{
    /// <summary>
    ///This is a test class for SynchronizedReadOnlyListTest and is intended
    ///to contain all SynchronizedReadOnlyListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SynchronizedReadOnlyListTest
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
            HelperObject1.ResetCounter();

            SynchronizedReadOnlyList<HelperObject1> target1 = new SynchronizedReadOnlyList<HelperObject1>(7);

            Assert.AreEqual(7, target1.Count);

            for (int i = 0; i < 7; i++)
            {
                Assert.IsNotNull(target1[i]);
                Assert.AreEqual(i, target1[i].Id);
            }

            SynchronizedReadOnlyList<HelperObject2> target2 = new SynchronizedReadOnlyList<HelperObject2>(new HelperObject2(1),
                new HelperObject2(1), new HelperObject2(3));

            Assert.AreEqual(3, target2.Count);

            Assert.AreEqual(1, target2[0].Id);
            Assert.AreEqual(1, target2[1].Id);
            Assert.AreNotSame(target2[0], target2[1]);
            Assert.AreEqual(3, target2[2].Id);

            try
            {
                target2 = new SynchronizedReadOnlyList<HelperObject2>(3);
            }
            catch (MissingMethodException) { }
        }

        [TestMethod]
        public void MutationsTest()
        {
            HelperObject1.ResetCounter();

            SynchronizedReadOnlyList<HelperObject1> list = new SynchronizedReadOnlyList<HelperObject1>(7);
            IList target = list;

            HelperObject1[] items = list.ToArray();

            try
            {
                target.Add(new HelperObject1());
            }
            catch (NotSupportedException) { }

            Assert.AreEqual(items.Length, list.Count);
            for (int i = 0; i < items.Length; i++)
            {
                Assert.IsNotNull(items[i]);
                Assert.AreSame(items[i], list[i]);
            }

            try
            {
                target.Clear();
            }
            catch (NotSupportedException) { }

            Assert.AreEqual(items.Length, list.Count);
            for (int i = 0; i < items.Length; i++)
            {
                Assert.IsNotNull(items[i]);
                Assert.AreSame(items[i], list[i]);
            }

            try
            {
                target.Insert(1, new HelperObject1());
            }
            catch (NotSupportedException) { }

            Assert.AreEqual(items.Length, list.Count);
            for (int i = 0; i < items.Length; i++)
            {
                Assert.IsNotNull(items[i]);
                Assert.AreSame(items[i], list[i]);
            }

            try
            {
                target.Remove(target[1]);
            }
            catch (NotSupportedException) { }

            Assert.AreEqual(items.Length, list.Count);
            for (int i = 0; i < items.Length; i++)
            {
                Assert.IsNotNull(items[i]);
                Assert.AreSame(items[i], list[i]);
            }

            try
            {
                target.RemoveAt(1);
            }
            catch (NotSupportedException) { }

            Assert.AreEqual(items.Length, list.Count);
            for (int i = 0; i < items.Length; i++)
            {
                Assert.IsNotNull(items[i]);
                Assert.AreSame(items[i], list[i]);
            }

            try
            {
                target[1] = new HelperObject1();
            }
            catch (NotSupportedException) { }

            Assert.AreEqual(items.Length, list.Count);
            for (int i = 0; i < items.Length; i++)
            {
                Assert.IsNotNull(items[i]);
                Assert.AreSame(items[i], list[i]);
            }
        }
    }
}
