using System;
using SplashKitSDK;

namespace SpaceShooter
{
    public abstract class DrawableObject
    {
        protected Position _position;
        public Image Image{get; protected set;}
        protected int Angle{get; set;} = 90;
        public int X {get => (int)_position.X;}
        public int Y {get => (int)_position.Y;}
        public virtual void Draw() => Image.Draw(X, Y);  
    }
}