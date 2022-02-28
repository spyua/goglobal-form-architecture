using Dicon.Project.Swtich.Test;
using Dicon.Project.Swtich.Testing.Config;
using Dicon.Util;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Dicon.Project.Swtich.Testing
{
    /// <summary>
    /// 客製化啟動程序
    /// </summary>
    public class Bootstrapper
    {
        private readonly IServiceProvider _sysServiceProvider;

        public Bootstrapper()
        {
            _sysServiceProvider = ServiceConfigure.GetProvider();
        }

        public void Run()
        {
            RegisterCrashDetectEvent();
            StarFormApp();
        }

        /// <summary>
        /// 初始化應用程式的錯誤服務訊息-抓無預警Crash錯誤
        /// </summary>
        private void RegisterCrashDetectEvent()
        {
           
            var applicationException = new ApplicationExceptionUtil();
            Application.ThreadException += applicationException.Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += applicationException.CurrentDomain_UnhandledException;
        }
        private void StarFormApp()
        {
            var form = CreateMainForm();

            form.FormClosing += (o, e) =>
            {
                // Close Event. 將Process砍乾淨
                ProcessUtils.KillProcessAndChildren(Process.GetCurrentProcess().Id);
            };

            Application.Run(form);
        }
        private Form CreateMainForm()
        {
            var form = _sysServiceProvider.GetRequiredService<MainForm>();
            return form;
        }
    }
}
