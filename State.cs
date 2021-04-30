using SplashKitSDK;
using System;

namespace Space_Shooter
{
    public abstract class State
    {
        protected int option;
        protected Game _game;
        public abstract void Draw();
        public abstract void ProcessInput();
        public virtual void Update(){ return;}
        public State(Game game)
        {
            _game = game;
        }
    }
    public class PausedGame : State
    {
        public PausedGame(Game game) : base(game){}
        public override void Draw()
        {
            SplashKit.DrawText("Game Paused", Color.Yellow, Global.BigFont, 60, 150, 50);
            SplashKit.DrawText("Resume", Color.Green, Global.MediumFont, 40, 150, 200);
            SplashKit.DrawText("Toggle music", Color.Blue, Global.MediumFont, 40, 150, 300);
            SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 400);
        }
        public override void ProcessInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                option = (int)Math.Floor((SplashKit.MouseY() / 100));
                switch(option)
                {
                    case 2: _game.SetState(_game.PlayingState); break;
                    case 3: Background.ToggleMusic(); break;
                    case 4: 
                    {
                        PlayingState.DeleteSession();
                        _game.SetState(_game.MainMenuState); break;
                    }
                    default: break;
                } 
            }
        }
    }
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
    public class PlayingState : State
    {
        private Session _session;
        private static bool _sessionStarted;
        public PlayingState(Game game) : base(game)
        {
            _session = null;
            _sessionStarted = false;
        }
        public override void Draw()
        {
            if (_sessionStarted)
                _session.Draw();
        }
        public override void ProcessInput()
        {
            if (_sessionStarted)
                _session.ProcessInput();
        }
        public override void Update()
        {
            if (!_sessionStarted)
            {
                _session = new Session(_game.SpaceshipChoice, _game.GameMode);
                _sessionStarted = true;
            } else
            {
                _session.Update();
                if (_session.CurrentStatus == Session.Status.Over)
                {
                    _game.SetState(_game.GameOverState);
                    _sessionStarted = false;
                } else if (_session.CurrentStatus == Session.Status.Paused)
                    _game.SetState(_game.PausedGameState);
            }             
        }
        public static void DeleteSession()
        {
            _sessionStarted = false;
        }
    }
    public class GameModeState : State
    {
        public GameModeState(Game game) : base(game){}
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
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                option = (int)Math.Floor((SplashKit.MouseY() / 100));
                _game.GameMode = option;
                _game.SetState(_game.MainMenuState);
            }
        }
    }
    public class GameOverState : State
    {
        public GameOverState(Game game) : base(game){}
        public override void Draw()
        {
            SplashKit.DrawText("Game Over", Color.Yellow, Global.BigFont, 60, 150, 50);
            SplashKit.DrawText("Quit to Menu", Color.Red, Global.MediumFont, 40, 150, 200);
        }
        public override void ProcessInput()
        {
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                option = (int)Math.Floor((SplashKit.MouseY() / 100));
                if (option == 2)
                    _game.SetState(_game.MainMenuState);
            }
        }
    }
}