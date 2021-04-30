using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class PurpleAlienship : Alienship
    {
        public PurpleAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
        {
            ExplosionType = Explosion.Type.Fire;
            Bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
            _gunSystem = new GunSystem(Bullet.Direction.Down, 3);
            _movePattern = new HorizontalMovement(2, 3, X, Y);
        }
        public PurpleAlienship() : this(Global.Width, Global.Height) { }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y); 
            _gunSystem.DrawBullets();
        }
        public override void Update()
        {
            if (Y >= 0) _gunSystem.AutoFire(X, Y);
            _gunSystem.Update();
            _movePattern.Update();
            Y = _movePattern.UpdatedY;
            X = _movePattern.UpdatedX;
        }
    }
}