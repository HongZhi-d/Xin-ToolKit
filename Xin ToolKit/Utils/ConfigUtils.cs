using System.Diagnostics;
using Microsoft.Windows.Storage;

namespace Xin_ToolKit.Utils;

internal class ConfigUtils
{
    private readonly ApplicationDataContainer _local = ApplicationData.GetDefault().LocalSettings;

    public T? ReadConfig<T>(string key)
    {
        return _local.Values[key] is T t ? t : default;
    }

    public void LoadConfig()
    {
        App.LocalConfig.Plugin_Naraka = ReadConfig<bool>("Plugin_Naraka");
        App.LocalConfig.NewUser = ReadConfig<bool>("NewUser");
        Debug.WriteLine("LoadState：" + App.LocalConfig.NewUser);
    }

    public void SaveConfig(AppConfig app)
    {
        if (app != null)
        {
            _local.Values["Plugin_Naraka"] = app.Plugin_Naraka;
            _local.Values["NewUser"] = app.NewUser;
            Debug.WriteLine("SaveState：" + app.NewUser);
        }
    }
}