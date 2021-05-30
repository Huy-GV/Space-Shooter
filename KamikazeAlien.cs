using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class KamikazeAlien : Enemy
    {
        public KamikazeAlien() : base()
        {
            var x = Global.Width / 2;
            var y = -50;
            CollisionDamage = 13;
            Angle = (SplashKit.Rnd(0, 42) + 69);
            _movePattern = new StraightLinePattern(9, x, y, Angle);
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