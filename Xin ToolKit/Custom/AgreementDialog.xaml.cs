
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.ApplicationModel.Resources;
using Xin_ToolKit.ViewModels;

namespace Xin_ToolKit.Custom;
public sealed partial class AgreementDialog : UserControl
{
    public bool Agree
    {
        get => (bool)GetValue(AgreeProperty);
        set => SetValue(AgreeProperty, value);
    }

    public static DependencyProperty AgreeProperty = DependencyProperty.Register(nameof(Agree), typeof(bool), typeof(AgreementDialog), new PropertyMetadata(false));

    public AgreementDialog()
    {
        InitializeComponent();
    }

    private static readonly ResourceLoader _resourceLoader = new(ResourceLoader.GetDefaultResourceFilePath());

    private void UserControl_Loaded(object sender, RoutedEventArgs e)
    {
        AgreementContent.Text = _resourceLoader.GetString("AgreementDetail");
    }
}
