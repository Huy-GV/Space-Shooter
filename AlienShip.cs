using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public abstract class Alienship : Enemy, IHaveGun
    {
        protected GunSystem _gunSystem;
        public List<Bullet> Bullets{ get { return _gunSystem.Bullets; }}
        public Alienship(int lastEnemyX, int lastEnemyY) : base()
        {
            int randomX = SplashKit.Rnd(0, 6);
            if (lastEnemyY >= 100){
                X = (2 * randomX + 1) * 50; 
                Y = -50;
            } else
            {
                while ( (2 * randomX + 1) * 50  == lastEnemyX) randomX = SplashKit.Rnd(0, 6);
                X = (2 * randomX + 1) * 50; 
                Y = lastEnemyY - 100;
            } 
        }
    }
}