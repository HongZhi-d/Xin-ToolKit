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
        App.LocalConfig.PictureSavePath = ReadConfig<string>("PictureSavePath");
        App.LocalConfig.DefaultSavePath = ReadConfig<int>("DefaultSavePath");
        Debug.WriteLine("LoadState：" + App.LocalConfig.DefaultSavePath);
        Debug.WriteLine("LoadPath：" + App.LocalConfig.PictureSavePath);
    }

    public void SaveConfig(AppConfig app)
    {
        if (app != null)
        {
            // 布尔值
            _local.Values["Plugin_Naraka"] = app.Plugin_Naraka;
            _local.Values["NewUser"] = app.NewUser;

            // 整数
            _local.Values["DefaultSavePath"] = app.DefaultSavePath;

            // 字符
            _local.Values["PictureSavePath"] = app.PictureSavePath;

            Debug.WriteLine("LoadState：" + App.LocalConfig.DefaultSavePath);
            Debug.WriteLine("LoadPath：" + App.LocalConfig.PictureSavePath);
        }
    }
}