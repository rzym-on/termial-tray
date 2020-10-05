![Drag Racing](terminal-tray.ico)
# Termial Tray
Simple .NET app that hides app window (not minimize) and restores them using hotkeys. (using .NETFramework Version v4.7.2).

## Contents
- [Background of the story](#backgound)
- [Installation](#installation)
- [Features](#features)
- [FAQ](#faq)

## Background
I really like new Windows Terminal but before I was using ConEmu - It's really nice, easy to use yet powerfull. I really like Quake Style behavior in ConEmu - app *lives in tray* and app window with my terminals appear only when I press some ShortKey (in my case *LAlt + \`*). By hiding and showing terminals window I could switch between other windows using Alt+Tab, go to other [workspaces](https://docs.microsoft.com/en-us/windows-hardware/drivers/debugger/using-workspaces) using `Win+Ctrl+LArrow/RArrow` and call my terminals when and where I wanted. 

I fell in love with new Windows Terminal, but didn't find any solution to *"store app in tray"* - hide terminals window, and call it using ShortKeys. I found some applications, but they were too big for my needs. So I wrote this little app, to resolve my problem. And here we are. For now **Termial Tray** is everything that I wanted. Maybe in the future MS add this *Quake Style* feature to Windows Terminal, but for now this little tray app is enough for me. And I hope that'll be enough for you too.


## Installation

### Using Visual Studio

1. Download source code from this repo.
2. Open `WindowsTermialTray.sln` in Visual Studio.
3. Select `Release` from Solution Configuration (by default it's above the main section).
4. Right-Click on project `WindowsTerminalTray` in Solution Explorer.
5. Click `Build`.
6. Go to folder with solution and then to: `bin\Release`
7. Run the `WindowsTermialTray.exe`.
8. (optional) Add `WindowsTermialTray.exe` to your startup folder, to run app at system start.
9. Enjoy your hidden terminals!

### Using command line and [MSBuild.exe](https://docs.microsoft.com/en-us/visualstudio/msbuild/msbuild?view=vs-2019)

1. Download source code from this repo.
2. Install and locate the MSBuild.exe on your computer. In my case it's in:
```
C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin
```
3. Add this path to your [Windows PATH](https://stackoverflow.com/a/41895179/10451282).
4. Go to folder with source code from this repo, with folder and run this command:
```
MSBuild.exe .\termial-tray\WindowsTerminalTray.csproj /property:Configuration=Release
```
5. Go to: `termial-tray/bin/Release`.
6. Run the `WindowsTermialTray.exe` app to start.
7. (optional) Add `WindowsTermialTray.exe` to your startup folder, to run app at system start:
```
C:\Users\your_user\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup
```
8. Enjoy your hidden terminals!

## Features

### **Access from multiple workspaces**
You can show your app on all of your workspaces, do something, hide it, go to oher workspace and pull the same app from system tray.

![workspaces](https://media.giphy.com/media/I9W8qqvDnjX8uayrWD/giphy.gif)

### **Run an app if it's not started**
When Termial Tray starts, it's not starting apps, that you defined in code - it's waiting for Hot Keys. After HotKeys is pressed, Termial Tray look if a process is already running and attaches it to HotKeys (One process, one window, one hotkey). If app is not running - it start an app for you and attach this process to HotKeys. If you close your app, and press HotKeys again, Termial Tray will start new app, and it'll attach to it.

![start app](https://media.giphy.com/media/mBOhbiuIyQZAnoh4tM/giphy.gif)

### **Apps run in background**
When you run something in app, that is attached to Termial Tray - it'll continue its work when it's hidden.

![background](https://media.giphy.com/media/t7UGFo9wxjAK0Rw1uq/giphy.gif)

### **Works with not-attached apps**
If you had multiple instances of an app, Termial Tray will store only one, so fe. you could have multiple windows with terminals, and one **floating** terminal that you can pull anywhere.

![multiple](https://media.giphy.com/media/GBQeQGHbiHZLsPEXyp/giphy.gif)

## FAQ

### **Can I manage other apps like Windows Terminal with Quake Style?**
Yes of course! But you need to dig into code. Just download this repo, go to `TerminalTracyIcon.cs` file and find this section:
```C#
_appTrayList.AddRange(new []
            {
                new TrayApp("WindowsTerminal", "wt.exe", ModifierKeys.Alt, Keys.Oemtilde)
                // Add your other tray apps here
                // new TrayApp("ProcessName", "exec.exe", ModifierKeys.Ctrl, Keys.Oemtilde)
            });
```
From here you can add other apps, but you need to specify and know:

* Your app [ProcessName](https://docs.microsoft.com/en-us/powershell/module/microsoft.powershell.management/get-process?view=powershell-7).
* Your app executable.
* Unique pair of `ModifierKey` and `Key`.

When you find out everything - add your app in code with parameters (and comment Windows Terminal if don't want it), and follow steps in [installation section](#installation), to build `.exe` file.

### **Why building and adding other apps is so difficult?**
Because I didn't prepared this. For now this little app is for me, but if you like it too - tell me, and I'll continue to contribuiting it!

### **Will you be contributing this Termial Tray?**
Nah. I hope that MS add *Quake Style* behaviour for Windows Terminal, but if not, or some people find Termial Tray useful for other Jobs - I'll start to work on this app, to make it more powerfull and usefull.

### **Your code is a mess, shame on you**
Thanks for your opinion! I'm not an expert in .NET and this kind of system apps - I created this to fit my personal needs and added it here for people (if they want to benefit from this solution). But if you're expert, please share your opinion about this project with me - I'll fix and update code to be clean and fast.