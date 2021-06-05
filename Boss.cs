using System;
using System.Collections.Generic;
using SplashKitSDK;
using System.Linq;
namespace SpaceShooter
{
    // public abstract class Boss : Enemy, IHaveGun
    // {
    //     public bool OverheatEnded{get => _gun.OverheatEnded;}
    //     protected Gun _gun;   
    //     protected int _speed;
    //     public Boss() : base(new Position(Global.Width / 2, -50))
    //     {
    //         Health = 80;
    //     }
    //     public virtual Bullet Shoot() => _gun.OpenFire(X, Y, Angle, 0); 
    // }
    public class Nightmare : Alienship
    { 
        private double _time;
        private int _speed;
        private double _movePatternDuration;
        private readonly int _defaultPatternDuration = 4;
        public Nightmare(Position position, IMoveStrategy movePattern, Gun gun) : base(position, gun, movePattern)
        {
            _speed = 5;
            var bitmap = SplashKit.LoadBitmap("Nightmare", "Bosses/Nightmare.png");
            Image = new StaticImage(bitmap);
            Health = 70;
        }
        public override void Update()
        {
            base.Update();
            _gun.Update();
            if (Y > 100 && _movePattern.GetType() == typeof(StraightLinePattern)) 
                _movePattern = new HorizontalPattern(_speed - 3);
            else if (_movePattern.GetType() != typeof(StraightLinePattern))
                SwitchMovePattern();
        }
        private void SwitchMovePattern()
        {
            _time += 1/(double)60;
            if (_time >= _movePatternDuration)
            {
                _time = 0;
                _movePatternDuration = _defaultPatternDuration;
                if (_movePattern.GetType() == typeof(HorizontalPattern))
                    _movePattern = new ZigzagPattern(_speed - 1, _speed - 2, Y);
                else if (Y < Global.Height / 2)
                    _movePattern = new HorizontalPattern(_speed - 3);
            }
        }
    }
    public class Phantom : Alienship
    {
        private double _invisibleDuration = 3;
        private double _time;
        private int _speed;
        private bool _isInvisible = false;
        public Phantom(Position position, IMoveStrategy movePattern, Gun gun) : base(position, gun, movePattern)
        {
            Image = SetAnimation();
            Health = 80;
            _gun = new Gun(1, Bullet.Type.TripleLaser, false);
            _speed = 4;
            _movePattern = new ZigzagPattern(_speed, _speed, -20, true);
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