using System.ComponentModel;

namespace GenshinLauncher
{
    internal sealed partial class Config
    {
        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e) => Save();
    }
}