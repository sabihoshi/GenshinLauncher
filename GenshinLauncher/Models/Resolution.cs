using System.Collections.Generic;

namespace GenshinLauncher.Models
{
    public class Resolution
    {
        public static List<Resolution> Presets = new()
        {
            new(640, 480),
            new(720, 400),
            new(720, 480),
            new(800, 600),
            new(1024, 768),
            new(1152, 864),
            new(1280, 720),
            new(1280, 800),
            new(1280, 960),
            new(1280, 1024),
            new(1336, 768),
            new(1600, 900),
            new(1680, 1050),
            new(1920, 1080)
        };

        public Resolution(int width, int height)
        {
            Width  = width;
            Height = height;
        }

        public int Height { get; set; }

        public int Width { get; set; }

        public override string ToString() => $"{Width}x{Height}";
    }
}