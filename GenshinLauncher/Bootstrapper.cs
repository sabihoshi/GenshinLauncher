using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using GenshinLauncher.ViewModels;
using Stylet;

namespace GenshinLauncher
{
    public class Bootstrapper : Bootstrapper<MainWindowViewModel>
    {
        protected override void Configure()
        {
            // Make Hyperlinks handle themselves
            EventManager.RegisterClassHandler(
                typeof(Hyperlink), Hyperlink.RequestNavigateEvent,
                new RequestNavigateEventHandler((_, e) =>
                {
                    var url = e.Uri.ToString();
                    Process.Start(new ProcessStartInfo(url)
                    {
                        UseShellExecute = true
                    });
                })
            );

            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Shift))
            {
            }
        }
    }
}