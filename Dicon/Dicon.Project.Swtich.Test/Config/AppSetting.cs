using Dicon.Project.Swtich.Testing.Core.Define;
using Dicon.Util;
using System.Configuration;

namespace Dicon.Project.Swtich.Testing.Config
{
    /// <summary>
    /// App Config and Ini Setting (Static)
    /// </summary>
    public class AppSetting
    {
        public IniUtil IniManager { get; set; }

        public static class SingletonHolder
        {
            static SingletonHolder() { }

            internal static readonly AppSetting INSTANCE = new AppSetting();
        }

        public static AppSetting Instance { get { return SingletonHolder.INSTANCE; } }

        /// <summary>
        /// 1XN DB
        /// </summary>
        public string MSSQL1XNDBCon { get; private set; }

        /// <summary>
        /// ERmeasurement DB 
        /// </summary>
        public string MSSQLERMDBCon { get; private set; }

        public string MS2BoardComPort { get; private set; }

        public string PDBoardComPort { get; private set; }

        public string UCBComPort { get; private set; }

        public AppSetting()
        {
            IniManager = new IniUtil(SystemDef.IniFilePath);

            MSSQL1XNDBCon = ConfigurationManager.ConnectionStrings["MSSQL_1XNDB"].ConnectionString;
            MSSQLERMDBCon = ConfigurationManager.ConnectionStrings["MSSQL_ERmeasurementDB"].ConnectionString;

            MS2BoardComPort = IniManager.ReadIni(SystemDef.IniUARTComSection, SystemDef.IniM2BoardComKey);
            PDBoardComPort = IniManager.ReadIni(SystemDef.IniUARTComSection, SystemDef.IniPDBoardComKey);
            UCBComPort = IniManager.ReadIni(SystemDef.IniUARTComSection, SystemDef.IniUCBComKey);
        }

   
        private string GetConfigValue(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }
        private int GetConfigIntVaule(string value)
        {
            return int.Parse(ConfigurationManager.AppSettings[value]);
        }
    }
}
