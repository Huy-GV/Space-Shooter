using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    
    public class PurpleAlienship : Alienship
    {
        public PurpleAlienship(Position position, ICanMove movePattern, Gun gun) : base(position, gun, movePattern)
        {
            Angle = 90;
            var bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
            Image = new StaticImage(bitmap);
        }
        public override void Update()
        {
            base.Update();
            var verticalLimit = (Global.Width / 2) / (SplashKit.Rnd(4) + 1);
            if (Y > verticalLimit && _movePattern.GetType() == typeof(StraightLinePattern)) 
                _movePattern = new HorizontalPattern(2);
        }
    }
}