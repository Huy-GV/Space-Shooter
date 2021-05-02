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
            var bitmap = SplashKit.LoadBitmap("Nightmare", "Bosses/Nightmare.png");
            _image = new StaticImage(bitmap);
            _movePattern = new ZigzagPattern(_speed, _speed - 2, X, Y, true);
        }
        public override void Update()
        {
            _gun.AutoFire(X, Y, Angle, 0);
            _gun.Update();
            Move();
            ChangeMovePattern();
        }
        public override void Draw()
        { 
            _image.Draw(X, Y);
            _gun.DrawBullets();
        }
        private void ChangeMovePattern()
        {
            _time += 1/(double)60;
            if (_time >= _movePatternDuration)
            {
                _time = 0;
                _movePatternDuration = 4;
                if (_movePattern.GetType() == typeof(HorizontalPattern))
                {
                    _movePattern = new ZigzagPattern(_speed - 1, _speed - 2, X, Y);
                } else if (Y < Global.Height / 2)
                {
                    _movePattern = new HorizontalPattern(_speed - 2, _speed - 3, X, Y);
                }
            }
        }
    }

    public class Phantom : Boss
    {
        private double _invisibleDuration;
        private double _time;
        private bool _isInvisible;
        public Phantom()
        {
            SetAnimation();
            _gun = new Gun(1, Bullet.Type.TripleLaser, false);
            _speed = 4;
            _isInvisible = false;
            _invisibleDuration = 3;
            _movePattern = new ZigzagPattern(_speed, _speed, X, Y, true);
        }
        private void SetAnimation()
        {
            var bitmap = SplashKit.LoadBitmap("Phantom", "Bosses/Phantom.png");
            var cellDetails = new int[]{300/2, 130, 2, 1, 2};
            _image = new AnimatedImage("flickerScript", "flickering", bitmap, cellDetails);
        }
        public override void Update()
        {
            _gun.AutoFire(X, Y, Angle, 0);
            _gun.Update();
            Move();
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
            if (!_isInvisible) _image.Draw(X, Y); 
            _gun.DrawBullets();
        }
    }
}