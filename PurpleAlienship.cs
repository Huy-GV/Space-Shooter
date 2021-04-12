using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class PurpleAlienship : Alienship
    {
        public PurpleAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
        {
            Damage = 4;
            ExplosionType = Explosion.Type.Fire;
            SetAnimations();
            _gunSystem = new GunSystem(Bullet.Direction.Down, 3);
            _movePattern = new HorizontalMovement(2, 3, X, Y);
        }
        public PurpleAlienship() : this(Global.Width, Global.Height) { }
        private void SetAnimations()
        {
            XOffset = 55;
            YOffset = 55;
            Bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
        }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY); 
            _gunSystem.DrawBullets();
        }
        public override void Update()
        {
            if (Y >= 0) _gunSystem.AutoFire(X, Y);
            _gunSystem.Update();
            _movePattern.Perform();
            Y = (int)_movePattern.UpdatedY;
            X = (int)_movePattern.UpdatedX;
        }
    }
}