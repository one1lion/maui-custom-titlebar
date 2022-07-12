# Information

This repository is my playground for exploring having a customizable TitleBar in a MAUI application (both regular and Blazor Hybrid) that works when targeting Windows platform.  I have come to understand (from [this article](https://docs.microsoft.com/en-us/windows/apps/develop/title-bar?tabs=winui3)) that the TitleBar Customization API only works with Windows 11.  After [reaching out for help](https://docs.microsoft.com/en-us/answers/questions/914870/blazor-hybrid-maui-application-window-customizatio.html) I received some feedback that proved difficult to understand and apply, so I [reached out again](https://docs.microsoft.com/en-us/answers/questions/918672/blazor-hybrid-maui-customize-titlebar.html?childToView=923053#comment-923053) and got a little bit more guidance.  This guidance suggested I use the sample from the first question to add a MainWindow.xaml to Platforms -> Windows and update the Platforms -> Windows -> App.xaml file to match.  Unfortunately, there are no Window .xaml file templates that I could find in Add -> New File, so I just chose `Content Page` and changed it to `Window` in the .xaml and code behind.

Regardless, using that, I applied the WinUI3 sample from [this repo](https://github.com/castorix/WinUI3_CustomCaption) by castorix to both a regular MAUI application and the blazor hybrid.  This required that I do a lot of aliasing in the [MainWindow.xaml.cs](./MauiCustomTitleBar/MauiCustomTitleBar.RegularMaui/Platforms/Windows/MainWindow.xaml.cs) file.

In short, the process I used to get the custom TitleBar to display using the sample project was: 
1. Create a new MAUI (either `.NET MAUI App` or `.NET MAUI Blazor App`)

2. Add the `MainWindow.xaml` (and `MainWindow.xaml.cs`) files to the Plastforms -> Windows folder.

**NOTE**: Since there was no template for XAML Window, I chose Content Page.  When the file was created, I changed `ContentPage` in the .xaml and .xaml.cs to `Window`, making sure to alias `using Window = Microsoft.UI.Xaml.Window;`

3. Re-namespaced (for convenience) the class to `[project].WinUI`. 

**NOTE**: in the .xaml file, this meant changing `x:Class=[project].Platforms.Windows.MainWindow` to `x:Class=[project].WinUI.MainWindow`; as well as the namespace in the .xaml.cs file to `namespace [project].WinUI;`

4. Copy over the code from the sample project's `MainWindow.xaml` and `MainWindow.xaml.cs`, taking care not to override the namespace

5. Add alias using statements to make the file use the Microsoft.UI.Xaml classes (or fully quallify the declarations); 

**NOTE**: I also did some variable re-naming to match my preferred naming conventions and declarations, as well as applying refactoring suggestions

6. Copy over the code from the sample project's Platforms -> Windows -> `App.xaml` and `App.xaml.cs`, taking care not to override the namespace, aliasing Window, and renaming the variable to match my preferred naming conventions

I was able to run both project types and see the custom title bar.

# Next Steps

Add the BlazorWebView component to the `MainWindow`.  It looks like this process replaces the application's App.xaml such that the app does not use `MainPage.xaml`.  I still need to understand it more, but I will work on this next.