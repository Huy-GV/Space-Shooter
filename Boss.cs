using System;
using System.Collections.Generic;
using SplashKitSDK;
using System.Linq;
namespace Space_Shooter
{
    public class Nightmare : Enemy, IHaveGun
    {
        public List<Bullet> Bullets{get{ return _gun.Bullets;}}
        private GunSystem _gun; 
        private int _speed;
        private int _health;
        public Nightmare()
        {
            X = Global.Width / 2;
            Y = -20;
            Damage = 12;
            _gun = new GunSystem(Bullet.Direction.Down, 1.5, Bullet.Type.RedLaser, false);
            _speed = 5;
            _health = 100;
            SetAnimation();
            _movePattern = new ZigzagMovement(_speed, X, Y);
        }
        private void SetAnimation()
        {
            XOffset = 75;
            YOffset = 50;
            Damage = 10;
            Bitmap = SplashKit.LoadBitmap("Nightmare", "Bosses/Nightmare.png");
        }
        public override void Update()
        {
            UpdateGuns();
            _movePattern.Update();
            //TODO: MAKE THE ZIG ZAG MOVEMENT MORE RANDOM
            X = _movePattern.UpdatedX;
            Y = _movePattern.UpdatedY;
        }
        public override void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets.ToArray())
            {
                if (bullet.HitTarget(this))
                {
                    bullets.Remove(bullet);
                    _health -= 10;
                }
            }
        }
        private void UpdateGuns()
        {
            _gun.AutoFire(X, Y);
            _gun.Update();
        }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY); 
            _gun.DrawBullets();
        }
    }

    public class Phantom : Enemy, IHaveGun
    {
        public List<Bullet> Bullets{get{ return _gun.Bullets;}}
        private GunSystem _gun;
        private int _speed;
        private int _health;   
        private double _invisibleTime;
        private double _time;
        private bool _isInvisible;
        private Animation _animation;
        private DrawingOptions _option;
        private AnimationScript _flyScript;
        public Phantom()
        {
            X = Global.Width / 2;
            Y = -20;
            Damage = 15;
            SetAnimation();
            _gun = new GunSystem(Bullet.Direction.Down, 1.2, Bullet.Type.TripleLaser, false);
            _speed = 6;
            _health = 100;
            _isInvisible = false;
            _movePattern = new ZigzagMovement(_speed, X, Y);
        }
        private void SetAnimation()
        {
            Bitmap = SplashKit.LoadBitmap("Phantom", "Bosses/Phantom.png");
            Bitmap.SetCellDetails(300/2, 130, 2, 1, 2);
            XOffset = 300 / 4;
            YOffset = 130 / 2;
            _flyScript = SplashKit.LoadAnimationScript("Flickering", "flickerScript.txt");
            _animation = _flyScript.CreateAnimation("flickering");
            _option = SplashKit.OptionWithAnimation(_animation);
        }
        public override void Update()
        {
            _movePattern.Update();
            _gun.AutoFire(X, Y);
            _gun.Update();
            _animation.Update();
            X = _movePattern.UpdatedX;
            Y = _movePattern.UpdatedY;
            UpdateVisibility();
        }
        private void UpdateVisibility()
        {
            _time += 1/(double)60;
            if (_time >= _invisibleTime)
            {
                _time = 0;
                _invisibleTime = SplashKit.Rnd(0, 4) + 3;
                _isInvisible = !_isInvisible;
            }
        }
        public override void Draw()
        { 
            if (!_isInvisible) SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY, _option); 
            _gun.DrawBullets();
        }
        public override void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets.ToArray())
            {
                if (bullet.HitTarget(this))
                {
                    bullets.Remove(bullet);
                    _health -= 7;
                }
            }
        }
    }
}