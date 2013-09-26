using System;
using System.Diagnostics;

namespace windows_uac_demo
{
    class Program
    {
        private const string OptionUac = "/uac";

        static private void Elevate()
        {
            // Start this executable as a child process with with the command line option '/uac'
            string processName = Process.GetCurrentProcess().MainModule.FileName;
            ProcessStartInfo processStartInfo = new ProcessStartInfo("\"" + processName + "\"", OptionUac);

            // Set runas verb if Windows Vista or greater to enable UAC prompt
            OperatingSystem os = Environment.OSVersion;
            if (os.Platform == PlatformID.Win32NT &&
                os.Version.Major == 6 &&
                os.Version.Minor >= 0)
            {
                // The magic 'runas' verb
                processStartInfo.Verb = "runas";
            }

            // Launch this process. The new process will execute RunElevatedCode()
            Process process = Process.Start(processStartInfo);
            process.WaitForExit();
        }

        static private void RunElevatedCode()
        {
            // Perform your registry changes, program/dll installation or anything else
            //   you require elevated privileges for here. If done properly, it will
            //   seem to the user that no other process has been launched.

            // This demo will launch a command line window. You can verify it's running as
            //   Administrator by observing the window title. Enter 'exit' or click the X
            //   box in this window to complete the demo
            Process process = Process.Start("cmd");
            if (process != null)
            {
                // Wait for 'exit' to be entered or X to be clicked
                process.WaitForExit();
            }
        }

        static void Main(string[] args)
        {
            // Check for '/uac' option and if given, execute elevated code
            if (args.Length == 1 && args[0] == OptionUac)
            {
                // Note that elevated code is running in a child process and not in the
                //   context of your current (parent) process
                RunElevatedCode();
            }
            else
            {
                // The initial execution of this program (without the '/uac' command line option)
                //   will cause this program to execute itself with the '/uac' command line option
                //   and with elevated privileges
                Elevate();
            }
        }
    }
}
