using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class PurpleAlienship : Alienship
    {
        public PurpleAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
        {
            ExplosionType = Explosion.Type.Fire;
            Angle = 90;
            var bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
            Image = new StaticImage(bitmap);
            _gun = new Gun(3);
            _movePattern = new HorizontalPattern(2, 3, _x, _y);
        }
        public PurpleAlienship() : this(Global.Width, Global.Height) { }
    }
}