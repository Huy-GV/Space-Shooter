using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class RedAlienship : Alienship
    {
        public RedAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
        {
            ExplosionType = Explosion.Type.RedLaser;
            var bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
            Image = new StaticImage(bitmap);
            _gun = new Gun(2);
            _movePattern = new ZigzagPattern(2, 3, _x, _y);
        }
        public RedAlienship() : this(Global.Width, Global.Height) { }
        public override void Draw()
        { 
            Image.Draw(_movePattern.X, _movePattern.Y);
            _gun.DrawBullets();
        }
    }
}