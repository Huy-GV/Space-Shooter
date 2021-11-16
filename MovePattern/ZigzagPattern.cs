using SplashKitSDK;
using Interface;
using Drawable;
using Main;

namespace Enemies{
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
}