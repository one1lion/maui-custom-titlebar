using Microsoft.UI;
using WinRT.Interop;

#if WINDOWS
using Microsoft.UI.Windowing;
#endif

namespace MauiCustomTitleBar.CustomizeAttempt;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        window.Title = "Customized Title";

        return window;
    }
}