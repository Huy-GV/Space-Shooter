using System.Collections.Generic;
using SplashKitSDK;
namespace Space_Shooter
{
    public abstract class GameObject
    {
        public int X{get;  protected set;}
        public int Y{get;  protected set;}
        //TODO: WRITE A separate class for drawable objects, including Draw, offsets, adjusted?
        protected double XOffset{get; set;}
        protected double YOffset{get; set;}
        public double AdjustedX => X - XOffset;
        public double AdjustedY => Y - YOffset; 
        public Bitmap Bitmap{get; protected set;}
        public abstract void Update();
        public virtual void Draw() => SplashKit.DrawBitmap(Bitmap, AdjustedX, AdjustedY);
    }
}


