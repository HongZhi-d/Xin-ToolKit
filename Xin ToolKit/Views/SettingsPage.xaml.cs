using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.System;
using Xin_ToolKit.Utils;
using Xin_ToolKit.ViewModels;
using ApplicationData = Microsoft.Windows.Storage.ApplicationData;

namespace Xin_ToolKit.Views;

public sealed partial class SettingsPage : Page
{
    private readonly StorageFolder storageFolder = ApplicationData.GetDefault().LocalFolder;

    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
        CheckPicturlPath();
    }

    private void CheckPicturlPath()
    {
        var mode = App.LocalConfig.DefaultSavePath;
        switch (mode)
        {
            case 0:
                DefaultPath_Combo.SelectedIndex = 0;
                if (!string.IsNullOrEmpty(App.LocalConfig.PictureSavePath))
                {
                    DefaultSavePath_edit.Text = App.LocalConfig.PictureSavePath;
                }
                else
                {
                    DefaultSavePath_edit.Text = storageFolder.Path + "/Photo/";
                    App.LocalConfig.PictureSavePath = DefaultSavePath_edit.Text;
                }
                break;

            case 1:
                DefaultPath_Combo.SelectedIndex = 1;
                if (!string.IsNullOrEmpty(App.LocalConfig.PictureSavePath))
                {
                    DefaultSavePath_edit.Text = App.LocalConfig.PictureSavePath;
                }
                else
                {
                    DefaultSavePath_edit.Text = storageFolder.Path + "/Photo/";
                    App.LocalConfig.PictureSavePath = DefaultSavePath_edit.Text;
                }
                break;
        }
    }

    private void ThemeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (Theme_Combo.SelectedIndex)
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

    private async void DefaultPathCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (DefaultPath_Combo.SelectedIndex)
        {
            case 0:
                DefaultSavePath_edit.Text = storageFolder.Path + "/Photo/";
                App.LocalConfig.PictureSavePath = DefaultSavePath_edit.Text;
                App.LocalConfig.DefaultSavePath = 0;
                break;

            case 1:
                if (App.LocalConfig.PictureSavePath == storageFolder.Path + "/Photo/")
                {
                    DefaultSavePath_edit.Text = "";

                    var openPicker = new Windows.Storage.Pickers.FolderPicker();

                    var window = App.MainWindow;

                    var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);

                    WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);

                    openPicker.SuggestedStartLocation = PickerLocationId.Desktop;
                    openPicker.FileTypeFilter.Add("*");

                    var folder = await openPicker.PickSingleFolderAsync();
                    if (folder != null)
                    {
                        StorageApplicationPermissions.FutureAccessList.AddOrReplace("PickedFolderToken", folder);
                        DefaultSavePath_edit.Text = folder.Path + "\\";
                        App.LocalConfig.PictureSavePath = DefaultSavePath_edit.Text;
                        App.LocalConfig.DefaultSavePath = 1;
                    }
                    else
                    {
                        DefaultSavePath_edit.Text = "操作取消";
                        DefaultPath_Combo.SelectedIndex = 0;
                        App.LocalConfig.DefaultSavePath = 0;
                    }
                }
                break;
        }
    }

    private async void SavePicture_Click(object sender, RoutedEventArgs e)
    {
        SavePicture_Button.IsEnabled = false;
        var data = DefaultSavePath_edit.Text + "image_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".webp";
        Debug.WriteLine(App.LocalConfig.PublicImgUri);
        var result = await DownloadUtils.DownLoadAsync(App.LocalConfig.PublicImgUri, data);
        if (result)
        {
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Xin ToolKit",
                Content = "照片下载成功！",
                PrimaryButtonText = "查看照片",
                CloseButtonText = "取消",
                DefaultButton = ContentDialogButton.Primary,
                RequestedTheme = ((FrameworkElement)sender).ActualTheme
            };
            dialog.PrimaryButtonClick += async (s, args) =>
            {
                if (DefaultPath_Combo.SelectedIndex != 0)
                {
                    await Launcher.LaunchFolderPathAsync(App.LocalConfig.PictureSavePath);
                }
                else
                {
                    await Launcher.LaunchFolderAsync(storageFolder);
                }
            };
            await dialog.ShowAsync();
            SavePicture_Button.IsEnabled = true;
        }
        else
        {
            var dialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Title = "Xin ToolKit",
                Content = "图片下载失败！",
                PrimaryButtonText = "确认",
                CloseButtonText = "取消",
                DefaultButton = ContentDialogButton.Primary,
                RequestedTheme = ((FrameworkElement)sender).ActualTheme
            };
            await dialog.ShowAsync();
            SavePicture_Button.IsEnabled = true;
        }
    }
}