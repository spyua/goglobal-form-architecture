using System.Threading.Tasks;

namespace DiconInstrument.Com
{
    public interface IControl
    {
        bool Connect();

        bool DisConnect();

        bool SendCmd(string command);
        
        string ReadData();

        Task<string> SendCmdAsync(string command, bool waitResponse = false);
        Task<string> ReadDataAsync(bool waitResponse = false);
        Task<bool> SendCmdAsync(string command, int millisecondsDelayWaitComplete);

    }
}
