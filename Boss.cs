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
            _position.X = Global.Width / 2;
            _position.Y = -50;
        }
        public virtual Bullet Shoot() => _gun.OpenFire(X, Y, Angle, 0); 
    }
    public class Nightmare : Boss
    { 
        private double _time;
        private double _movePatternDuration;
        private readonly int _defaultPatternDuration = 4;
        public Nightmare() : base()
        {
            _gun = new Gun(1.2, Bullet.Type.RedLaser, false);
            _speed = 5;
            _movePattern = new StraightLinePattern(_speed / 2, X, Y, 90);
            var bitmap = SplashKit.LoadBitmap("Nightmare", "Bosses/Nightmare.png");
            Image = new StaticImage(bitmap);
        }
        public override void Update()
        {
            base.Update();
            _gun.Update();
            if (Y > 100 && _movePattern is StraightLinePattern) 
                _movePattern = new HorizontalPattern(_speed - 3, X, Y);
            else if (_movePattern is not StraightLinePattern)
                SwitchMovePattern();
        }
        private void SwitchMovePattern()
        {
            _time += 1/(double)60;
            if (_time >= _movePatternDuration)
            {
                _time = 0;
                _movePatternDuration = _defaultPatternDuration;
                if (_movePattern is HorizontalPattern)
                    _movePattern = new ZigzagPattern(_speed - 1, _speed - 2, X, Y);
                else if (Y < Global.Height / 2)
                    _movePattern = new HorizontalPattern(_speed - 3, X, Y);
            }
        }
    }
    public class Phantom : Boss
    {
        private double _invisibleDuration;
        private double _time;
        private bool _isInvisible;
        public Phantom() : base()
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