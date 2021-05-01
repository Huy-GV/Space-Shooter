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
            var bitmap = SplashKit.LoadBitmap("RedAlienship", "Alienships/RedAlienship.png");
            _image = new StaticImage(bitmap);
            _gun = new Gun(2);
            _movePattern = new ZigzagPattern(2,3, X, Y);
        }
        public RedAlienship() : this(Global.Width, Global.Height) { }
        public override void Draw()
        { 
            // SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y); 
            _image.Draw(X, Y);
            _gun.DrawBullets();
        }
        public override void Update()
        {
            UpdateGun();
            Move();
        }
    }
}