using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MauiCustomTitleBar.Shared.Helpers;

public abstract class BaseNotifyPropertyChanges : INotifyPropertyChanging, INotifyPropertyChanged, IDisposable
{
    public event PropertyChangingEventHandler? PropertyChanging;
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanging([CallerMemberName] string? name = default)
    {
        RaisePropertyChanging(this, new PropertyChangingEventArgs(name));
    }

    protected void RaisePropertyChanging(object? sender, PropertyChangingEventArgs e)
    {
        PropertyChanging?.Invoke(sender, e);
    }

    protected void RaisePropertyChanged([CallerMemberName] string? name = default)
    {
        RaisePropertyChanged(this, new PropertyChangedEventArgs(name));
    }

    protected void RaisePropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(sender, e);
    }

    protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string? name = default)
    {
        if (storage?.Equals(value) ?? false) { return; }
        RaisePropertyChanging(name);
        storage = value;
        RaisePropertyChanged(name);
    }

    private void UnsubAll()
    {
        PropertyChanged = null;
        PropertyChanging = null;
    }

    public void Dispose()
    {
        UnsubAll();
        AdditionalDispose();
    }

    /// <summary>
    /// Perform additional dispose by overriding this in the inheriting class
    /// </summary>
    protected virtual void AdditionalDispose() { }
}
