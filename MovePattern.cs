using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace Space_Shooter
{

    public abstract class MovePattern
    {
        protected int _direction;
        protected Point2D _position;
        public int UpdatedX { get{ return (int)_position.X;}}
        public int UpdatedY { get{ return (int)_position.Y;}}
        public double HorizontalSpeed{get; protected set;}
        public double VerticalSpeed{get; protected set;}
        public MovePattern(double horizontalSpeed, double verticalSpeed, double x, double y)
        {
            HorizontalSpeed = horizontalSpeed;
            VerticalSpeed = verticalSpeed ;
            _position = new Point2D();
            _position.Y = y;
            _position.X = x;
            //direction is either 1 or -1
            _direction = SplashKit.Rnd(2) * 2 - 1;
        }
        public abstract void Update();
    }
    public class HorizontalMovement : MovePattern
    {
        private int _verticalLimit;
        public HorizontalMovement(int horizontalSpeed, int verticalSpeed, double x, double y) : base(horizontalSpeed, verticalSpeed, x ,y) 
        { 
            _verticalLimit = (Global.Width / 2) / (SplashKit.Rnd(4) + 1);
        }
        public override void Update()
        {
            if (_position.Y < _verticalLimit) _position.Y += VerticalSpeed;
            else
            {
                _position.X += _direction * HorizontalSpeed;
                if (_position.X >= Global.Width - 5 || _position.X <= 5) _direction *= -1;
            }
        }
    }
    public class VerticalMovement : MovePattern
    {
        public VerticalMovement(int verticalSpeed, double x, double y) : base(0, verticalSpeed, x, y)
        {
            _direction = 1;
        }
        public VerticalMovement(int verticalSpeed, double x, double y, int direction) : base(0, verticalSpeed, x, y) 
        {
            _direction = direction;
        }
        public override void Update()
        {
            _position.Y += VerticalSpeed * _direction;
        }
    }
    public class ZigzagMovement : MovePattern
    {
        private int _heightLimit, _verticalDirection, _horizontalDirection;
        private bool _firstCrossing;
        private bool _random;
        public ZigzagMovement(int horizontalSpeed, int verticalSpeed, double x, double y) : this(horizontalSpeed,  verticalSpeed, x ,y, false) {}
        public ZigzagMovement(int horizontalSpeed, int verticalSpeed, double x, double y, bool random): base(horizontalSpeed,  verticalSpeed, x, y)
        {
            _verticalDirection = 1;
            _horizontalDirection = 1;
            _heightLimit = Global.Width / 2;
            if (HorizontalSpeed == VerticalSpeed) horizontalSpeed++;
            //if it enters the game window for the first time, it wont reverse its vertical direction
            _firstCrossing = _position.Y <= 0 ? true : false;
            _random = random;
        }
        public override void Update()
        {
            _position.Y += VerticalSpeed * _verticalDirection;
            _position.X += HorizontalSpeed * _horizontalDirection;
            RandomizeSpeed();
            UpdateHorizontalComponent();
            UpdateVerticalComponent();
        }   
        private void UpdateHorizontalComponent()
        {
            if (_position.X >= Global.Width - 5 || _position.X <= 5) 
                _horizontalDirection *= -1;
        }
        private void UpdateVerticalComponent()
        {
            if (_position.Y > _heightLimit && _verticalDirection != -1)
            {
                if (_firstCrossing) 
                    _firstCrossing = false;
                _verticalDirection *= -1;
            } else if (_position.Y <= 0 && !_firstCrossing && _verticalDirection != 1)
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
    public class StraightMovement : MovePattern
    {
        private Vector2D _pathVector;
        private int _targetAngle;
        public StraightMovement(int speed, int x, int y, int angle) : base(speed, speed,x , y)
        {
            _targetAngle = angle;
            _pathVector = SplashKit.VectorFromAngle(_targetAngle, 1);
            Console.WriteLine("angle is " + _targetAngle);
            Console.WriteLine("vector is " + _pathVector.X +" "+ _pathVector.Y);
        }
        public override void Update()
        {
            _position.X += _pathVector.X * VerticalSpeed;
            _position.Y += _pathVector.Y * VerticalSpeed;
        }
        public double GetAngle(int x)
        {
            var angle = _targetAngle - 90;
            return angle;
        }
    }
    public class ChargingMovement : MovePattern
    {
        private Vector2D _pathVector;
        private double _targetAngle;
        public ChargingMovement(int speed, int x, int y) : base(speed, speed,x , y)
        {
            _targetAngle = (SplashKit.Rnd(0, 42) + 69);
            _pathVector = SplashKit.VectorFromAngle(_targetAngle, 1);
            Console.WriteLine("angle is " + _targetAngle);
            Console.WriteLine("vector is " + _pathVector.X +" "+ _pathVector.Y);
        }
        public override void Update()
        {
            _position.X += _pathVector.X * VerticalSpeed;
            _position.Y += _pathVector.Y * VerticalSpeed;
        }
        public double GetAngle(int x)
        {
            var angle =  _targetAngle - 90;
            return angle;
        }
    }
}