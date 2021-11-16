using System.Collections.Generic;
using SplashKitSDK;
using System;

namespace Drawable
{
    public static class Background
    {
        private static bool _musicOn = true;
        private static readonly SoundEffect _music = SplashKit.LoadSoundEffect("arcade", "arcade.mp3");
        private static readonly Bitmap _background = SplashKit.LoadBitmap("space", "Background/space.png");
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