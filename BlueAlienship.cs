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
            var bitmap = SplashKit.LoadBitmap("BlueAlienship", "Alienships/BlueAlienship.png");
            Image = new StaticImage(bitmap);
            _gun = new Gun(3);
            _movePattern = new StraightLinePattern(3, X, Y, 90);
        }
        public BlueAlienship() : this(Global.Width, Global.Height) { }
        public override void Draw()
        { 
            Image.Draw(X, Y);
            _gun.DrawBullets();
        }
        public override void Update()
        {
            UpdateGun();
            Move();
        }
    }
}