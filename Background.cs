using System.Collections.Generic;
using SplashKitSDK;
using System;

namespace Space_Shooter
{
    public static class Global
    {
        public const int Width = 600, Height = 800;
        public static readonly Font SmallFont = SplashKit.LoadFont("Consolas", "consolab.ttf");
        public static readonly  Font BigFont = SplashKit.LoadFont("Consolas", "consolab.ttf");
        public static readonly  Font MediumFont = SplashKit.LoadFont("SegouUI", "segoeuib.ttf");
    }
    public static class Background
    {
        private static bool _musicOn = true;
        private static SoundEffect _music = SplashKit.LoadSoundEffect("arcade", "arcade.mp3");
        private static Bitmap _background = SplashKit.LoadBitmap("space", "space.png");
        public static void DrawBackground()
        { 
            SplashKit.ClearScreen(Color.Black);
            SplashKit.DrawBitmap(_background, 0, 0);
        }
        public static void PlayMusic()
        {
            if (!SplashKit.SoundEffectPlaying("arcade") && _musicOn) 
                SplashKit.PlaySoundEffect("arcade", (float)0.1); 
        }
        public static void ToggleMusic()
        {
            _musicOn = !_musicOn;   
            SplashKit.StopSoundEffect("arcade");
        } 
    }
}