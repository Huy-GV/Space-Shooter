using System;
using System.Collections.Generic;
using SplashKitSDK;
using System.Linq;
namespace SpaceShooter
{
    public abstract class Boss : Enemy, IHaveGun
    {
        public bool CoolDownEnded{get => _gun.CoolDownEnded;}
        protected Gun _gun;   
        protected int _speed;
        public Boss()
        {
            Health = 80;
        }
        public virtual Bullet Shoot() => _gun.OpenFire(X, Y, Angle, 0); 
    }
    public class Nightmare : Boss
    { 
        private double _time;
        private double _movePatternDuration;
        private readonly int _defaultPatternDuration = 4;
        public Nightmare()
        {
            _gun = new Gun(1.2, Bullet.Type.RedLaser, false);
            _speed = 5;
            var bitmap = SplashKit.LoadBitmap("Nightmare", "Bosses/Nightmare.png");
            Image = new StaticImage(bitmap);
            _movePattern = new ZigzagPattern(_speed, _speed - 2, Global.Width/2 , -20, true);
        }
        public override void Update()
        {
            base.Update();
            _gun.Update();
            ChangeMovePattern();
        }
        private void ChangeMovePattern()
        {
            _time += 1/(double)60;
            if (_time >= _movePatternDuration)
            {
                _time = 0;
                _movePatternDuration = _defaultPatternDuration;
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
            Image = SetAnimation();
            _gun = new Gun(1, Bullet.Type.TripleLaser, false);
            _speed = 4;
            _isInvisible = false;
            _invisibleDuration = 3;
            _movePattern = new ZigzagPattern(_speed, _speed, Global.Width/2 , -20, true);
        }
        private Image SetAnimation()
        {
            var bitmap = SplashKit.LoadBitmap("Phantom", "Bosses/Phantom.png");
            var cellDetails = new int[]{300/2, 130, 2, 1, 2};
            return new AnimatedImage("flickerScript", "flickering", bitmap, cellDetails);
        }
        public override void Update()
        {
            base.Update();
            _gun.Update();
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
            if (!_isInvisible) Image.Draw(X, Y); 
        }
    }
}