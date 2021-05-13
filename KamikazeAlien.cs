using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class KamikazeAlien : Enemy
    {
        public KamikazeAlien(int lastEnemyX, int lastEnemyY) : base()
        {
            var x = Global.Width / 2;
            var y = -50;
            CollisionDamage = 13;
            ExplosionType = Explosion.Type.Fire;
            Angle = (SplashKit.Rnd(0, 42) + 69);
            _movePattern = new StraightLinePattern(9, x, y, Angle);
            Image = SetAnimation();
        }
        public KamikazeAlien() : this(Global.Width, Global.Height) { }
        private Image SetAnimation()
        {
            var bitmap = SplashKit.LoadBitmap("KamikazeAlien", "Alienships/KamikazeAlien.png");
            var option = SplashKit.OptionRotateBmp(Angle - 90);
            return new StaticImage(bitmap, option);
        }
        public override void Draw()
        { 
            Image.Draw(_movePattern.X, _movePattern.Y);
        }
    }
}