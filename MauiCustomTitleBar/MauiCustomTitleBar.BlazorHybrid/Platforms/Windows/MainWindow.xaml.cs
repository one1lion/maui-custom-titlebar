using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.ComponentModel;
using Windows.Graphics;
using AppWindow = Microsoft.UI.Windowing.AppWindow;
using Border = Microsoft.UI.Xaml.Controls.Border;
using Color = Windows.UI.Color;
using Colors = Microsoft.UI.Colors;
using Grid = Microsoft.UI.Xaml.Controls.Grid;
using Rect = Windows.Foundation.Rect;
using Size = Windows.Foundation.Size;
using SolidColorBrush = Microsoft.UI.Xaml.Media.SolidColorBrush;
using Thickness = Microsoft.UI.Xaml.Thickness;
using Visibility = Microsoft.UI.Xaml.Visibility;
using Win32Interop = Microsoft.UI.Win32Interop;
using Window = Microsoft.UI.Xaml.Window;

namespace MauiCustomTitleBar.BlazorHybrid.WinUI;

public partial class MainWindow : Window
{
    private IntPtr _hWnd = IntPtr.Zero;
    private AppWindow _appW = null;

    private TranslateTransform _translateTransform1 = null;
    private TransformGroup _trfg1 = null;
    private TextBlock _text1 = null;
    private Border _border1 = null;
    private double _nCurrentTranslateTransformX = 0;
    private double _nTextX = 0;

    public MainWindow()
    {
        InitializeComponent();

        _hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var myWndId = Win32Interop.GetWindowIdFromWindow(_hWnd);
        _appW = AppWindow.GetFromWindowId(myWndId);

        _appW.Resize(new SizeInt32(600, 250));
        _appW.Move(new PointInt32(600, 400));

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);

        CompositionTarget.Rendering += CompositionTarget_Rendering;
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private float _scrollSpeed = 1.0f;

    private float ScrollSpeed
    {
        get => _scrollSpeed;
        set
        {
            _scrollSpeed = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScrollSpeed)));
        }
    }
    public double SetSpeed(float? x) => _scrollSpeed;
    public float? GetSpeed(double x) => ScrollSpeed = (float)x;

    private void CompositionTarget_Rendering(object sender, object e)
    {
        if (bRender)
            Render();
    }

    private void myButton_Click(object sender, RoutedEventArgs e)
    {
        ShowScrollingText(!bRender);
    }

    private bool bRender = false;
    private void Render()
    {
        if (_text1 != null)
        {
            if (_translateTransform1 == null)
            {
                _translateTransform1 = new TranslateTransform();
                _trfg1 = new TransformGroup();
                _trfg1.Children.Add(_translateTransform1);
                _text1.RenderTransform = _trfg1;
            }

            _translateTransform1.X = _nTextX;
            _nTextX -= _scrollSpeed;
            if (_nTextX <= -_text1.ActualWidth)
            {
                _nTextX = _nCurrentTranslateTransformX;
            }
        }
    }

    private void ShowScrollingText(bool bShow)
    {
        if (bShow)
        {
            if (_text1 == null && _border1 == null)
            {
                _text1 = new TextBlock()
                {
                    Text = "This is a scrolling text",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 20,
                    TextWrapping = TextWrapping.NoWrap,
                    TextTrimming = TextTrimming.None,
                    Foreground = new SolidColorBrush(Colors.Lime)
                };
                _text1.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _text1.Margin = new Thickness(0, 0, -_text1.ActualWidth, 0);

                _border1 = new Border()
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)),
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(190, 1, 98, 1),
                    Child = _text1,
                    Background = new SolidColorBrush(Colors.Black)
                };

                _border1.SizeChanged += Border1_SizeChanged;
                _border1.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _nTextX = _border1.DesiredSize.Width;

                _border1.SetValue(Grid.RowProperty, 1);
                _border1.SetValue(Grid.ColumnProperty, 1);
                AppTitleBar.Children.Add(_border1);
            }
            _text1.Visibility = Visibility.Visible;
            _border1.Visibility = Visibility.Visible;
            sliderSpeed.Visibility = Visibility.Visible;
            myButton.Content = "Hide Scrolling text";
            bRender = true;
        }
        else
        {
            _text1.Visibility = Visibility.Collapsed;
            _border1.Visibility = Visibility.Collapsed;
            sliderSpeed.Visibility = Visibility.Collapsed;
            myButton.Content = "Show Scrolling text";
            bRender = false;
        }
    }

    private void Border1_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        _nCurrentTranslateTransformX = e.NewSize.Width;
        var rg = new RectangleGeometry
        {
            Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height)
        };
        ((UIElement)sender).Clip = rg;
    }
}