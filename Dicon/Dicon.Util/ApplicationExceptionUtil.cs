using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Dicon.Util
{
    /// <summary>
    /// App Exception相關處理
    /// </summary>
    public class ApplicationExceptionUtil
    {
        public string FileName { get; private set; }

        public string DirectoryPath { get; private set; }

        public ApplicationExceptionUtil()
           : this(Assembly.GetExecutingAssembly().GetName().Name,
                 Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
        {
        }

        public ApplicationExceptionUtil(string fileName, string directory)
        {
            FileName = fileName;
            DirectoryPath = directory;
        }

        /// <summary>
        /// 發生應用程式執行緒無法預測的例外
        /// </summary>
        public void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            CatchException(e.Exception as Exception, new StackTrace(true));
        }

        /// <summary>
        /// 發生應用程式無法預測的例外
        /// </summary>
        public void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            CatchException(e.ExceptionObject as Exception, new StackTrace(true));
        }

        public Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // 取得設定檔
            var unAssemblyResolve = string.Format("{0}_{1}.txt", FileName, "UnAssemblyResolve");
            var unAssemblyResolvePath = Path.Combine(DirectoryPath, unAssemblyResolve);

            var path = Path.Combine(DirectoryPath, @"Libs\");
            PrintToFile(unAssemblyResolvePath, args.Name.ToString());
            if (args.Name.Split(',')[0].Contains(".dll"))
            {
                path = Path.Combine(path, args.Name.Split(',')[0]);
                path = string.Format(@"{0}.dll", path);
                return Assembly.LoadFrom(path);
            }

            return args.RequestingAssembly;
        }

        public void CatchException(Exception exception)
        {
            CatchException(exception, new StackTrace(true));
        }

        private void CatchException(Exception exception, StackTrace stackTrace)
        {
            CatchException(GetUnhandleLog(), exception, stackTrace);
        }

        private void CatchException(string unhandleLogPath, Exception exception, StackTrace stackTrace)
        {
            try
            {
                var errorMsg = string.Empty;
                if (exception != null) errorMsg = exception.GetExceptionDetail();
                else
                {
                    if (stackTrace == null) stackTrace = new StackTrace(true);
                    errorMsg = stackTrace.ToString();
                }

                PrintToFile(unhandleLogPath, errorMsg);
            }
            catch (Exception ex)
            {
                PrintToFile(unhandleLogPath, ex.ToString());
            }
        }

        private string GetUnhandleLog()
        {
            var unhandleLog = string.Format("{0}_{1}.txt", FileName, "UnhandleLog");
            return Path.Combine(DirectoryPath, unhandleLog);
        }

        private void PrintToFile(string filePath, string content, bool appendTime = true)
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            File.AppendAllText(filePath, GetPrintContent(content, appendTime), Encoding.UTF8);
        }

        private string GetPrintContent(string content, bool appendTime)
        {
            return ((appendTime) ? content + " " + DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss.fff]") : content) + Environment.NewLine;
        }
    }
}
