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
            Bitmap = SplashKit.LoadBitmap("BlueAlienship", "Alienships/BlueAlienship.png");
            _gun = new Gun(3);
            _movePattern = new StraightMovement(3, X, Y, 90);
        }
        public BlueAlienship() : this(Global.Width, Global.Height) { }
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