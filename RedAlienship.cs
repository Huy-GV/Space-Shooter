using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class RedAlienship : Alienship
    {
        public RedAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
        {
            Damage = 3;
            ExplosionType = Explosion.Type.RedLaser;
            SetAnimations();
            _gunSystem = new GunSystem(Bullet.Direction.Down, 2);
            
            _movePattern = new ZigzagMovement(2, X, Y);
        }
        public RedAlienship() : this(Global.Width, Global.Height) { }
        private void SetAnimations()
        {
            XOffset = 55;
            YOffset = 53;
            Bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
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