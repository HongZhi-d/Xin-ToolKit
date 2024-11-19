using Microsoft.UI.Xaml;
using Windows.UI.ViewManagement;
using Windows.UI.WindowManagement;
using WinUIEx;
using Xin_ToolKit.Helpers;
using Xin_ToolKit.Utils;

namespace Xin_ToolKit;

public sealed partial class MainWindow : WindowEx
{
    private readonly Microsoft.UI.Dispatching.DispatcherQueue dispatcherQueue;

    private readonly UISettings settings;

    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/logo_ico.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();

        dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
        settings = new UISettings();
        settings.ColorValuesChanged += Settings_ColorValuesChanged;

        TrySetDesktopAcrylicBackdrop();

        ConfigUtils configUtils = new();
        configUtils.LoadConfig();
    }

    private void MainWindowClose(object sender, WindowEventArgs e)
    {
        ConfigUtils configUtils = new();
        configUtils.SaveConfig(App.LocalConfig);
    }

    private void Settings_ColorValuesChanged(UISettings sender, object args)
    {
        dispatcherQueue.TryEnqueue(() =>
        {
            TitleBarHelper.ApplySystemThemeToCaptionButtons();
        });
    }

    private bool TrySetDesktopAcrylicBackdrop()
    {
        if (Microsoft.UI.Composition.SystemBackdrops.DesktopAcrylicController.IsSupported())
        {
            var DesktopAcrylicBackdrop = new Microsoft.UI.Xaml.Media.DesktopAcrylicBackdrop();
            SystemBackdrop = DesktopAcrylicBackdrop;

            return true;
        }

        return false;
    }
}