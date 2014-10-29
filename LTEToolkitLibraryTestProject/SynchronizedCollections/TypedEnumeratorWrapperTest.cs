using System;
using Erwine.Leonard.T.Toolkit.Collections.Synchronized;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LTEToolkitLibraryTestProject.SynchronizedCollections
{
    /// <summary>
    ///This is a test class for TypedEnumeratorWrapperTest and is intended
    ///to contain all TypedEnumeratorWrapperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TypedEnumeratorWrapperTest
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
            int[] enumerable1 = new int[12];

            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable1)) { }

            string[] enumerable2 = new string[12];
            // This won't fail because we haven't tried to get a value, yet.
            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable2)) { }

            enumerable1 = null;
            try
            {
                TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable1);
            }
            catch (ArgumentNullException) { }
        }

        [TestMethod]
        public void MoveNextTestMethod()
        {
            int[] enumerable1 = new int[] { 1, 2, 3 };

            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable1))
            {
                int index = 0;
                while (target.MoveNext())
                {
                    Assert.IsTrue(index < enumerable1.Length);
                    index++;
                }

                Assert.AreEqual(enumerable1.Length, index);
            }

            enumerable1 = new int[0];

            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable1))
                Assert.IsFalse(target.MoveNext());
        }

        [TestMethod]
        public void CurrentTestMethod()
        {
            int[] enumerable1 = new int[] { 1, 2, 3 };

            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable1))
            {
                int index = 0;
                while (target.MoveNext())
                {
                    Assert.AreEqual(enumerable1[index], target.Current);
                    index++;
                }

                try
                {
                    int dummy = target.Current;
                }
                catch (InvalidOperationException) { }

                Assert.AreEqual(enumerable1.Length, index);
            }

            enumerable1 = new int[0];

            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable1))
            {
                target.MoveNext();
                try
                {
                    int dummy = target.Current;
                }
                catch (InvalidOperationException) { }
            }

            object[] enumerable2 = new object[] { "one", null, 3 };

            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable2))
            {
                target.MoveNext();
                try
                {
                    int dummy = target.Current;
                }
                catch (InvalidCastException) { }
                target.MoveNext();
                try
                {
                    int dummy = target.Current;
                }
                catch (NullReferenceException) { }
                target.MoveNext();
                Assert.AreEqual(3, target.Current);
            }
        }

        [TestMethod]
        public void ResetTestMethod()
        {
            int[] enumerable1 = new int[] { 1, 2, 3 };

            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(enumerable1))
            {
                int index = 0;
                while (target.MoveNext())
                {
                    Assert.AreEqual(enumerable1[index], target.Current);
                    index++;
                }

                target.Reset();

                index = 0;
                while (target.MoveNext())
                {
                    Assert.AreEqual(enumerable1[index], target.Current);
                    index++;
                }

                Assert.AreEqual(enumerable1.Length, index);
            }
        }

        [TestMethod]
        public void DisposeTestMethod()
        {
            HelperList list = new HelperList(1, 2, 3, 4, 5, 6, 7);
            list.EnumeratorWasDisposed = false;
            using (TypedEnumeratorWrapper<int> target = new TypedEnumeratorWrapper<int>(list)) { }
            Assert.IsTrue(list.EnumeratorWasDisposed);
        }
    }
}
