using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fairhaven;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            String s = Ascii.Bold("str");
        }
    }
}
