using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

using Microsoft.Win32;

class Program
{
    static void ReplaceKey(string app)
    {
        RegistryKey key = Registry.ClassesRoot.OpenSubKey($"{app}\\shell\\open\\command", true);
        key.SetValue(string.Empty,
            $"\"{AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName}\" %1");
        key.Close();
    }

    [STAThread]
    static void Main(string[] args)
    {
        Console.WindowWidth = 51;
        Console.WindowHeight = 30;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(@"     ░▒▓███████▓▒░░▒▓███████▓▒░░▒▓█▓▒░░▒▓█▓▒░ 
     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
     ░▒▓███████▓▒░░▒▓███████▓▒░ ░▒▓██████▓▒░  
     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
     ░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓█▓▒░░▒▓█▓▒░ 
     ░▒▓█▓▒░░▒▓█▓▒░▒▓███████▓▒░░▒▓█▓▒░░▒▓█▓▒░");
        Console.WriteLine("\n[NOTE] this wont update roblox");
        Console.WriteLine("   Open source project at \n   github.com/Laamy/RobloxMinimalLauncher\n");
        if (args.Length == 0 && new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
        {
            ReplaceKey("roblox-player"); // robloxes key
            Console.WriteLine("[+] RobloxMinimalLauncher installed successfully");
            Console.WriteLine("press any key to continue..");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("[-] Launching roblox..");

            //C:\Users\yeemi\AppData\Local\Roblox\Versions
            var localappdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var robloxVersions = Directory.GetDirectories(Path.Combine(localappdata, "Roblox\\Versions"));

            var rbx = Process.Start(robloxVersions[robloxVersions.Length - 1] + "\\RobloxPlayerBeta.exe", args[0]);

            while (rbx.MainWindowHandle == IntPtr.Zero && rbx.MainWindowTitle != "Roblox") { }
        }
    }
}