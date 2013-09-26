namespace windows_uac_demo
{
    class Program
    {
        private const string OptionUac = "/uac";

        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == OptionUac) {
                System.Diagnostics.Process process = System.Diagnostics.Process.Start("cmd");
                if (process != null) process.WaitForExit();
            }
            else {
                System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo(
                    "\"" + System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName + "\"", OptionUac);

                System.OperatingSystem os = System.Environment.OSVersion;
                if (os.Platform == System.PlatformID.Win32NT && os.Version.Major == 6 && os.Version.Minor >= 0) {
                    processStartInfo.Verb = "runas";
                }

                (System.Diagnostics.Process.Start(processStartInfo)).WaitForExit();
            }
        }
    }
}
