using SplashKitSDK;
using System;
using System.Collections.Generic;

namespace SpaceShooter
{
    public interface IMoveStrategy
    {
        Position Move(Position position);
    }
    public class HorizontalPattern : IMoveStrategy
    {
        private double _speed;
        private int _direction;
        public HorizontalPattern(int speed)
        { 
            _direction = SplashKit.Rnd(2) * 2 - 1;
            _speed = speed;
        }
        public Position Move(Position currentPosition)
        {
            currentPosition.X += (int)(_direction * _speed);
            if (currentPosition.X >= Global.Width - 5 || currentPosition.X <= 5) _direction *= -1;
            return currentPosition;
        }
    }
    public class ZigzagPattern : IMoveStrategy
    {
        private int _heightLimit, _verticalDirection, _horizontalDirection;
        private double _horizontalSpeed, _verticalSpeed;
        private bool _firstCrossing;
        private bool _random;
        public ZigzagPattern(int horizontalSpeed, int verticalSpeed, double y) : this(horizontalSpeed, verticalSpeed ,y, false) {}
        public ZigzagPattern(int horizontalSpeed, int verticalSpeed, double y, bool random)
        {
            _verticalDirection = 1;
            _horizontalDirection = 1;
            _heightLimit = Global.Width / 2;
            _horizontalSpeed = horizontalSpeed;
            _verticalSpeed = verticalSpeed;
            if (_horizontalSpeed == _verticalSpeed) _horizontalSpeed++;
            //if it enters the game window for the first time, it wont reverse its vertical direction
            _firstCrossing = y <= 0 ? true : false;
            _random = random;
        }
        public Position Move(Position currentPosition)
        {
            currentPosition.Y += (int) (_verticalSpeed * _verticalDirection);
            currentPosition.X += (int) (_horizontalSpeed * _horizontalDirection);
            RandomizeSpeed();
            UpdateHorizontalComponent(currentPosition.X);
            UpdateVerticalComponent(currentPosition.Y);
            return currentPosition;
        }   
        private void UpdateHorizontalComponent(int x)
        {
            if (x >= Global.Width - 5 || x <= 5) 
                _horizontalDirection *= -1;
        }
        private void UpdateVerticalComponent(int y)
        {
            if (y > _heightLimit && _verticalDirection != -1)
            {
                if (_firstCrossing) 
                    _firstCrossing = false;
                _verticalDirection *= -1;
            } else if (y <= 0 && !_firstCrossing && _verticalDirection != 1)
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
    public class StraightLinePattern : IMoveStrategy
    {
        private double _speed;
        private Vector2D _pathVector;
        private int _targetAngle;
        public StraightLinePattern(int speed, int angle)
        {
            _targetAngle = angle;
            _speed = speed;
            _pathVector = SplashKit.VectorFromAngle(_targetAngle, 1);
            // Console.WriteLine("angle is " + _targetAngle);
            // Console.WriteLine("vector is " + _pathVector.X +" "+ _pathVector.Y);
        }
        public StraightLinePattern(int speed) :  this(speed, 90){}
        public Position Move(Position currentPosition)
        {
            currentPosition.X += (int)(_pathVector.X * _speed);
            currentPosition.Y += (int)(_pathVector.Y * _speed);
            return currentPosition;
        }
    }
}