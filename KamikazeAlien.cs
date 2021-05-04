using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class KamikazeAlien : Enemy
    {
        public KamikazeAlien(int lastEnemyX, int lastEnemyY) : base()
        {
            X = Global.Width / 2;
            Y = -50;
            CollisionDamage = 13;
            ExplosionType = Explosion.Type.Fire;
            Angle = (SplashKit.Rnd(0, 42) + 69);
            _movePattern = new StraightLinePattern(9, X, Y, Angle);
            SetAnimations();
        }
        public KamikazeAlien() : this(Global.Width, Global.Height) { }
        private void SetAnimations()
        {
            var bitmap = SplashKit.LoadBitmap("KamikazeAlien", "Alienships/KamikazeAlien.png");
            var option = SplashKit.OptionRotateBmp(Angle - 90);
            Image = new StaticImage(bitmap, option);
        }
        public override void Draw()
        { 
            Image.Draw(X, Y);
        }
        public override void Update()
        {
            Move();
        }
    }
}