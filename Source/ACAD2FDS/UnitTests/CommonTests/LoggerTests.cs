namespace UnitTests.CommonTests
{
    using System;
    using System.Threading;
    using Common;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LoggerTests
    {
        private ILogger log1;
        private ILogger log2;

        [TestMethod]
        [Timeout(TestTimeout.Infinite)]
        public void WriteToLog()
        {
            log1 = new Logger();
            log2 = new Logger();

            Thread thread1 = new Thread(WriteToLoggerOne);
            Thread thread2 = new Thread(WriteToLoggerTwo);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();
        }

        private void WriteToLoggerOne()
        {
            for (var i = 0; i < 100; i++)
            {
                log2.LogInfo(string.Format("Thread Id {0} From log2 Count value {1}", Thread.CurrentThread.ManagedThreadId, i));
                log1.LogInfo(string.Format("Thread Id {0} From log1 Count value {1}", Thread.CurrentThread.ManagedThreadId, i));

                try
                {
                    throw new Exception(string.Format("Thread Id {0} Count value {1}", Thread.CurrentThread.ManagedThreadId, i));
                }
                catch (Exception ex)
                {
                    log2.LogError(ex);
                    log1.LogError(ex);
                }
            }
        }

        private void WriteToLoggerTwo()
        {
            for (int i = 0; i < 100; i++)
            {
                log2.LogInfo(string.Format("Thread Id {0} From log2 Count value {1}", Thread.CurrentThread.ManagedThreadId, i));
                log1.LogInfo(string.Format("Thread Id {0} From log1 Count value {1}", Thread.CurrentThread.ManagedThreadId, i));

                try
                {
                    throw new Exception(string.Format("Thread Id {0} Count value {1}", Thread.CurrentThread.ManagedThreadId, i));
                }
                catch (Exception ex)
                {
                    log2.LogError(ex);
                    log1.LogError(ex);
                }
            }
        }
    }
}