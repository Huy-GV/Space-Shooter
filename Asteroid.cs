using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class Asteroid : Enemy
    {
        public Asteroid(int lastAsteroidX, int lastAsteroidY) : base()
        {
            SetCoordinates(lastAsteroidX, lastAsteroidY);
            SetAnimation();
            _movePattern = new StraightLinePattern(4, X, Y, 90);
            ExplosionType = Explosion.Type.Fire;
        }
        private void SetAnimation()
        {
            Bitmap bitmap;
            if (SplashKit.Rnd(0, 2) == 0) 
                bitmap = SplashKit.LoadBitmap("asteroid1", "Asteroids/grayAsteroid.png");
            else
                bitmap = SplashKit.LoadBitmap("asteroid2", "Asteroids/brownAsteroid.png");

            _image = new StaticImage(bitmap);
        }
        public Asteroid() : this(Global.Width, Global.Height){}
        public void SetCoordinates(int lastAsteroidX, int lastAsteroidY)
        {
            X = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
            Y = (lastAsteroidY > 50) ? -10 : lastAsteroidY - 60;
        }
        public override void Update()
        {
            Move();
        }
        public override void Draw()
        {
            _image.Draw(X, Y);
        }

        
    }
}