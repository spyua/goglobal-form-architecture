using System.IO;
using System.Text;

namespace Dicon.Project.Swtich.Testing.Core.Define
{
    public static class SystemDef
    {
        // Ini 相關定義
        public static string IniFilePath = Path.Combine(IniPath(), "SystemConfig.ini");
        public static string IniPath()
        {
            var exePath = System.AppDomain.CurrentDomain.BaseDirectory;
            var pathlist = exePath.Split('\\');
            var pathStr = new StringBuilder();
            for (int i = 0; i < pathlist.Length - 4; i++)
                pathStr.Append(pathlist[i] + "\\");

            return pathStr.ToString();
        }

        // Ini Section Key定義
        public static string IniUARTComSection = "UARTCom";
        public static string IniM2BoardComKey = "MS2BoardCom";
        public static string IniPDBoardComKey = "PDBoardCom";
        public static string IniUCBComKey = "UCBCom";
    }
}
