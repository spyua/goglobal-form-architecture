using System;
using System.Text;

namespace DiconInstrument.Core.Help
{
    public class LogBuilder
    {

        public static string LogInfo(string className, string methodRows, string errorMessage, string detailMessage)
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("Date Time:" + DateTime.Now);
            strBuilder.Append("Class:" + className);
            strBuilder.Append("Row:" + methodRows);
            strBuilder.Append("Message:"+errorMessage);
            strBuilder.Append("Detail Message:" + detailMessage);

            return strBuilder.ToString();
        }
    }
}
