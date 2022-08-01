using MauiCustomTitleBar.Shared.State;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml;
using System.Diagnostics;
using Color = Windows.UI.Color;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiCustomTitleBar.Attempt.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    private ColorState _colorState = default!;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();

        WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            var serviceProvider = handler.MauiContext.Services;
            _colorState = serviceProvider.GetService<ColorState>();

            _colorState.PropertyChanged += HandleColorStatePropertyChanged;
        });
    }

    private void HandleColorStatePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ColorState.Color))
        {
            var resources = Current.Resources;
            if (!resources.ContainsKey("TitleBarBackgroundColor"))
            {
                Debug.WriteLine("Windows Platform-specific: Could not find the resource 'TitleBarBackgroundColor' in the dictionary.");
                return;
            }

            resources["TitleBarBackgroundColor"] = Color.FromArgb(_colorState.Color.A, _colorState.Color.R, _colorState.Color.G, _colorState.Color.B);
            Debug.WriteLine($"Windows Platform-specific: Successfully updated the resource 'TitleBarBackgroundColor' in the dictionary to {resources["TitleBarBackgroundColor"]}.");
        }
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}