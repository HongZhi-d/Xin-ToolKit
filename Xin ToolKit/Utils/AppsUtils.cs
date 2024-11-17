using Microsoft.Win32;

namespace Xin_ToolKit.Utils;

public class InstalledAppInfo
{
    public string? DisplayName
    {
        get; set;
    }

    public string? DisplayVersion
    {
        get; set;
    }

    public string? Publisher
    {
        get; set;
    }

    public string? InstallLocation
    {
        get; set;
    }

    public string? UninstallString
    {
        get; set;
    }
}

internal class AppsUtils
{
    internal static async Task<InstalledAppInfo> GetInstalledAppAsync(string appName)
    {
        InstalledAppInfo? appInfo = null;
        if (appInfo == null)
        {
            throw new InvalidOperationException("Fuck You.");
        }
        await Task.Run(() =>
        {
            // 访问HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall
            var uninstallKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                .OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");

            if (uninstallKey != null)
            {
                foreach (var subKeyName in uninstallKey.GetSubKeyNames())
                {
                    using var subKey = uninstallKey.OpenSubKey(subKeyName);
                    if (subKey != null)
                    {
                        var displayName = subKey.GetValue("DisplayName") as string;
                        if (!string.IsNullOrEmpty(displayName) && displayName.Equals(appName, StringComparison.OrdinalIgnoreCase))
                        {
                            appInfo = new InstalledAppInfo
                            {
                                DisplayName = displayName,
                                DisplayVersion = subKey.GetValue("DisplayVersion") as string,
                                Publisher = subKey.GetValue("Publisher") as string,
                                InstallLocation = subKey.GetValue("InstallLocation") as string,
                                UninstallString = subKey.GetValue("UninstallString") as string
                            };
                            break; // 找到后退出循环
                        }
                    }
                }
            }

            // 访问HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall 用于32位应用程序在64位系统上
            var wow6432NodeKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)
                .OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

            if (wow6432NodeKey != null && appInfo == null) // 只在第一次搜索未找到时搜索Wow6432Node
            {
                foreach (var subKeyName in wow6432NodeKey.GetSubKeyNames())
                {
                    using var subKey = wow6432NodeKey.OpenSubKey(subKeyName);
                    if (subKey != null)
                    {
                        var displayName = subKey.GetValue("DisplayName") as string;
                        if (!string.IsNullOrEmpty(displayName) && displayName.Equals(appName, StringComparison.OrdinalIgnoreCase))
                        {
                            appInfo = new InstalledAppInfo
                            {
                                DisplayName = displayName,
                                DisplayVersion = subKey.GetValue("DisplayVersion") as string,
                                Publisher = subKey.GetValue("Publisher") as string,
                                InstallLocation = subKey.GetValue("InstallLocation") as string,
                                UninstallString = subKey.GetValue("UninstallString") as string
                            };
                            break; // 找到后退出循环
                        }
                    }
                }
            }
        });

        return appInfo;
    }
}