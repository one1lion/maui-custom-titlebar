using MauiCustomTitleBar.Shared.State;

namespace MauiCustomTitleBar.Attempt;

public partial class App : Application
{
    private readonly ColorState _colorState;
    
    public App(ColorState colorState)
    {
        InitializeComponent();
        _colorState = colorState;

        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        var window = base.CreateWindow(activationState);
        window.Title = "Customized Title";

        return window;
    }
}