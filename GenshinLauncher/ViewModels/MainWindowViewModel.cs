using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using GenshinLauncher.Models;
using Microsoft.Win32;
using ModernWpf.Controls;
using Stylet;

namespace GenshinLauncher.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public const string Title = "Genshin Impact Launcher";

        public MainWindowViewModel()
        {
            Quality    = new();
            Resolution = GetLastResolution();
        }

        public Command Client => Cli.Wrap(Location);

        public QualityViewModel Quality { get; }

        public Resolution Resolution { get; set; }

        public string Location
        {
            get => Config.Default.GenshinLocation;
            set => Config.Default.GenshinLocation = value;
        }

        private static string? InstallLocation => Registry.LocalMachine
            .OpenSubKey(@"SOFTWARE\launcher", false)
            ?.GetValue("InstPath") as string;

        public async Task<CommandResult> LaunchSelector() =>
            await Client.WithArguments("-show-screen-selector")
                .ExecuteAsync();

        protected override async void OnInitialActivate()
        {
            if (!TryGetLocation())
                await LocationMissing();
        }

        private async Task LocationMissing()
        {
            var dialog = new ContentDialog
            {
                Title   = "Error",
                Content = "Could not find Game's Location",

                PrimaryButtonText   = "Find Manually...",
                SecondaryButtonText = "Ignore",
                CloseButtonText     = "Exit"
            };

            var result = await dialog.ShowAsync();

            switch (result)
            {
                case ContentDialogResult.None:
                    RequestClose();
                    break;
                case ContentDialogResult.Primary:
                    await SetLocation();
                    break;
                case ContentDialogResult.Secondary:
                    break;
            }
        }

        public async Task SetLocation()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Executable|*.exe|All files (*.*)|*.*",
                InitialDirectory = InstallLocation is null
                    ? string.Empty
                    : Path.Combine(InstallLocation, "Genshin Impact Game")
            };

            var success = openFileDialog.ShowDialog() == true;
            var set = TrySetLocation(openFileDialog.FileName);

            if (!(success && set)) await LocationMissing();
        }

        private Resolution GetLastResolution()
        {
            var config = Registry.CurrentUser
                .OpenSubKey(@"SOFTWARE\miHoYo\Genshin Impact", false);

            var w = config.GetValue(@"Screenmanager Resolution Width_h182942802");
            var h = config.GetValue(@"Screenmanager Resolution Height_h2627697771");

            if (w is int width && h is int height)
                return Resolution.GetResolution(width, height);

            return Resolution.Presets.Last();
        }

        private bool TryGetLocation()
        {
            var locations = new[]
            {
                // User set location
                Location,

                // Default install location
                @"C:\Program Files\Genshin Impact\Genshin Impact Game\GenshinImpact.exe",

                // Custom install location
                Path.Combine(InstallLocation ?? string.Empty, @"Genshin Impact Game\GenshinImpact.exe"),

                // Relative location
                AppContext.BaseDirectory + "GenshinImpact.exe"
            };

            return locations.Any(TrySetLocation);
        }

        private bool TrySetLocation(string? location)
        {
            if (File.Exists(location) && location is not null)
            {
                Location = location;
                return true;
            }

            return false;
        }

        public async Task LaunchGame()
        {
            var client = Client
                .WithArguments(args =>
                {
                    args
                        .Add("-screen-width").Add(Resolution.Width)
                        .Add("-screen-height").Add(Resolution.Height)
                        .Add("-screen-fullscreen").Add(Config.Default.Fullscreen ? 1 : 0);

                    if (Config.Default.Borderless)
                        args.Add("-popupwindow");

                    if (Quality.SelectedQuality != Models.Quality.Default)
                        args.Add("-screen-quality").Add(Quality.SelectedQuality);
                });

            try
            {
                await client.ExecuteAsync();
            }
            catch (InvalidOperationException)
            {
                await LocationMissing();
            }
            catch (Win32Exception)
            {
                await LocationMissing();
            }
        }
    }
}