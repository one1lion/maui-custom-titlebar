using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.ComponentModel;
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

namespace MauiCustomTitleBar.Attempt.WinUI;

public partial class MainWindow : Window
{
    #region Private Fields and Properties
    private TranslateTransform _scrollTransform = null;
    private TransformGroup _transformGroup = null;
    private TextBlock _scrollingTextBlock = null;
    private Border _scrollBlockBorder = null;
    private double _curScrollTransform = 0;
    private double _curTextX = 0;

    private float _scrollSpeed = 1.0f;
    private bool _performRender;

    private float ScrollSpeed
    {
        get => _scrollSpeed;
        set
        {
            _scrollSpeed = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ScrollSpeed)));
        }
    }
    #endregion Private Fields and Properties

    #region Public Fields and Properties
    public event PropertyChangedEventHandler PropertyChanged;

    public double SetSpeed(float? x) => _scrollSpeed;
    public float? GetSpeed(double x) => ScrollSpeed = (float)x;

    private void Render()
    {
        if (_scrollingTextBlock != null)
        {
            if (_scrollTransform == null)
            {
                _scrollTransform = new TranslateTransform();
                _transformGroup = new TransformGroup();
                _transformGroup.Children.Add(_scrollTransform);
                _scrollingTextBlock.RenderTransform = _transformGroup;
            }

            _scrollTransform.X = _curTextX;
            _curTextX -= _scrollSpeed;
            if (_curTextX <= -_scrollingTextBlock.ActualWidth)
            {
                _curTextX = _curScrollTransform;
            }
        }
    }
    #endregion

    #region Constructor
    public MainWindow()
    {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);

        CompositionTarget.Rendering += HandleCompositionTargetRendering;
    }
    #endregion

    private void HandleCompositionTargetRendering(object sender, object e)
    {
        if (_performRender)
        {
            Render();
        }
    }

    private void ToggleScrollingTextVisible(object sender, RoutedEventArgs e)
    {
        ShowScrollingText(!_performRender);
    }

    private void ShowScrollingText(bool show)
    {
        if (show)
        {
            if (_scrollingTextBlock == null && _scrollBlockBorder == null)
            {
                _scrollingTextBlock = new TextBlock()
                {
                    Text = "This is a scrolling text",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 20,
                    TextWrapping = TextWrapping.NoWrap,
                    TextTrimming = TextTrimming.None,
                    Foreground = new SolidColorBrush(Colors.Lime)
                };
                _scrollingTextBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _scrollingTextBlock.Margin = new Thickness(0, 0, -_scrollingTextBlock.ActualWidth, 0);

                _scrollBlockBorder = new Border()
                {
                    BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0)),
                    BorderThickness = new Thickness(2),
                    Margin = new Thickness(190, 1, 98, 1),
                    Child = _scrollingTextBlock,
                    Background = new SolidColorBrush(Colors.Black)
                };

                _scrollBlockBorder.SizeChanged += HandleBorderSizeChanged;
                _scrollBlockBorder.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                _curTextX = _scrollBlockBorder.DesiredSize.Width;

                _scrollBlockBorder.SetValue(Grid.RowProperty, 1);
                _scrollBlockBorder.SetValue(Grid.ColumnProperty, 1);
                AppTitleBar.Children.Add(_scrollBlockBorder);
            }
            _scrollingTextBlock.Visibility = Visibility.Visible;
            _scrollBlockBorder.Visibility = Visibility.Visible;
            SpeedSlider.Visibility = Visibility.Visible;
            ScrollTextButton.Content = "Hide Scrolling text";
            _performRender = true;
        }
        else
        {
            _scrollingTextBlock.Visibility = Visibility.Collapsed;
            _scrollBlockBorder.Visibility = Visibility.Collapsed;
            SpeedSlider.Visibility = Visibility.Collapsed;
            ScrollTextButton.Content = "Show Scrolling text";
            _performRender = false;
        }
    }

    private void HandleBorderSizeChanged(object sender, SizeChangedEventArgs e)
    {
        _curScrollTransform = e.NewSize.Width;
        var rg = new RectangleGeometry
        {
            Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height)
        };
        ((UIElement)sender).Clip = rg;
    }
}