using System;
using SplashKitSDK;
using Interface;
using Drawable;
using Main;


namespace MovePattern
{
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
}

