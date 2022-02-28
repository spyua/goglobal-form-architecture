using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;

namespace Dicon.Util
{
    public static class ProcessUtils
    {
        /// <summary>
        /// Kill a process, and all of its children, grandchildren, etc.
        /// </summary>
        /// <param name="pid">Process ID.</param>
        public static void KillProcessAndChildren(int pid)
        {
            // Cannot close 'system idle process'.
            if (pid == 0) return;

            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                    ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }

            KillProcess(pid);
        }

        /// <summary>
        /// 取得所有pid的Children Process
        /// </summary>
        /// <param name="pid">Process ID.</param>
        /// <returns>All Children Prcoess ID. Contain it self Process ID.</returns>
        public static IEnumerable<int> GetProcessChildren(int pid)
        {
            var childrenPID = new List<int>();
            childrenPID.Add(pid);

            ManagementObjectSearcher searcher = new ManagementObjectSearcher
             ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (ManagementObject mo in moc)
            {
                var PIDs = GetProcessChildren(Convert.ToInt32(mo["ProcessID"]));
                if (PIDs.Count() != 0)
                {
                    childrenPID.AddRange(GetProcessChildren(Convert.ToInt32(mo["ProcessID"])));
                }
            }

            return childrenPID;
        }

        /// <summary>
        /// 取得Process的PID
        /// </summary>
        /// <param name="name">Process的名稱</param>
        /// <returns>PID</returns>
        public static int GetProcessID(string name)
        {
            var processName = GetProcessName(name);
            var process = Process.GetProcesses().SingleOrDefault(p => p.ProcessName.Equals(processName));
            return (process != null) ? process.Id : 0;
        }

        public static string GetProcessName(int pid)
        {
            var process = Process.GetProcesses().SingleOrDefault(p => p.Id == pid);
            return process.ProcessName;
        }

        /// <summary>
        /// 取得Process Name，若找不到ProcessName，則回傳null
        /// </summary>
        public static string GetProcessName(string fileFullPath)
        {
            if (string.IsNullOrEmpty(fileFullPath)) throw new ArgumentNullException("Name can't empty or null");
            var fileName = Path.GetFileName(fileFullPath);
            var processName = Path.GetFileNameWithoutExtension(fileName);
            return processName;
        }

        /// <summary>
        /// 終止相同名稱的程式
        /// </summary>
        public static void KillProcess(string processName)
        {
            var pid = GetProcessID(processName);
            if (pid != 0) KillProcessAndChildren(pid);
        }

        public static void KillProcess(int pid)
        {
            try
            {
                var proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch
            {
                // Process already exited.
            }
        }

        /// <summary>
        /// 判斷Process是否存在
        /// </summary>
        /// <param name="name">Process的名稱，也可以傳入fileName</param>
        /// <returns></returns>
        public static bool IsProcessExists(string name)
        {
            // 名稱為空
            if (string.IsNullOrEmpty(name)) return false;

            // get process name
            var processName = GetProcessName(name);

            var allProcess = Process.GetProcesses();
            return allProcess.AsEnumerable().Any(p => processName.Equals(p.ProcessName));
        }

        /// <summary>
        /// 開啟應用程式
        /// </summary>
        /// <param name="fileFullPath">檔案名稱</param>
        /// <param name="openTimeoutMs">
        /// 小於 0: 不等待
        /// 等於 0: 無限等待
        /// 大於 0: 等待程式開啟的後的超時時間
        /// </param>
        /// <param name="arguments"></param>
        public static void OpenProcess(string fileFullPath, int openTimeoutMs = 0, string arguments = "")
        {
            if (string.IsNullOrEmpty(fileFullPath) || !File.Exists(fileFullPath)) return;

            using (var process = new Process())
            {
                process.StartInfo.FileName = fileFullPath;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;

                // 設定參數
                if (!string.IsNullOrEmpty(arguments)) process.StartInfo.Arguments = arguments;

                process.Start();

                // 確認開啟的超時時間
                if (openTimeoutMs < 0) { /* 不等待 */}
                else if (openTimeoutMs == 0) process.WaitForInputIdle();
                else process.WaitForInputIdle(openTimeoutMs);
            }
        }

        /// <summary>
        /// 關機功能
        /// </summary>
        public static void Shutdown(bool isReboot = false)
        {
            var mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();

            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            var mboShutdownParams = mcWin32.GetMethodParameters("Win32Shutdown");

            // Flag 1 means we want to shut down the system. Use "2" to reboot.
            mboShutdownParams["Flags"] = isReboot ? "2" : "1";
            mboShutdownParams["Reserved"] = "0";
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                manObj.InvokeMethod("Win32Shutdown", mboShutdownParams, null);
            }
        }
    }
}
