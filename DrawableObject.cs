using System;
using SplashKitSDK;

namespace SpaceShooter
{
    public abstract class DrawableObject
    {
        protected Point2D _position;
        public Image Image{get; protected set;}
        public int X {get => (int)_position.X;}
        public int Y {get => (int)_position.Y;}
        public DrawableObject()
        {
            _position = new Point2D();
        }
    }
}