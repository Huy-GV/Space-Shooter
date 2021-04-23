using System;
using System.Collections.Generic;
using SplashKitSDK;
using System.Linq;
namespace Space_Shooter
{
    public abstract class Boss : Enemy, IHaveGun
    {
        public List<Bullet> Bullets{get{ return _gun.Bullets;}}
        protected GunSystem _gun;   
        public int Damage{get; protected set;}
        protected int _health;
        protected int _speed;
        public Boss()
        {
            X = Global.Width / 2;
            Y = -21;
        }
        public override void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets.ToArray())
            {
                if (bullet.HitTarget(this))
                {
                    bullets.Remove(bullet);
                    _health -= bullet.Damage;
                    Console.WriteLine("boss hit");
                }
            }
        }
    }
    public class Nightmare : Boss
    { 
        private double _time;
        private double _movePatternDuration;
        public Nightmare()
        {
            X = Global.Width / 2;
            Y = -20;
            Damage = 12;
            _gun = new GunSystem(Bullet.Direction.Down, 1.2, Bullet.Type.RedLaser, false);
            _speed = 5;
            _health = 100;
            SetAnimation();
            _movePattern = new ZigzagMovement(_speed, _speed - 2, X, Y, true);
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
            ChangeMovePattern();
            _movePattern.Update();
            X = _movePattern.UpdatedX;
            Y = _movePattern.UpdatedY;
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
        private void ChangeMovePattern()
        {
            _time += 1/(double)60;
            if (_time >= _movePatternDuration)
            {
                _time = 0;
                _movePatternDuration = 4;
                if (_movePattern.GetType() == typeof(HorizontalMovement))
                {
                    _movePattern = new ZigzagMovement(_speed - 1, _speed - 2, X, Y);
                } else if (Y < Global.Height / 2)
                {
                    _movePattern = new HorizontalMovement(_speed - 2, _speed - 3, X, Y);
                }
            }
        }
    }

    public class Phantom : Boss
    {
        private double _invisibleDuration;
        private double _time;
        private bool _isInvisible;
        private Animation _animation;
        private DrawingOptions _option;
        private AnimationScript _flyScript;
        public Phantom()
        {
            Damage = 15;
            SetAnimation();
            _gun = new GunSystem(Bullet.Direction.Down, 1, Bullet.Type.TripleLaser, false);
            _speed = 4;
            _health = 100;
            _isInvisible = false;
            _invisibleDuration = 3;
            _movePattern = new ZigzagMovement(_speed, _speed, X, Y, true);
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
            if (_time >= _invisibleDuration)
            {
                _time = 0;
                _invisibleDuration = SplashKit.Rnd(3, 5);
                _isInvisible = !_isInvisible;
            }
        }
        public override void Draw()
        { 
            if (!_isInvisible) SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY, _option); 
            _gun.DrawBullets();
        }
    }
}