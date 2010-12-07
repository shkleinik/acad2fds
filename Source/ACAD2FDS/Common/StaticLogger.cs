namespace Common
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class StaticLogger
    {
        #region Constants

        private const string FolderName = "Log";

        private const string FileName = "Log.txt";

        #endregion

        #region Fields

        private static StreamWriter streamWriter;
        private static StackTrace stackTrace;
        private static MethodBase methodBase;

        #endregion

        public static void LogInfo(object info)
        {
            if (!Directory.Exists(FolderName))
            {
                Directory.CreateDirectory(FolderName);
            }

            var pathToLogFile = FolderName + Path.DirectorySeparatorChar + FileName;

            try
            {
                streamWriter = new StreamWriter(pathToLogFile, true);

                streamWriter.WriteLine("_ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _ _");
                streamWriter.WriteLine(DateTime.UtcNow);
                streamWriter.WriteLine(info);
                streamWriter.WriteLine();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            finally
            {
                Dispose();
            }
        }

        public static void LogError(Exception ex)
        {
            try
            {
                stackTrace = new StackTrace(ex, true);

                var fileNames = stackTrace.GetFrame((stackTrace.FrameCount - 1)).GetFileName();

                var lineNumber = stackTrace.GetFrame((stackTrace.FrameCount - 1)).GetFileLineNumber();

                methodBase = stackTrace.GetFrame((stackTrace.FrameCount - 1)).GetMethod();

                var methodName = methodBase.Name;

                var sb = new StringBuilder();

                sb.AppendFormat("Error occured in {0}", fileNames);
                sb.AppendFormat("Method name : {0}", methodName);
                sb.AppendFormat("Line number : {0}", lineNumber);
                sb.AppendFormat("Error Message: {0}", ex.Message);

                LogInfo(sb.ToString());

            }
            catch (Exception genEx)
            {
                LogInfo((object)ex.Message);
                LogError(genEx);
            }
            finally
            {
                Dispose();
            }
        }

        private static void Dispose()
        {
            if (streamWriter != null)
            {
                streamWriter.Close();
                streamWriter.Dispose();
                streamWriter = null;
            }

            if (stackTrace != null)
                stackTrace = null;

            if (methodBase != null)
                methodBase = null;
        }
    }
}
