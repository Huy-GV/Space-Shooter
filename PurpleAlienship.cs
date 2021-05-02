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
            Angle = 90;
            var bitmap = SplashKit.LoadBitmap("PurpleAlienship", "Alienships/PurpleAlienship.png");
            _image = new StaticImage(bitmap);
            _gun = new Gun( 3);
            _movePattern = new HorizontalPattern(2, 3, X, Y);
        }
        public PurpleAlienship() : this(Global.Width, Global.Height) { }
        public override void Draw()
        { 
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