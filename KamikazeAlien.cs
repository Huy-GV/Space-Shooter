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
            X = (2 * SplashKit.Rnd(0, 6) + 1) * 50; 
            Y = -50;
            CollisionDamage = 13;
            ExplosionType = Explosion.Type.Fire;
            _movePattern = new ChargingMovement(9, X, Y);
            SetAnimations();
        }
        public KamikazeAlien() : this(Global.Width, Global.Height) { }
        private void SetAnimations()
        {
            XOffset = 40;
            YOffset = 55;
            Bitmap = SplashKit.LoadBitmap("KamikazeAlien", "Alienships/KamikazeAlien.png");
            var angle = (_movePattern as ChargingMovement).GetAngle(X);
            _option = SplashKit.OptionRotateBmp(angle);
        }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY, _option); 
        }
        public override void Update()
        {
            _movePattern.Update();
            Y = (int)_movePattern.UpdatedY;
            X = (int)_movePattern.UpdatedX;
        }
    }
}