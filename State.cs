using SplashKitSDK;
using System;

namespace Space_Shooter
{
    public abstract class State
    {
        protected int option;
        public abstract void Draw();
        public abstract void ProcessInput();
        public abstract void Update();
    }
    public class MainMenu : State
    {
        public override void Draw()
        {
            SplashKit.DrawText("SPACE SHOOTER", Color.Yellow, Global.BigFont, 60, 100, 50);
            SplashKit.DrawText("Play", Color.White, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Choose level" , Color.Orange, Global.MediumFont, 40, 150, 300);
            SplashKit.DrawText("Change Space Ship", Color.Blue, Global.MediumFont, 40, 150, 400);
        }
        public override void ProcessInput()
        {
            
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}