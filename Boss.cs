using System;
using System.Collections.Generic;
using SplashKitSDK;
using System.Linq;
namespace Space_Shooter
{
    public abstract class Boss : Enemy, IHaveGun
    {
        public List<Bullet> Bullets{get{ return _gun.Bullets;}}
        protected Gun _gun;   
        protected int _speed;
        public Boss()
        {
            X = Global.Width / 2;
            Y = -21;
            Health = 80;
        }
        public override void CheckPlayerBullets(List<Bullet> bullets)
        {
            foreach(var bullet in bullets.ToArray())
            {
                if (bullet.HitTarget(this))
                {
                    bullets.Remove(bullet);
                    Health -= bullet.Damage;
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
            _gun = new Gun(1.2, Bullet.Type.RedLaser, false);
            _speed = 5;
            Bitmap = SplashKit.LoadBitmap("Nightmare", "Bosses/Nightmare.png");
            _movePattern = new ZigzagMovement(_speed, _speed - 2, X, Y, true);
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
            _gun.AutoFire(X, Y, Angle);
            _gun.Update();
        }
        public override void Draw()
        { 
            SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y); 
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
            SetAnimation();
            _gun = new Gun(1, Bullet.Type.TripleLaser, false);
            _speed = 4;
            _isInvisible = false;
            _invisibleDuration = 3;
            _movePattern = new ZigzagMovement(_speed, _speed, X, Y, true);
        }
        private void SetAnimation()
        {
            Bitmap = SplashKit.LoadBitmap("Phantom", "Bosses/Phantom.png");
            Bitmap.SetCellDetails(300/2, 130, 2, 1, 2);
            _flyScript = SplashKit.LoadAnimationScript("Flickering", "flickerScript.txt");
            _animation = _flyScript.CreateAnimation("flickering");
            _option = SplashKit.OptionWithAnimation(_animation);
        }
        public override void Update()
        {
            _movePattern.Update();
            _gun.AutoFire(X, Y, Angle);
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
            if (!_isInvisible) SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y, _option); 
            _gun.DrawBullets();
        }
    }
}