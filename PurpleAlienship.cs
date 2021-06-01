using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class PurpleAlienship : Alienship
    {
        public PurpleAlienship(int lastEnemyX = Global.Width, int lastEnemyY = Global.Height) : base(lastEnemyX, lastEnemyY)
        {
            Angle = 90;
            var bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
            Image = new StaticImage(bitmap);
            _gun = new Gun(3);
            _movePattern = new StraightLinePattern(2, X, Y, 90);
        }
        public override void Update()
        {
            base.Update();
            var verticalLimit = (Global.Width / 2) / (SplashKit.Rnd(4) + 1);
            if (Y > verticalLimit && _movePattern.GetType() == typeof(StraightLinePattern)) 
                _movePattern = new HorizontalPattern(2, X, Y);
        }
    }
}