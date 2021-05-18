using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class RedAlienship : Alienship
    {
        public RedAlienship(int lastEnemyX = Global.Width, int lastEnemyY = Global.Height) : base(lastEnemyX, lastEnemyY)
        {
            ExplosionType = Explosion.Type.RedLaser;
            var bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
            Image = new StaticImage(bitmap);
            _gun = new Gun(2);
            _movePattern = new ZigzagPattern(2, 3, _x, _y);
        }
    }
}