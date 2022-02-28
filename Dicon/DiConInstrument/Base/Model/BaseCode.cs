namespace DiconInstrument.Base.Model
{
    public class BaseCode
    {

        public string DetailCode;

        public string Description;

        public EventLevel Level;

        public BaseCode(string detailCode, string decription, EventLevel level)
        {
            DetailCode = detailCode;
            Description = decription;
            Level = level;
        }

        public enum EventLevel
        {
            /// <summary>
            /// 沒有異常
            /// </summary>
            None,

            /// <summary>
            /// 警報(可以繼續使用)
            /// </summary>
            Alarm,

            /// <summary>
            /// 異常(不可以繼續使用)
            /// </summary>
            Error,
        }
    }
}
