using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{

    public abstract class MovePattern
    {
        protected Point2D _updatedPosition;
        public int X { get{ return (int)_updatedPosition.X;}}
        public int Y { get{ return (int)_updatedPosition.Y;}}
        public double HorizontalSpeed{get; protected set;}
        public double VerticalSpeed{get; protected set;}
        public MovePattern(double horizontalSpeed, double verticalSpeed, double x, double y)
        {
            HorizontalSpeed = horizontalSpeed;
            VerticalSpeed = verticalSpeed ;
            _updatedPosition = new Point2D();
            _updatedPosition.Y = y;
            _updatedPosition.X = x;
        }
        public abstract void Update();
    }
    public class HorizontalPattern : MovePattern
    {
        private int _verticalLimit;
        private int _direction;
        public HorizontalPattern(int horizontalSpeed, int verticalSpeed, double x, double y) : base(horizontalSpeed, verticalSpeed, x ,y) 
        { 
            _verticalLimit = (Global.Width / 2) / (SplashKit.Rnd(4) + 1);
            _direction = SplashKit.Rnd(2) * 2 - 1;
        }
        public override void Update()
        {
            if (_updatedPosition.Y < _verticalLimit) _updatedPosition.Y += VerticalSpeed;
            else
            {
                _updatedPosition.X += _direction * HorizontalSpeed;
                if (_updatedPosition.X >= Global.Width - 5 || _updatedPosition.X <= 5) _direction *= -1;
            }
        }
    }
    public class ZigzagPattern : MovePattern
    {
        private int _heightLimit, _verticalDirection, _horizontalDirection;
        private bool _firstCrossing;
        private bool _random;
        public ZigzagPattern(int horizontalSpeed, int verticalSpeed, double x, double y) : this(horizontalSpeed,  verticalSpeed, x ,y, false) {}
        public ZigzagPattern(int horizontalSpeed, int verticalSpeed, double x, double y, bool random): base(horizontalSpeed,  verticalSpeed, x, y)
        {
            _verticalDirection = 1;
            _horizontalDirection = 1;
            _heightLimit = Global.Width / 2;
            if (HorizontalSpeed == VerticalSpeed) horizontalSpeed++;
            //if it enters the game window for the first time, it wont reverse its vertical direction
            _firstCrossing = _updatedPosition.Y <= 0 ? true : false;
            _random = random;
        }
        public override void Update()
        {
            _updatedPosition.Y += VerticalSpeed * _verticalDirection;
            _updatedPosition.X += HorizontalSpeed * _horizontalDirection;
            RandomizeSpeed();
            UpdateHorizontalComponent();
            UpdateVerticalComponent();
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
                var temp = HorizontalSpeed;
                HorizontalSpeed = VerticalSpeed;
                VerticalSpeed = temp;
            }
        }
    }
    public class StraightLinePattern : MovePattern
    {
        private Vector2D _pathVector;
        private int _targetAngle;
        public StraightLinePattern(int speed, int x, int y, int angle) : base(speed, speed, x ,y)
        {
            _targetAngle = angle;
            _pathVector = SplashKit.VectorFromAngle(_targetAngle, 1);
            // Console.WriteLine("angle is " + _targetAngle);
            // Console.WriteLine("vector is " + _pathVector.X +" "+ _pathVector.Y);
        }
        public StraightLinePattern(int speed, int x, int y) : base(speed, speed, x ,y)
        {
            _targetAngle = (SplashKit.Rnd(0, 42) + 69);
            _pathVector = SplashKit.VectorFromAngle(_targetAngle, 1);
        }
        public override void Update()
        {
            _updatedPosition.X += _pathVector.X * VerticalSpeed;
            _updatedPosition.Y += _pathVector.Y * VerticalSpeed;
        }
    }
}