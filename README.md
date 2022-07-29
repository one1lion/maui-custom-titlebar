# Information

This repository is my playground for exploring having a customizable TitleBar in a MAUI application (both regular and Blazor Hybrid) that works when targeting Windows platform.  I have come to understand (from [this article](https://docs.microsoft.com/en-us/windows/apps/develop/title-bar?tabs=winui3)) that the TitleBar Customization API only works with Windows 11.  After [reaching out for help](https://docs.microsoft.com/en-us/answers/questions/914870/blazor-hybrid-maui-application-window-customizatio.html) I received some feedback that proved difficult to understand and apply, so I [reached out again](https://docs.microsoft.com/en-us/answers/questions/918672/blazor-hybrid-maui-customize-titlebar.html?childToView=923053#comment-923053) and got a little bit more guidance.  This guidance suggested I use the sample from the first question to add a MainWindow.xaml to Platforms -> Windows and update the Platforms -> Windows -> App.xaml file to match.  

I gutted this repo and setup the projects in a way that would allow me to explore different approaches (as different branches).  You can still see my previous attempts in the commit history, but from here forward, the branches will be separate attempts.