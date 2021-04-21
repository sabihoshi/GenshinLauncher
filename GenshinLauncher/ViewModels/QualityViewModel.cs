using System;
using System.Collections.Generic;
using System.Linq;
using GenshinLauncher.Models;
using Stylet;

namespace GenshinLauncher.ViewModels
{
    public class QualityViewModel : Screen
    {
        public IEnumerable<Quality> Qualities { get; } = Enum.GetValues(typeof(Quality)).Cast<Quality>();

        public Quality SelectedQuality { get; set; } = Quality.Default;
    }
}