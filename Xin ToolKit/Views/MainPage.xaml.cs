using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Windows.System;
using Xin_ToolKit.Utils;
using Xin_ToolKit.ViewModels;
using ApplicationData = Microsoft.Windows.Storage.ApplicationData;

namespace Xin_ToolKit.Views;

public sealed partial class MainPage : Page
{
    private readonly StorageFolder storageFolder = ApplicationData.GetDefault().LocalFolder;

    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }

    private async void MenuFlyoutItem_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        MenuFlyoutItem_Menu.IsEnabled = false;
        var path = storageFolder.Path + "/Photo/";
        var data = path + "image_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".webp";
        var result = await DownloadUtils.DownLoadAsync(ViewModel.ImageUri, data);
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
                await Launcher.LaunchFolderAsync(storageFolder);
            };
            await dialog.ShowAsync();
            MenuFlyoutItem_Menu.IsEnabled = true;
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
            MenuFlyoutItem_Menu.IsEnabled = true;
        }
    }
}