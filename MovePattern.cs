using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{

    public abstract class MovePattern
    {
        protected Point2D _updatedPosition = new Point2D();
        public MovePattern(double x, double y)
        {
            _updatedPosition.Y = y;
            _updatedPosition.X = x;
        }
        public abstract Point2D Update();
    }
    public class HorizontalPattern : MovePattern
    {
        private double _speed;
        private int _direction;
        public HorizontalPattern(int speed, double x, double y) : base(x ,y) 
        { 
            _direction = SplashKit.Rnd(2) * 2 - 1;
            _speed = speed;
        }
        public override Point2D Update()
        {
            _updatedPosition.X += _direction * _speed;
            if (_updatedPosition.X >= Global.Width - 5 || _updatedPosition.X <= 5) _direction *= -1;
            return _updatedPosition;
        }
    }
    public class ZigzagPattern : MovePattern
    {
        private int _heightLimit, _verticalDirection, _horizontalDirection;
        private double _horizontalSpeed, _verticalSpeed;
        private bool _firstCrossing;
        private bool _random;
        public ZigzagPattern(int horizontalSpeed, int verticalSpeed, double x, double y) : this(horizontalSpeed,  verticalSpeed, x ,y, false) {}
        public ZigzagPattern(int horizontalSpeed, int verticalSpeed, double x, double y, bool random): base(x, y)
        {
            _verticalDirection = 1;
            _horizontalDirection = 1;
            _heightLimit = Global.Width / 2;
            _horizontalSpeed = horizontalSpeed;
            _verticalSpeed = verticalSpeed;
            if (_horizontalSpeed == _verticalSpeed) _horizontalSpeed++;
            //if it enters the game window for the first time, it wont reverse its vertical direction
            _firstCrossing = _updatedPosition.Y <= 0 ? true : false;
            _random = random;
        }
        public override Point2D Update()
        {
            _updatedPosition.Y += _verticalSpeed * _verticalDirection;
            _updatedPosition.X += _horizontalSpeed * _horizontalDirection;
            RandomizeSpeed();
            UpdateHorizontalComponent();
            UpdateVerticalComponent();
            return _updatedPosition;
        }   
        private void UpdateHorizontalComponent()
        {
            if (_updatedPosition.X >= Global.Width - 5 || _updatedPosition.X <= 5) 
                _horizontalDirection *= -1;
        }
        private void UpdateVerticalComponent()
        {
            if (_updatedPosition.Y > _heightLimit && _verticalDirection != -1)
            {
                if (_firstCrossing) 
                    _firstCrossing = false;
                _verticalDirection *= -1;
            } else if (_updatedPosition.Y <= 0 && !_firstCrossing && _verticalDirection != 1)
                _verticalDirection *= -1;
        }
        private bool RandomInstance() => SplashKit.Rnd(0,70) == 0;
        private void RandomizeSpeed()
        {
            if(_random && RandomInstance()) 
            {
                var temp = _horizontalSpeed;
                _horizontalSpeed = _verticalSpeed;
                _verticalSpeed = temp;
            }
        }
    }
    public class StraightLinePattern : MovePattern
    {
        private double _speed;
        private Vector2D _pathVector;
        private int _targetAngle;
        public StraightLinePattern(int speed, int x, int y, int angle) : base(x ,y)
        {
            _targetAngle = angle;
            _speed = speed;
            _pathVector = SplashKit.VectorFromAngle(_targetAngle, 1);
            // Console.WriteLine("angle is " + _targetAngle);
            // Console.WriteLine("vector is " + _pathVector.X +" "+ _pathVector.Y);
        }
        public StraightLinePattern(int speed, int x, int y) : 
        this(speed, x ,y, (SplashKit.Rnd(0, 42) + 69)){}
        public override Point2D Update()
        {
            _updatedPosition.X += _pathVector.X * _speed;
            _updatedPosition.Y += _pathVector.Y * _speed;
            return _updatedPosition;
        }
    }
}