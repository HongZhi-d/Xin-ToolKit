using Microsoft.UI.Xaml.Controls;
using Windows.Storage;
using Microsoft.Windows.Storage;
using Xin_ToolKit.ViewModels;
using ApplicationData = Microsoft.Windows.Storage.ApplicationData;
using Xin_ToolKit.Utils;
using Microsoft.UI.Xaml;
using System.IO.Compression;
using Windows.System;
using System.Diagnostics;
using Xin_ToolKit.Custom;
using Windows.Foundation.Metadata;
using Microsoft.UI.Xaml.Markup;

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
        var MainPageRoot = Content as FrameworkElement;
        if (MainPageRoot != null)
        {
            MainPageRoot.Loaded += async (s, e) => await CheckUseState();
        }
    }

    private async Task CheckUseState()
    {
        var isNew = App.LocalConfig.NewUser;
        if (isNew)
        {
            await AgreementDialog.ShowAsync();
        }
    }

    private void AgreementDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        App.LocalConfig.NewUser = false;
    }

    private void AgreementDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
         Process.GetCurrentProcess().Kill();
    }

    private async void MenuFlyoutItem_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var path = storageFolder.Path + "/Photo/";
        var data = path + "image_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png";
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
                DefaultButton = ContentDialogButton.Primary
            };
            dialog.PrimaryButtonClick += async (s, args) =>
            {
                await Launcher.LaunchFolderAsync(storageFolder);
            };
            await dialog.ShowAsync();
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
                DefaultButton = ContentDialogButton.Primary
            };
            await dialog.ShowAsync();
        }
    }


}