using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class KamikazeAlien : Enemy
    {
        public KamikazeAlien(Position position, ICanMove movePattern, int angle) : base(position, movePattern)
        {
            CollisionDamage = 13;
            Angle = angle;
            Image = SetAnimation();
        }
        private Image SetAnimation()
        {
            var bitmap = SplashKit.LoadBitmap("KamikazeAlien", "Alienships/KamikazeAlien.png");
            var option = SplashKit.OptionRotateBmp(Angle - 90);
            return new StaticImage(bitmap, option);
        }
    }
}