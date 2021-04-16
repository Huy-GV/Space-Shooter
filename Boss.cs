using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Space_Shooter
{
    public class Nightmare : Enemy
    {
        private GunSystem _leftGun, _rightGun, _middleGun;
        private List<GunSystem> _guns;
        private int _speed;
        public Nightmare()
        {
            X = Global.Width / 2;
            Y = -20;
            Bitmap = SplashKit.LoadBitmap("Nightmare", "Nightmare.png");
            _guns = new List<GunSystem>
            {
                new GunSystem(Bullet.Direction.Down, 2),
                new GunSystem(Bullet.Direction.Down, 2),
                new GunSystem(Bullet.Direction.Down, 2),
            };
            _speed = 5;
            _movePattern = new ZigzagMovement(_speed, X, Y);
        }

        public override void Update()
        {
            
        }

    }
}