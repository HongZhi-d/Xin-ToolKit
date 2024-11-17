namespace Xin_ToolKit;

public partial record AppConfig
{
    #region 插件设置

    public bool Plugin_Naraka
    {
        get; set;
    } = false;

    #endregion 插件设置

    #region 用户设置

    public bool NewUser
    {
        get; set;
    } = true;

    public string? UserName
    {
        get; set;
    }
    public string? Password
    {
        get; set;
    }

    #endregion 用户设置
}