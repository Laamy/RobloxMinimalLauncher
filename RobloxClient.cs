using System.Diagnostics;
using System.IO;
using System;
using System.Threading;

internal class RobloxClient
{
    /// <summary>
    /// Note that this is just an example. I recommend getting the latest version and checking for that folder instead
    /// </summary>
    /// <param name="args"></param>
    internal static void LaunchGame(string[] args)
    {
        //C:\Users\yeemi\AppData\Local\Roblox\Versions
        var localappdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        var robloxVersions = Directory.GetDirectories(Path.Combine(localappdata, "Roblox\\Versions"));

        var rbx = Process.Start(robloxVersions[robloxVersions.Length - 1] + "\\RobloxPlayerBeta.exe", args[0]);

        while (rbx.MainWindowHandle == IntPtr.Zero && rbx.MainWindowTitle != "Roblox") { Thread.Sleep(15); }
    }

    internal class Singleton
    {
        private static Mutex robloxMutex = null;

        /// <summary>
        /// Will return if this program has captured the roblox singleton mutex
        /// </summary>
        public static bool HasRubberDucky => robloxMutex != null;

        /// <summary>
        /// Take the roblox singleton mutex (NOTE: please call this before roblox has launched)
        /// </summary>
        internal static bool TakeSingleton()
        {
            if (robloxMutex == null)
            {
                try
                {
                    bool createdNew;
                    robloxMutex = new Mutex(true, "ROBLOX_singletonMutex", out createdNew);

                    if (createdNew)
                    {
                        return true;
                    }
                    else robloxMutex = null;
                }
                catch { }
            }
            return false;
        }

        /// <summary>
        /// Release the roblox singleton mutex if not already taken
        /// </summary>
        internal static bool ReleaseSingleton()
        {
            if (robloxMutex != null)
            {
                try
                {
                    robloxMutex.Close();
                    robloxMutex.Dispose();
                    return true;
                }
                catch {}
            }
            return false;
        }
    }
}