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
        public List<Explosion> Explosions{get; init;} = new List<Explosion>();
        public List<Bullet> PlayerProjectiles{get; init;} = new List<Bullet>();
        public List<Bullet> EnemyProjectiles{get; init;} = new List<Bullet>();
        public IEnumerable<Enemy> Enemies{ get => _gameMode.Enemies;}
        private Renderer _renderer;
        private LogicHandler _logicHandler;
        public EnemyManager EnemyManager { get; init;}
        private GameMode _gameMode;
        public State CurrentState{get; private set;} = State.Running;
        public Session(int spaceshipChoice, int gameModeIndex)
        {
            Player = new Player(spaceshipChoice);
            _gameMode = SetGameMode(gameModeIndex);
            _renderer = new Renderer(this);
            _logicHandler = new LogicHandler(this, _gameMode);
            EnemyManager = new EnemyManager(EnemyProjectiles);
        }
        public void Continue() => CurrentState = State.Running;
        public void End() => CurrentState = State.Over;
        private GameMode SetGameMode(int gameModeIndex)
        {
            switch(gameModeIndex)
            {
                case int n when (n >= 1 && n <= 4): return new ByLevelMode(gameModeIndex);
                case 7: return new SurvivalMode(); 
                case 6: return new BossRunMode(); 
                case 5: return new MineFieldMode(10);
                default: throw new IndexOutOfRangeException($"Game mode for index {gameModeIndex} does not exist");
            }
        }
        public void Update()
        {
            _logicHandler.Update();
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
            if (SplashKit.KeyDown(KeyCode.SpaceKey) && Player.CoolDownEnded) PlayerProjectiles.Add(Player.Shoot());
            if (SplashKit.KeyDown(KeyCode.EscapeKey)) CurrentState = State.Paused;
            if (SplashKit.KeyDown(KeyCode.FKey)) Player.SwapGun();
        }
    }                                                                   
}