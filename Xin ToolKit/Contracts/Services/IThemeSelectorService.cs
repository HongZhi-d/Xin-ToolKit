using Microsoft.UI.Xaml;

namespace Xin_ToolKit.Contracts.Services;

public interface IThemeSelectorService
{
    ElementTheme Theme
    {
        get;
    }

    Task InitializeAsync();

    Task SetThemeAsync(ElementTheme theme);

    Task SetRequestedThemeAsync();
}