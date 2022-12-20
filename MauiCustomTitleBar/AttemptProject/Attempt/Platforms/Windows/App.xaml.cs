using MauiCustomTitleBar.Shared.State;
using Microsoft.Maui.Handlers;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using System.Diagnostics;
using WinRT.Interop;
using ColorTranslator = System.Drawing.ColorTranslator;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MauiCustomTitleBar.Attempt.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    private ColorState _colorState;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();

        WindowHandler.Mapper.AppendToMapping(nameof(IWindow), (handler, view) =>
        {
            _colorState = handler.MauiContext.Services.GetRequiredService<ColorState>();
            _colorState.PropertyChanged += HandleColorChanged;

            var nativeWindow = handler.PlatformView;
            nativeWindow.Activate();

            var hWnd = WindowNative.GetWindowHandle(nativeWindow);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);

            if (appWindow.TitleBar is not null)
            {
                var color = Windows.UI.Color.FromArgb(255, 222, 111, 33);
                appWindow.TitleBar.BackgroundColor = color;
                appWindow.TitleBar.ButtonBackgroundColor = color;
                appWindow.TitleBar.ForegroundColor = Windows.UI.Color.FromArgb(255, 178, 255, 178);
                color = Windows.UI.Color.FromArgb(255, 82, 41, 12);
                appWindow.TitleBar.InactiveBackgroundColor = color;
                appWindow.TitleBar.ButtonInactiveBackgroundColor = color;
                appWindow.TitleBar.InactiveForegroundColor = Windows.UI.Color.FromArgb(255, 178, 178, 178);
                // HACK: Causes the Icon to re-render so it takes the new bg color right away
                appWindow.TitleBar.IconShowOptions = IconShowOptions.ShowIconAndSystemMenu;
            }
        });
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    private void HandleColorChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ColorState.Color))
        {
            var resources = Current.Resources;
            if (!resources.ContainsKey("TitleBarBackgroundColor"))
            {
                Debug.WriteLine("Main App: Could not find the resource 'TitleBarBackgroundColor' in the dictionary.");
                return;
            }

            resources["TitleBarBackgroundColor"] = Color.Parse(ColorTranslator.ToHtml(_colorState.Color));
            Debug.WriteLine($"Main App: Successfully updated the resource 'TitleBarBackgroundColor' in the dictionary to {resources["TitleBarBackgroundColor"]}.");
        }
    }
}