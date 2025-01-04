using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        ConsoleConfig.ShowWelcomeArt(); // useless. just for visuals..

        if (args.Length == 0 && RegistryUtils.IsAdmin())
        {
            RegistryUtils.ReplaceKey("roblox-player"); // robloxes key
            Console.WriteLine("[+] RobloxMinimalLauncher installed successfully");
            Console.WriteLine("press any key to continue..");
            Console.ReadKey();
        }
        else
        {
            // utilities
            if (RobloxClient.Singleton.TakeSingleton())
                Console.WriteLine("[-] Roblox singleton captured");

            // anything past here is required for the launcher to work
            Console.WriteLine("[-] Launching roblox..");
            RobloxClient.LaunchGame(args);

            // can be removed, but will break multi-instance
            while (RobloxClient.Singleton.HasRubberDucky && Process.GetProcessesByName("RobloxPlayerBeta").Length != 0) { Thread.Sleep(15); }
            RobloxClient.Singleton.ReleaseSingleton(); // roblox has closed
        }
    }
}