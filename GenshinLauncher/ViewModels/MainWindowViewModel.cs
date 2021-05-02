using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CliWrap;
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
            Resolution = Resolution.Presets.Last();
        }

        public Command Client => Cli.Wrap(GenshinLocation);

        public QualityViewModel Quality { get; }

        public Resolution Resolution { get; set; }

        public string GenshinLocation { get; set; }

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

        private bool TryGetLocation()
        {
            var location = @"C:\Program Files\Genshin Impact\Genshin Impact Game\GenshinImpact.exe";

            if (TrySetLocation(location))
                return true;

            location = InstallLocation + @"\Genshin Impact Game\GenshinImpact.exe";

            if (TrySetLocation(location))
                return true;

            location = AppContext.BaseDirectory + "GenshinImpact.exe";

            return TrySetLocation(location);
        }

        private bool TrySetLocation(string location)
        {
            if (File.Exists(location))
            {
                GenshinLocation = location;
                return true;
            }

            return false;
        }

        public void LaunchGame()
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

            _ = client.ExecuteAsync();
        }
    }
}