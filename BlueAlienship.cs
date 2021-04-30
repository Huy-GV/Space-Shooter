using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class BlueAlienship : Alienship
    {
        public BlueAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
        {
            ExplosionType = Explosion.Type.Fire;
            SetAnimations();
            _gunSystem = new GunSystem(Bullet.Direction.Down, 3);
            _movePattern = new VerticalMovement(3, X, Y);
        }
        public BlueAlienship() : this(Global.Width, Global.Height) { }
        private void SetAnimations()
        {
            XOffset = 40;
            YOffset = 55;
            Bitmap = SplashKit.LoadBitmap("BlueAlienship", "Alienships/BlueAlienship.png");
        }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY); 
            _gunSystem.DrawBullets();
        }
        public override void Update()
        {
            if (Y >= 0 && Y <= Global.Height / 2) _gunSystem.AutoFire(X, Y);
            _gunSystem.Update();
            _movePattern.Update();
            Y = _movePattern.UpdatedY;
            X = _movePattern.UpdatedX;
        }
    }
}