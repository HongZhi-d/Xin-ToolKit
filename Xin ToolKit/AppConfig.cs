﻿namespace Xin_ToolKit;

public partial record AppConfig
{
    #region 插件设置

    public bool Plugin_Naraka
    {
        get; set;
    } = false;

    #endregion 插件设置

    #region 用户设置

    public bool OldUser
    {
        get; set;
    }

    public string? UserName
    {
        get; set;
    }

    public string? Password
    {
        get; set;
    }

    #endregion 用户设置

    #region 其他设置

    public int DefaultSavePath
    {
        get; set;
    } = 0;

    public string? PictureSavePath
    {
        get; set;
    }

    public string? PublicImgUri
    {
        get; set;
    }

    #endregion 其他设置
}