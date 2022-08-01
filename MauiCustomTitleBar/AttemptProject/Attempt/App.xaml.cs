using MauiCustomTitleBar.Shared.State;
using System.Diagnostics;

#if WINDOWS
using ColorTranslator = System.Drawing.ColorTranslator;
#endif

namespace MauiCustomTitleBar.Attempt;

public partial class App : Application
{
#if WINDOWS
    private readonly ColorState _colorState;

    public App(ColorState colorState)
    {
        InitializeComponent();

        MainPage = new MainPage();

        colorState.PropertyChanged += HandleColorChanged;
        _colorState = colorState;
    }
#else
    public App ()
	{
        InitializeComponent();

        MainPage = new MainPage();
	}
#endif

#if WINDOWS
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
#endif
}