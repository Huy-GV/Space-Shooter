using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace Space_Shooter
{
    public class Game
    {
        private State _currentState, _mainMenuState, _playingState, _gameModeState;
        private readonly int _defaultLevel = 7;
        private Window gameWindow = new Window("Space Shooter", Global.Width, Global.Height);
        public State MainMenuState{ get{ return _mainMenuState;}}
        public State PlayingState{ get{ return _playingState;}}
        public State GameModeState{ get{ return _gameModeState;}}
        private int _spaceshipChoice, _gameMode;
        public int GameMode
        { 
            get{ return _gameMode;}
            set
            {
                if (value >= 1 && value <= 7) 
                    _gameMode = value;
                else 
                    _gameMode = 7;
            }
        }
        public int SpaceshipChoice
        { 
            get{ return _gameMode;}
            set
            {
                if (_spaceshipChoice >= 0 && _spaceshipChoice <= 2) 
                    _spaceshipChoice = value;
                else 
                    _spaceshipChoice = 0;
            }
        }
        public Game()
        {
            _gameMode = 7;
            _spaceshipChoice = 0;
            _mainMenuState = new MainMenu(this);
            _playingState = new PlayingState(this);
            _gameModeState = new GameModeState(this);
        }
        public void SetState(State newState) => _currentState = newState;
        // public void SetState(State newState, int level)
        // {
        //     _currentState = newState;
        //     _level = level;
        // } 
        // public void SetState(State newState, int level, int spaceshipChoice)
        // {
        //     _currentState = newState;
        //     _spaceshipChoice = spaceshipChoice;
        //     _level = level;
        // } 
        public void Draw()
        {                
            Background.DrawBackground();
            _currentState.Update();
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
        private void UpdatePlayerChoice(){ _spaceshipChoice = _spaceshipChoice == 2 ? 0 :_spaceshipChoice + 1; }
    }
}