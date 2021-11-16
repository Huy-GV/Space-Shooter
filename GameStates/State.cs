using SplashKitSDK;
using System;
using Drawable;
using Main;

namespace GameStates
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
                    case 2:
                    {
                        PlayingState.ContinueSession();
                        _game.SetState(_game.PlayingState); break;
                    } 
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