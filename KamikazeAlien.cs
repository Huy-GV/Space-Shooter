using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class KamikazeAlien : Enemy
    {
        private DrawingOptions _option;
        public KamikazeAlien(int lastEnemyX, int lastEnemyY) : base()
        {
            X = Global.Width / 2;
            Y = -50;
            CollisionDamage = 13;
            ExplosionType = Explosion.Type.Fire;
            Angle = (SplashKit.Rnd(0, 42) + 69);
            _movePattern = new StraightMovement(9, X, Y, Angle);
            SetAnimations();
        }
        public KamikazeAlien() : this(Global.Width, Global.Height) { }
        private void SetAnimations()
        {
            Bitmap = SplashKit.LoadBitmap("KamikazeAlien", "Alienships/KamikazeAlien.png");
            _option = SplashKit.OptionRotateBmp(Angle - 90);
        }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y, _option); 
        }
        public override void Update()
        {
            Move();
        }
    }
}