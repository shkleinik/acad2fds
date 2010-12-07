namespace UnitTests.CommonTests
{
    using System;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LoggerTests
    {
        [TestMethod]
        public void WriteToLog()
        {
            StaticLogger.LogError(new Exception("Test exception message."));
        }
    }
}