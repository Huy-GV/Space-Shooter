using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Session 
    {

        public Player Player{get; init;}
        public List<Explosion> Explosions{get; init;}
        public int GameModeIndex{get; init;}
        public List<Enemy> Enemies{ get => _gameMode.Enemies;}
        private Renderer _renderer;
        private EventProcessor _eventProcessor;
        public enum Status
        {
            Running,
            Paused,
            Over
        }
        private GameMode _gameMode;
        public Status CurrentStatus{get; private set;}
        public Session(int spaceshipChoice, int gameModeIndex)
        {
            Player = new Player(spaceshipChoice);
            Explosions = new List<Explosion>();
            _renderer = new Renderer(this);
            _eventProcessor = new EventProcessor(this, _gameMode);
            GameModeIndex = gameModeIndex;
            SetGameMode(GameModeIndex);
            CurrentStatus = Status.Running;
            
        }
        public void Continue() => CurrentStatus = Status.Running;
        public void End() => CurrentStatus = Status.Over;
        private void SetGameMode(int gameModeIndex)
        {
            switch(gameModeIndex)
            {
                case 7: _gameMode = new SurvivalMode(); break;
                case 6: _gameMode = new BossRunMode(); break;
                case 5: _gameMode = new MineFieldMode(10); break;
                default: _gameMode = new ByLevelMode(gameModeIndex); break;
            }
        }
        public void Update()
        {
            _eventProcessor.Update();
        }
        public void Draw()
        {
            _renderer.Draw();
        }
        public void ProcessInput()
        {
            if (SplashKit.KeyDown(KeyCode.LeftKey) && Player.X > 0)   Player.MoveLeft();
            if (SplashKit.KeyDown(KeyCode.RightKey) && Player.X < Global.Width)  Player.MoveRight();
            if (SplashKit.KeyDown(KeyCode.UpKey) && Player.Y > Global.Height / 2)   Player.MoveUp();
            if (SplashKit.KeyDown(KeyCode.DownKey) && Player.Y < Global.Height)  Player.MoveDown();
            if (SplashKit.KeyDown(KeyCode.SpaceKey) && Player.CoolDown == 0) Player.Shoot();
            if (SplashKit.KeyDown(KeyCode.EscapeKey)) CurrentStatus = Status.Paused;
        }
    }
}