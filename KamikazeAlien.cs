using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class KamikazeAlien : Enemy
    {
        public KamikazeAlien(Position position, IMoveStrategy movePattern, int angle, int damage) : base(position, movePattern, EnemyType.KamikazeAlien)
        {
            CollisionDamage = damage;
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