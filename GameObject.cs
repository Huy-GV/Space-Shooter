using System;
using SplashKitSDK;

namespace SpaceShooter
{
    public abstract class GameObject
    {
        protected Point2D _position;
        public int X {get => (int)_position.X;}
        public int Y {get => (int)_position.Y;}
        public GameObject()
        {
            _position = new Point2D();
        }
    }
}