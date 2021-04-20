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
        private int _verticalLimit, _horizontalSpeed;
        public HorizontalMovement(int horizontalSpeed, int verticalSpeed, double x, double y) : base(horizontalSpeed, verticalSpeed, x ,y) 
        { 
            _verticalLimit = (Global.Width / 2) / (SplashKit.Rnd(4) + 1);
        }
        public override void Update()
        {
            if (_position.Y < _verticalLimit) _position.Y += VerticalSpeed;
            else
            {
                _position.X += _direction * _horizontalSpeed;
                if (_position.X >= Global.Width - 5 || _position.X <= 5) _direction *= -1;
            }
        }
    }
    public class VerticalMovement : MovePattern
    {
        public VerticalMovement(int verticalSpeed, double x, double y) : base(0, verticalSpeed, x, y) {}
        public override void Update()
        {
            _position.Y += VerticalSpeed;
        }
    }

    public class ZigzagMovement : MovePattern
    {
        private int _verticalLimit, _verticalDirection;
        private bool _firstCrossing;
        private bool _random;
        public ZigzagMovement(int horizontalSpeed, int verticalSpeed, double x, double y) : this(horizontalSpeed,  verticalSpeed, x ,y, false) {}
        public ZigzagMovement(int horizontalSpeed, int verticalSpeed, double x, double y, bool random): base(horizontalSpeed,  verticalSpeed, x, y)
        {
            _verticalDirection = 1;
            _verticalLimit = Global.Width / 2;
            _firstCrossing = true;
            _random = random;
        }
        public override void Update()
        {
            _position.Y += VerticalSpeed * _verticalDirection;
            _position.X += _direction * HorizontalSpeed;
            if(_random && SplashKit.Rnd(0,70) == 0) {
                HorizontalSpeed *= (SplashKit.Rnd(0,2) + 0.5);
                Console.WriteLine("new hori speed is " + HorizontalSpeed);
            }
            if (_position.X >= Global.Width - 5 || _position.X <= 5) _direction *= -1;
            if (_position.Y >= _verticalLimit)
            {
                if (_firstCrossing) _firstCrossing = false;
                _verticalDirection *= -1;
            } else if (_position.Y <= 0 && !_firstCrossing) _verticalDirection *= -1;
        }   
    }

    public class ChargingMovement : MovePattern
    {
        private Vector2D _pathVector;
        public ChargingMovement(int speed, int x, int y) : base(speed, speed,x , y)
        {
            var randomX = SplashKit.Rnd(0, 6);
            var targetX = (2 * randomX + 1) * 50; 
            var targetY = Global.Height - 10;

            _pathVector = new Vector2D();
            _pathVector.X = targetX - x;
            _pathVector.Y = targetY - y;
            _pathVector = SplashKit.UnitVector(_pathVector);
        }
        public override void Update()
        {
            _position.X += _pathVector.X * VerticalSpeed;
            _position.Y += _pathVector.Y * VerticalSpeed;
        }
        public double GetAngle(int x)
        {
            var yAxis = new Vector2D();
            yAxis.X = 0;
            yAxis.Y = -1;
            var angle = SplashKit.AngleBetween(yAxis, _pathVector) - 90;
            return angle;
        }
    }

    //TODO: make a more randomized zig zag, may be a bool field as a flag?
}