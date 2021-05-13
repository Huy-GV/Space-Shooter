using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Asteroid : Enemy
    {
        public Asteroid(int lastAsteroidX, int lastAsteroidY) : base()
        {
            // SetCoordinates(lastAsteroidX, lastAsteroidY);
            Image = SetAnimation();
            var x = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
            var y = (lastAsteroidY > 50) ? -10 : lastAsteroidY - 60;

            _movePattern = new StraightLinePattern(4, x, y, 90);
            ExplosionType = Explosion.Type.Fire;
        }
        private Image SetAnimation()
        {
            Bitmap bitmap;
            if (SplashKit.Rnd(0, 2) == 0) 
                bitmap = SplashKit.LoadBitmap("asteroid1", "Asteroids/grayAsteroid.png");
            else
                bitmap = SplashKit.LoadBitmap("asteroid2", "Asteroids/brownAsteroid.png");
            return new StaticImage(bitmap);
        }
        public Asteroid() : this(Global.Width, Global.Height){}
        public override void Draw()
        {
            Image.Draw(_movePattern.X, _movePattern.Y);
        } 
    }
}