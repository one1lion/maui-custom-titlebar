using MauiCustomTitleBar.Shared.Helpers;
using System.Drawing;

namespace MauiCustomTitleBar.Shared.State;

public class ColorState : BaseNotifyPropertyChanges
{
    private Color _color = Color.Black;

    public Color Color { get => _color; set => SetProperty(ref _color, value); }
}
