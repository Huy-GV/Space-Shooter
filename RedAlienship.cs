using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class RedAlienship : Alienship
    {
        public RedAlienship(Position position, IMoveStrategy movePattern, Gun gun) : base(position, gun, movePattern)
        {
            var bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
            Image = new StaticImage(bitmap);
        }
    }
}