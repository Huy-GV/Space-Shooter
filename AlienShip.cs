using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public abstract class Alienship : Enemy, IHaveGun
    {
        protected Gun _gun;
        protected int _x, _y;
        public List<Bullet> Bullets{ get { return _gun.Bullets; }}
        public Alienship(int lastEnemyX, int lastEnemyY) : base()
        {
            int randomX = SplashKit.Rnd(0, 6);
            if (lastEnemyY >= 100){
                _x = (2 * randomX + 1) * 50; 
                _y = -50;
            } else
            {
                while ( (2 * randomX + 1) * 50  == lastEnemyX) randomX = SplashKit.Rnd(0, 6);
                _x = (2 * randomX + 1) * 50; 
                _y = lastEnemyY - 100;
            } 
        }
        public override void Update()
        {
            base.Update();
            UpdateGun();
        }
        protected void UpdateGun()
        { 
            _gun.Update();
            if (_movePattern.Y >= 0 && _movePattern.Y <= Global.Height / 2) 
                _gun.AutoFire(_movePattern.X, _movePattern.Y, Angle, 0); 
        }
    }
}