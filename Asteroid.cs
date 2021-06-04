using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Asteroid : Enemy
    {
        public Asteroid(Position position, ICanMove movePattern) : base(position, movePattern)
        {
            Bitmap bitmap;
            if (SplashKit.Rnd(0, 2) == 0) 
                bitmap = SplashKit.LoadBitmap("asteroid1", "Asteroids/grayAsteroid.png");
            else
                bitmap = SplashKit.LoadBitmap("asteroid2", "Asteroids/brownAsteroid.png");
            Image = new StaticImage(bitmap);
        }
    }
}