using SplashKitSDK;
using Main;
using System;

namespace GameStates
{
    public class MainMenu : State
    {
        public MainMenu(Game game) : base(game){}
        public override void Draw()
        {
            SplashKit.DrawText("SPACE SHOOTER", Color.Yellow, Global.BigFont, 60, 100, 50);
            SplashKit.DrawText("Play", Color.White, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Choose level" , Color.Orange, Global.MediumFont, 40, 150, 300);
            SplashKit.DrawText("Change Space Ship", Color.Blue, Global.MediumFont, 40, 150, 400);
            DrawPlayerOption(_game.SpaceshipChoice);
        }
        private void DrawPlayerOption(int option)
        {
            Bitmap bitmap;
            switch(option)
            {
                case 1: 
                    bitmap = SplashKit.LoadBitmap("option2", "options/option2.png");
                    break;
                case 2:
                    bitmap = SplashKit.LoadBitmap("option3", "options/option3.png");
                    break;
                default:
                    bitmap = SplashKit.LoadBitmap("option1", "options/option1.png");
                    break;
            }
            bitmap.Draw(150, 500);
        }
        public override void ProcessInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                option = (int)Math.Floor((SplashKit.MouseY() / 100));
                switch(option)
                {
                    case 2:
                        _game.SetState(_game.PlayingState);
                        break;
                    case 3:
                        _game.SetState(_game.GameModeState);
                        break;
                    case 4:
                        _game.SpaceshipChoice++;
                        break;
                    default: break;
                }
            }

        }
    }
}