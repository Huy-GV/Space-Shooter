using SplashKitSDK;
using System;

namespace Space_Shooter
{
    public abstract class State
    {
        //TODO: write a game class with a state field
        protected int option;
        protected Game _game;
        public abstract void Draw();
        public virtual void ProcessInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
                option = (int)Math.Floor((SplashKit.MouseY() / 100));
        }
        public abstract void Update();
        public State(Game game)
        {
            _game = game;
        }
    }
    public class MainMenu : State
    {
        private int _spaceshipChoice = 0;
        public MainMenu(Game game) : base(game)
        {

        }
        public override void Draw()
        {
            SplashKit.DrawText("SPACE SHOOTER", Color.Yellow, Global.BigFont, 60, 100, 50);
            SplashKit.DrawText("Play", Color.White, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Choose level" , Color.Orange, Global.MediumFont, 40, 150, 300);
            SplashKit.DrawText("Change Space Ship", Color.Blue, Global.MediumFont, 40, 150, 400);
            DrawPlayerOption(_spaceshipChoice);
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
            base.ProcessInput();
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
        public override void Update()
        {
            return;
        }
    }
    public class PlayingState : State
    {
        private Session _session;

        public PlayingState(Game game) : base(game)
        {
            _session = null;
        }
        public override void Draw()
        {
            throw new NotImplementedException();
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
    public class GameModeState : State
    {
        public GameModeState(Game game) : base(game)
        {

        }
        public override void Draw()
        {
            for (int i = 1; i <= 4; i++)
                SplashKit.DrawText("Level " + i, Color.White, Global.MediumFont, 40, 150, 100 * i);
            SplashKit.DrawText("Endless Mine Field", Color.Orange, Global.MediumFont, 40, 150, 500);
            SplashKit.DrawText("Boss Run", Color.Orange, Global.MediumFont, 40, 150, 600);
            SplashKit.DrawText("Survival", Color.Red, Global.MediumFont, 40, 150, 700);
        }
        public override void Update()
        {
            return;
        }
        public override void ProcessInput()
        {
            base.ProcessInput();
            _game.GameMode = option;
        }
    }
}