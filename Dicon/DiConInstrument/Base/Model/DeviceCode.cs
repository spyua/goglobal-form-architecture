namespace DiconInstrument.Base.Model
{
    public class DeviceCode : BaseCode
    {

        public static readonly DeviceCode NoError = new DeviceCode("00", "No Error", EventLevel.None);

        public static readonly DeviceCode ScanTimeOut = new DeviceCode("01", "Scan device time out", EventLevel.Error);
        public static readonly DeviceCode OpenComPortError = new DeviceCode("02", "Open commport error", EventLevel.Error);
        public static readonly DeviceCode CLoseComPortError = new DeviceCode("03", "Close commport error", EventLevel.Error);
        public static readonly DeviceCode SendCommandError = new DeviceCode("04", "Send data error", EventLevel.Error);
        public static readonly DeviceCode ReadCommandError = new DeviceCode("05", "Read data error", EventLevel.Error);
        public DeviceCode(string detailCode, string decription, EventLevel level):base(detailCode, decription, level)
        {

        }

    }
}
