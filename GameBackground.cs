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
    public static class GameBackground
    {
        private static SoundEffect _music = SplashKit.LoadSoundEffect("arcade", "arcade.mp3");
        private static Bitmap _background = SplashKit.LoadBitmap("space", "space.png");
        private static List<Explosion> _explosions = new List<Explosion>();
        // public static double Score = 0;
        // public static void GainScore(){ Score += 1/(double)60;}
        public static void DrawBackground()
        { 
            SplashKit.ClearScreen(Color.Black);
            SplashKit.DrawBitmap(_background, 0, 0);
        }
        public static void UpdateExplosions()
        {
            foreach (var explosion in _explosions.ToArray())
            {
                explosion.Update();
                if (explosion.AnimationEnded()) _explosions.Remove(explosion);
            }
        }
        public static void DrawExplosions()
        {
            foreach (var explosion in _explosions.ToArray()) explosion.Draw();
        }
        public static void PlayMusic(){if (!SplashKit.SoundEffectPlaying("arcade")) SplashKit.PlaySoundEffect("arcade", (float)0.1); }
        public static void CreateExplosion(int x, int y, Explosion.Type type) => _explosions.Add(new Explosion(x, y, type));
    }
}