using SplashKitSDK;

namespace Main
{
    public static class Global
    {
        public const int Width = 600, Height = 800;
        public static bool InWindow(int x, int y) => x > 0 && x < Width && y < 0 && y > Height;
        public static readonly Font SmallFont = SplashKit.LoadFont("Consolas", "consolab.ttf");
        public static readonly  Font BigFont = SplashKit.LoadFont("Consolas", "consolab.ttf");
        public static readonly  Font MediumFont = SplashKit.LoadFont("SegouUI", "segoeuib.ttf");
    }
}