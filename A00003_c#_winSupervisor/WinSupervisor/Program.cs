using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Management;
using System.IO;
namespace WinSupervisor
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                if (CheckQueueListenProcess())
                {
                  //  Console.WriteLine("live");
                }
                else
                {
                   // Console.WriteLine("dead");
                    System.Diagnostics.Process.Start("Listener.bat");
                }
                System.Threading.Thread.Sleep(5000);//5秒
            }

        }

        private static bool CheckQueueListenProcess()
        {

            using (ManagementObjectSearcher mos = new ManagementObjectSearcher(
       "SELECT CommandLine FROM Win32_Process "))
            {
                foreach (ManagementObject mo in mos.Get())
                {
                    string cmd = Convert.ToString(mo["CommandLine"]);
                    if (cmd.IndexOf("php  artisan queue:work  --daemon") != -1)
                    {

                        return true;
                    }
                }
            }
            return false;
        }

    }
}
