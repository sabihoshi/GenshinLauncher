# Genshin Impact Launcher [![GitHub release (latest by date)](https://img.shields.io/github/v/release/sabihoshi/GenshinLauncher)](https://github.com/sabihoshi/GenshinLauncher/releases/latest)

A Genshin Impact launcher  made using C# and WPF using Windows Fluent design. If you enjoyed this project, consider [contributing](https://github.com/sabihoshi/GenshinLauncher#contributing) or 🌟 starring the repository. Thank you~

## **[Download latest version ![GitHub all releases](https://img.shields.io/github/downloads/sabihoshi/GenshinLauncher/total?style=social)](https://github.com/sabihoshi/GenshinLauncher/releases/latest)**

![GenshinLauncher_2021-04-21_19-38-11](https://user-images.githubusercontent.com/25006819/115547689-20649e00-a2d9-11eb-9e36-decd13993717.png)

## How to use

1. [Download the program](https://github.com/sabihoshi/GenshinLauncher/releases/latest) and then run, no need for installation.


## Features
* Change the set resolution of the game.
* Set the quality preset of the game.
* Allow for borderless fullscreen gameplay.

### Controller Settings
This launches the unity screen selector for more options such as changing the joystick bindings as well as some other redundant options.

## Upcoming
* Change the location of the .exe to a custom one.
* Create a shortcut to auto-apply settings.

## About

### Can this get me banned?
No it will not. This only uses [Unity command line arguments](https://docs.unity3d.com/Manual/CommandLineArguments.html) that are built in itself. It essentially just runs the game with extra flags such as `-popupwindow`. [Here is miHoYo's response](https://genshin.mihoyo.com/en/news/detail/5763) to using 3rd party tools.

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email (sabihoshi.dev@gmail.com), or any other method with me or the maintainers of this repository before making a change.

This project has a [Code of Conduct](CONTRIBUTING.md), please follow it in all your interactions with the project.

## Pull Request Process

1. Do not include the build itself where the project is cleaned using `dotnet clean`.
2. Update the README.md with details of changes to the project, new features, and others that are applicable.
3. Increase the version number of the project and the README.md to the new version that this
   Pull Request would represent. The versioning scheme we use is [SemVer](http://semver.org/).
4. You may merge the Pull Request in once you have the the approval of the maintainers.

## Build
If you just want to run the program, there are precompiled releases that can be found in [here](https://github.com/sabihoshi/GenshinLauncher/releases).
### Requirements
* [Git](https://git-scm.com) for cloning the project
* [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1) SDK

#### Publish a single binary for Windows
```bat
git clone https://github.com/sabihoshi/GenshinLauncher.git
cd GenshinLauncher\GenshinLauncher

dotnet publish -r win-x86 --framework netcoreapp3.1 -o bin\publish --no-self-contained -p:PublishSingleFile=true
```
> For other runtimes, visit the [RID Catalog](https://docs.microsoft.com/en-us/dotnet/core/rid-catalog) and change the runtime value.

#### Build the project (not necessary if you published)
```bat
git clone https://github.com/sabihoshi/GenshinLauncher.git
cd GenshinLauncher

dotnet build
```

#### Publish the project using defaults
```bat
git clone https://github.com/sabihoshi/GenshinLauncher.git
cd GenshinLauncher

dotnet publish
```

# License
* This project is under the [MIT](LICENSE.md) license.
* All rights reserved by © miHoYo Co., Ltd. This project is not affiliated nor endorsed by miHoYo. Genshin Impact™ and other properties belong to their respective owners.
* This project uses third-party libraries or other resources that may be
distributed under [different licenses](/THIRD-PARTY-NOTICES.md).