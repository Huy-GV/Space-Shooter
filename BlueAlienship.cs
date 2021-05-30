using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    
    public class BlueAlienship : Alienship
    {
        public BlueAlienship(int lastEnemyX = Global.Width, int lastEnemyY = Global.Height) : base(lastEnemyX, lastEnemyY)
        {
            var bitmap = SplashKit.LoadBitmap("BlueAlienship", "Alienships/BlueAlienship.png");
            Image = new StaticImage(bitmap);
            _gun = new Gun(3);
            _movePattern = new StraightLinePattern(3, X, Y, 90);
        }
    }
}