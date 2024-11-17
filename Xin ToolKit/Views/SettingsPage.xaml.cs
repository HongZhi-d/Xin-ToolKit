using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel.Core;
using Xin_ToolKit.ViewModels;

namespace Xin_ToolKit.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (ThemeCombo.SelectedIndex)
        {
            case 0:
                ViewModel.SwitchThemeCommand.Execute(ElementTheme.Default);
                break;

            case 1:
                ViewModel.SwitchThemeCommand.Execute(ElementTheme.Light);
                break;

            case 2:
                ViewModel.SwitchThemeCommand.Execute(ElementTheme.Dark);
                break;
        }
    }

}