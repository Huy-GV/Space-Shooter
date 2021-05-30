using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class PurpleAlienship : Alienship
    {
        public PurpleAlienship(int lastEnemyX = Global.Width, int lastEnemyY = Global.Height) : base(lastEnemyX, lastEnemyY)
        {
            ExplosionType = Explosion.Type.Fire;
            Angle = 90;
            var bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
            Image = new StaticImage(bitmap);
            _gun = new Gun(3);
            _movePattern = new StraightLinePattern(2, _x, _y, 90);
        }
        public override void Update()
        {
            base.Update();
            var verticalLimit = (Global.Width / 2) / (SplashKit.Rnd(4) + 1);
            if (_movePattern.Y > verticalLimit && _movePattern is StraightLinePattern) 
                _movePattern = new HorizontalPattern(2, X, Y);
        }
    }
}