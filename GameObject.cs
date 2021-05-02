using System.Collections.Generic;
using SplashKitSDK;
namespace Space_Shooter
{
    public abstract class GameObject
    {
        public int X{get;  protected set;}
        public int Y{get;  protected set;}
        public abstract void Update();
        public abstract void Draw();
    }
}


