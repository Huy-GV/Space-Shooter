using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class RedAlienship : Alienship
    {
        public RedAlienship(int lastEnemyX, int lastEnemyY) : base(lastEnemyX, lastEnemyY)
        {
            ExplosionType = Explosion.Type.RedLaser;
            Bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
            _gun = new Gun(2);
            _movePattern = new ZigzagMovement(2,3, X, Y);
        }
        public RedAlienship() : this(Global.Width, Global.Height) { }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y); 
            _gun.DrawBullets();
        }
        public override void Update()
        {
            UpdateGun();
            Move();
        }
    }
}