using System.Collections.Generic;
using SplashKitSDK;
namespace Space_Shooter
{
    public abstract class GameObject
    {
        public int X{get;  protected set;}
        public int Y{get;  protected set;}
        // public Bitmap Bitmap{get; protected set;}
        public abstract void Update();
        // public virtual void Draw() => SplashKit.DrawBitmap(Bitmap, X - Bitmap.CellCenter.X,  Y - Bitmap.CellCenter.Y);

        public abstract void Draw();
    }
}


