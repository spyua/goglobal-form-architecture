namespace DiconInstrument.Core.Def
{
    /// <summary>
    /// IEEE 488.2 Common Commands
    /// https://rfmw.em.keysight.com//spdhelpfiles/truevolt/webhelp/US/Content/__I_SCPI/IEEE-488_Common_Commands.htm#IDN
    /// </summary>
    public class IEEE4882Command 
    {
        public static class SingletonHolder
        {
            static SingletonHolder() { }
            internal static readonly IEEE4882Command INSTANCE = new IEEE4882Command();
        }

        public static IEEE4882Command Instance { get { return SingletonHolder.INSTANCE; } }

        public string QueryIdentifyDevice = "*IDN?";

        public string ClearDeviceStatus = "*CLS";

        public string SetOperationComplete = "*OPC";

        public string QueryOperationComplete = "*OPC?";
    }
}
