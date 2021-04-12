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
            _movePattern = new VerticalMovement(4, X, Y);
            XOffset = 45;
            YOffset = 45;
            Damage = 4;
            ExplosionType = Explosion.Type.Fire;
            Bitmap = (SplashKit.Rnd(0, 2) == 0) ? SplashKit.LoadBitmap("asteroid1", "Asteroids/grayAsteroid.png") : SplashKit.LoadBitmap("asteroid2", "Asteroids/brownAsteroid.png");
        }
        public Asteroid() : this(Global.Width, Global.Height){}
        public void SetCoordinates(int lastAsteroidX, int lastAsteroidY)
        {
            if (lastAsteroidY >= 90)
            {
                X = (2 * SplashKit.Rnd(0, 6) + 1) * 50; 
                Y = -45;
            } else
            {
                X = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
                Y = lastAsteroidY - 90;
            }
        }

        private void Move() => Y += 4; 
        public override void Update()
        {
            _movePattern.Perform();
            Y = (int)_movePattern.UpdatedY;
        } 
    }
}