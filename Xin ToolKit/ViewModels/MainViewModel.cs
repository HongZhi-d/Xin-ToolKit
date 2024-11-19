using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Media.Imaging;

namespace Xin_ToolKit.ViewModels;

public partial class MainViewModel : ObservableRecipient
{
    [ObservableProperty]
    private BitmapImage bitmapImage;

    [ObservableProperty]
    private string imageUri;

    public MainViewModel()
    {
        Random random = new();
        imageUri = "https://www.loliapi.com/acg/pc/?id=" + random.Next(1, 699);
        bitmapImage = SetNetworkImageAsBackground(imageUri);
    }

    private static BitmapImage SetNetworkImageAsBackground(string imageUrl)
    {
        var uri = new Uri(imageUrl);
        var bitmapImage = new BitmapImage
        {
            UriSource = uri,
            CreateOptions = BitmapCreateOptions.IgnoreImageCache
        };
        return bitmapImage;
    }
}