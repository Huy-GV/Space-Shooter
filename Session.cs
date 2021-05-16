using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class Session 
    {
        public enum State
        {
            Running,
            Paused,
            Over
        }
        public Player Player{get; init;}
        public List<Explosion> Explosions{get; init;}
        public List<Bullet> PlayerProjectiles{get; init;}
        public List<Bullet> EnemyProjectiles{get; init;}
        public int GameModeIndex{get; init;}
        public List<Enemy> Enemies{ get => _gameMode.Enemies;}
        private Renderer _renderer;
        private EventProcessor _eventProcessor;
        private GameMode _gameMode;
        public State CurrentState{get; private set;}
        public Session(int spaceshipChoice, int gameModeIndex)
        {
            Player = new Player(spaceshipChoice);
            Explosions = new List<Explosion>();
            //expand into mini bombs?
            PlayerProjectiles = new List<Bullet>();
            EnemyProjectiles = new List<Bullet>();
            _gameMode = SetGameMode(GameModeIndex);
            _renderer = new Renderer(this);
            _eventProcessor = new EventProcessor(this, _gameMode);
            GameModeIndex = gameModeIndex;
            CurrentState = State.Running;
        }
        public void Continue() => CurrentState = State.Running;
        public void End() => CurrentState = State.Over;
        public void Pause() => CurrentState = State.Paused;
        private GameMode SetGameMode(int gameModeIndex)
        {
            switch(gameModeIndex)
            {
                case 7: return new SurvivalMode(); 
                case 6: return new BossRunMode(); 
                case 5: return new MineFieldMode(10);
                default: return new ByLevelMode(gameModeIndex);
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
        //TODO: delegate input processor into a class ???
        public void ProcessInput()
        {
            if (SplashKit.KeyDown(KeyCode.LeftKey) && Player.X > 0)   Player.MoveLeft();
            if (SplashKit.KeyDown(KeyCode.RightKey) && Player.X < Global.Width)  Player.MoveRight();
            if (SplashKit.KeyDown(KeyCode.UpKey) && Player.Y > Global.Height / 2)   Player.MoveUp();
            if (SplashKit.KeyDown(KeyCode.DownKey) && Player.Y < Global.Height)  Player.MoveDown();
            if (SplashKit.KeyDown(KeyCode.SpaceKey) && Player.CoolDownEnded) PlayerProjectiles.Add(Player.Shoot());
            if (SplashKit.KeyDown(KeyCode.EscapeKey)) CurrentState = State.Paused;
        }
    }
}