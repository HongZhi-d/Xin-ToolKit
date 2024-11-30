using Microsoft.UI.Xaml.Controls;

using Xin_ToolKit.ViewModels;

namespace Xin_ToolKit.Views;

public sealed partial class UserPage : Page
{
    public UserViewModel ViewModel
    {
        get;
    }

    public UserPage()
    {
        ViewModel = App.GetService<UserViewModel>();
        InitializeComponent();
    }
}
