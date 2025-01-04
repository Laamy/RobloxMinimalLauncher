using System;
using System.Security.Principal;
using Microsoft.Win32;

// not much to explain here
// this class is used to replace the roblox registry keys with our own
internal class RegistryUtils
{
    internal static bool IsAdmin() => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

    internal static void ReplaceKey(string app)
    {
        RegistryKey key = Registry.ClassesRoot.OpenSubKey($"{app}\\shell\\open\\command", true);
        key.SetValue(string.Empty,
            $"\"{AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName}\" %1");
        key.Close();
    }
}