using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    
    public class BlueAlienship : Alienship
    {
        public BlueAlienship(Position position, ICanMove movePattern, Gun gun) : base(position, gun, movePattern)
        {
            var bitmap = SplashKit.LoadBitmap("BlueAlienship", "Alienships/BlueAlienship.png");
            Image = new StaticImage(bitmap);
        }
    }
}