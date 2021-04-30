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
            _movePattern = new VerticalMovement(4, X, Y);
            XOffset = 50;
            YOffset = 50;
            ExplosionType = Explosion.Type.Fire;
        }
        private void SetAnimation()
        {
            if (SplashKit.Rnd(0, 2) == 0) 
                Bitmap = SplashKit.LoadBitmap("asteroid1", "Asteroids/grayAsteroid.png");
            else
                Bitmap = SplashKit.LoadBitmap("asteroid2", "Asteroids/brownAsteroid.png");
        }
        public Asteroid() : this(Global.Width, Global.Height){}
        public void SetCoordinates(int lastAsteroidX, int lastAsteroidY)
        {
            X = (2 * SplashKit.Rnd(0, 6) + 1) * 50;
            Y = (lastAsteroidY > 50) ? -10 : -90;
        }
        public override void Update()
        {
            _movePattern.Update();
            Y = (int)_movePattern.UpdatedY;

            // Console.WriteLine("X is " + X);
            // Console.WriteLine("offset is " + XOffset);
            // Console.WriteLine("X from bitmap is " + Bitmap.CellCenter.X);
        } 
        
    }
}