using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Space_Shooter
{
    public class Nightmare : Enemy
    {
        private GunSystem _leftMiddleGun, _rightMiddleGun, _leftGun, _rightGun;
        ///TODO: the bullet property will return concatenated lists of bullets from the 4 guns
        private List<GunSystem> _guns;
        private int _speed;
        private int _health;
        public Nightmare()
        {
            X = Global.Width / 2;
            Y = -20;
            Bitmap = SplashKit.LoadBitmap("Nightmare", "Nightmare.png");
            _leftGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _rightGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _leftMiddleGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _rightMiddleGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _speed = 5;
            _health = 100;
            _movePattern = new ZigzagMovement(_speed, X, Y);
        }
        public override void Update()
        {
            UpdateGuns();
            _movePattern.Update();
            X = _movePattern.UpdatedX;
            Y = _movePattern.UpdatedY;
        }
        public override void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets)
            {
                if (bullet.HitTarget(this))
                {
                    bullets.Remove(bullet);
                    _health -= 7;
                }
            }
        }
        private void UpdateGuns()
        {
            _leftGun.AutoFire(X - 20, Y);
            _rightGun.AutoFire(X + 20, Y);
            _leftMiddleGun.AutoFire(X - 10, Y);
            _rightMiddleGun.AutoFire(X + 10, Y);
        }
    }

    //TODO: make the second boss with the ability to disappear, spawn kamikazes?

    public class Mothership : Enemy
    {
        private GunSystem _leftMiddleGun, _rightMiddleGun, _leftGun, _rightGun;
        ///TODO: the bullet property will return concatenated lists of bullets from the 4 guns
        private List<GunSystem> _guns;
        private int _speed;
        private int _health;   
        public Mothership()
        {
            X = Global.Width / 2;
            Y = -20;
            Bitmap = SplashKit.LoadBitmap("Nightmare", "Nightmare.png");
            _leftGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _rightGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _leftMiddleGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _rightMiddleGun = new GunSystem(Bullet.Direction.Down, 2, Bullet.Type.BlueLaser, false);
            _speed = 5;
            _health = 100;
            _movePattern = new ZigzagMovement(_speed, X, Y);
        }
        public override void Update()
        {
            UpdateGuns();
            _movePattern.Update();
            X = _movePattern.UpdatedX;
            Y = _movePattern.UpdatedY;
        }
        public override void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets)
            {
                if (bullet.HitTarget(this))
                {
                    bullets.Remove(bullet);
                    _health -= 7;
                }
            }
        }
        private void UpdateGuns()
        {
            _leftGun.AutoFire(X - 20, Y);
            _rightGun.AutoFire(X + 20, Y);
            _leftMiddleGun.AutoFire(X - 10, Y);
            _rightMiddleGun.AutoFire(X + 10, Y);
        }
    }
}