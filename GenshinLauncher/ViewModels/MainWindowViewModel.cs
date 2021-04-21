using System.Linq;
using System.Threading.Tasks;
using CliWrap;
using Stylet;

namespace GenshinLauncher.ViewModels
{
    public class MainWindowViewModel : Screen
    {
        public MainWindowViewModel()
        {
            Quality    = new();
            Resolution = Resolution.Presets.Last();
        }

        public bool Fullscreen { get; set; } = true;

        public bool Borderless { get; set; }

        public Command Client => Cli.Wrap(GenshinLocation);

        public QualityViewModel Quality { get; }

        public Resolution Resolution { get; set; }

        public string Title { get; } = "Genshin Impact Launcher";

        public string GenshinLocation { get; set; } =
            @"C:\Program Files\Genshin Impact\Genshin Impact Game\GenshinImpact.exe";

        public async Task<CommandResult> LaunchSelector() =>
            await Client.WithArguments("-show-screen-selector")
               .ExecuteAsync();

        public void LaunchGame()
        {
            var client = Client
               .WithArguments(args =>
                {
                    args
                       .Add("-screen-width").Add(Resolution.Width)
                       .Add("-screen-height").Add(Resolution.Height)
                       .Add("-screen-fullscreen").Add(Fullscreen ? 1 : 0);

                    if (Borderless)
                        args.Add("-popupwindow");

                    if (Quality.SelectedQuality != Models.Quality.Default)
                        args.Add("-screen-quality").Add(Quality.SelectedQuality);
                });

            _ = client.ExecuteAsync();
        }
    }
}