using System;
using System.Collections.Generic;
using System.Linq;

namespace GenshinLauncher.Models
{
    public class Resolution : IEquatable<Resolution>
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

        public bool Equals(Resolution? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Height == other.Height && Width == other.Width;
        }

        public static Resolution GetResolution(int width, int height)
        {
            var other = new Resolution(width, height);
            var resolution = Presets.FirstOrDefault(r => r.Equals(other));

            if (resolution is null)
            {
                Presets.Add(other);
                return other;
            }

            return resolution;
        }

        public override string ToString() => $"{Width}x{Height}";

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Resolution) obj);
        }

        public override int GetHashCode() => HashCode.Combine(Height, Width);
    }
}