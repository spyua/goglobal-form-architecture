using Dicon.Project.Swtich.Testing;
using System;
using System.Windows.Forms;

namespace Dicon.Project.Swtich.Test
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //啟動
            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
