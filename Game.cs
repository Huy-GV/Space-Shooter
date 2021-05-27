using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public class Game
    {
        private State _currentState;
        private readonly int _defaultGameMode = 7;
        private readonly int _defaultSpaceship = 0;
        private int _spaceshipChoice, _gameMode;
        private Window gameWindow = new Window("Space Shooter", Global.Width, Global.Height);
        public State MainMenuState{ get; init;}
        public State PlayingState{ get; init;}
        public State GameModeState{ get; init;}
        public State GameOverState{ get; init;}
        public State PausedGameState{ get; init;}
        public int GameMode
        { 
            get{ return _gameMode;}
            set
            {
                if (value >= 1 && value <= 7) 
                    _gameMode = value;
                else 
                    _gameMode = _defaultGameMode;
            }
        }
        public int SpaceshipChoice
        { 
            get{ return _spaceshipChoice;}
            set
            {
                if (_spaceshipChoice < 2) 
                    _spaceshipChoice = value;
                else 
                    _spaceshipChoice = 0;
            }
        }
        public Game()
        {
            _gameMode = _defaultGameMode;
            _spaceshipChoice = _defaultSpaceship;
            MainMenuState = new MainMenu(this);
            PlayingState = new PlayingState(this);
            GameModeState = new GameModeState(this);
            GameOverState = new GameOverState(this);
            PausedGameState = new PausedGame(this);
            _currentState = MainMenuState;
        }
        public void SetState(State newState) 
        {
            // Console.WriteLine("state transition from: {0} to {1}", _currentState, newState);
            _currentState = newState;
        }
        public void Draw()
        {                
            Background.DrawBackground();
            _currentState.Draw();
            SplashKit.RefreshScreen(60);
        }
        public void ProcessInputs()
        {
            _currentState.ProcessInput();
        }
        public void Update()
        {
            SplashKit.ProcessEvents();
            Background.PlayMusic();
            _currentState.Update();
        }
    }
}