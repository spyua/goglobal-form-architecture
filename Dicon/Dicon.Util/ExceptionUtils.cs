using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dicon.Util
{
    public static class ExceptionUtils
    {
        public static string GetExceptionDetail(this Exception exception)
        {
            var stringBuilder = new StringBuilder();
            try
            {
                stringBuilder.AppendLine("-----------------------------------------------------------------------------");
                stringBuilder.AppendLine("Date : " + DateTime.Now.ToString());
                stringBuilder.AppendLine();

                while (exception != null)
                {
                    stringBuilder.AppendLine(exception.GetType().FullName);
                    stringBuilder.AppendLine("Message : " + exception.Message);
                    stringBuilder.AppendLine("StackTrace : " + exception.StackTrace);
                    exception = exception.InnerException;
                }

                return stringBuilder.ToString();
            }
            finally
            {
                stringBuilder.Clear();
            }
        }

        public static void CheckException(Action onExecute, Action<Exception> onException, Action onFinally = null)
        {
            try
            {
                onExecute?.Invoke();
            }
            catch (Exception e)
            {
                onException?.Invoke(e);
            }
            finally
            {
                onFinally?.Invoke();
            }
        }
    }
}
