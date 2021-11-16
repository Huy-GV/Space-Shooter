using SplashKitSDK;
using Interface;
using Drawable;
using MovePattern;

namespace MovePattern 
{
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