using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Xin_ToolKit.Custom;
using Xin_ToolKit.ViewModels;

namespace Xin_ToolKit.Views;

public sealed partial class PluginPage : Page
{
    public PluginViewModel ViewModel
    {
        get;
    }

    public PluginPage()
    {
        ViewModel = App.GetService<PluginViewModel>();
        InitializeComponent();
        CheckPluginState();
        Debug.WriteLine(Application.Current.GetType().Assembly.Location);
    }

    private void CheckPluginState()
    {
        var isCheck = App.LocalConfig.Plugin_Naraka;
        if (!isCheck)
        {
            Naraka_Plugin_0.Content = "启用插件";
            Naraka_Plugin_0.IsChecked = isCheck;
        }
        else
        {
            Naraka_Plugin_0.Content = "关闭插件";
            Naraka_Plugin_0.IsChecked = isCheck;
        }
    }

    private void Naraka_Plugin_0_Click(object sender, RoutedEventArgs e)
    {
        var isOpen = Naraka_Plugin_0.IsChecked;
        if (isOpen == false)
        {
            Naraka_Plugin_0.Content = "启用插件";
            App.LocalConfig.Plugin_Naraka = false;
        }
        else
        {
            Naraka_Plugin_0.Content = "关闭插件";
            App.LocalConfig.Plugin_Naraka = true;
        }
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new ContentDialog
        {
            XamlRoot = XamlRoot,
            Title = "设置",
            PrimaryButtonText = "保存",
            CloseButtonText = "取消",
            DefaultButton = ContentDialogButton.Primary,
            Content = new XinDialog(),
            RequestedTheme = ((FrameworkElement)sender).ActualTheme
        };

        await dialog.ShowAsync();
    }
}