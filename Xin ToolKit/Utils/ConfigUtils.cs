using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.Storage;
using Newtonsoft.Json;

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

        //Debug.WriteLine("LoadState：" + App.LocalConfig.Plugin_Naraka);
    }

    public void SaveConfig(AppConfig app)
    {
        if (app != null)
        {
            _local.Values["Plugin_Naraka"] = app.Plugin_Naraka;
            //Debug.WriteLine("SaveState：" + app.Plugin_Naraka);
        }
    }
}