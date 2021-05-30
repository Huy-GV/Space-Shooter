using SplashKitSDK;
using System;
using System.Collections.Generic;

//TODO: return to the old system, with x and y belonging to objects

namespace SpaceShooter
{
    public abstract class Alienship : Enemy, IHaveGun
    {
        protected Gun _gun;
        public bool CoolDownEnded => _gun.CoolDownEnded;
        public Alienship(int lastEnemyX, int lastEnemyY) : base()
        {
            int randomX = SplashKit.Rnd(0, 6);
            if (lastEnemyY >= 100){
                _position.X = (2 * randomX + 1) * 50; 
                _position.Y = -50;
            } else
            {
                while ( (2 * randomX + 1) * 50  == lastEnemyX) randomX = SplashKit.Rnd(0, 6);
                _position.X = (2 * randomX + 1) * 50; 
                _position.Y = lastEnemyY - 100;
            } 

        }
        public override void Update()
        {
            base.Update();
            _gun.Update();
        }
        public Bullet Shoot() => _gun.OpenFire(X, Y, Angle, 0); 
    }
}