using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class SoloGame
    {
        public enum GameStates
        {
            LevelCompleted,
            PlayerDestroyed,
            PlayerAlive
        }
        private int _level;
        private Player _player;
        private List<Enemy> _enemies;
        private GameMode _mode;
        public GameStates State{get; private set;}
        public SoloGame(int level , int spaceshipChoice)
        {
            _player = new Player(spaceshipChoice);
            _enemies = new List<Enemy>();
            _level = level;
            SetGameMode();
            // Difficulty.SetEnemyLimitByLevel(level);
            State = GameStates.PlayerAlive;
            Console.WriteLine("level is {0}", _level);
        }
        private void SetGameMode()
        {
            if (_level == 7) _mode = new SurvivalMode();
            else _mode = new ByLevelMode(_level);
            switch(_level)
            {
                case 7: _mode = new SurvivalMode(); break;
                case 6: _mode = new MineFieldMode(); break;
                case 5: _mode = new BossRunMode(); break;
                default: _mode = new ByLevelMode(_level); break;
            }
        }
        public void Update()
        {
            Background.UpdateExplosions();
            HandleKeyboardInputs();
            UpdatePlayer();
            UpdateEnemies();
            Console.WriteLine(_player.Health);
        }
        private void UpdatePlayer()
        {
            _player.Update();
            if (_player.Score < 100 && _level < 7) _player.GainScore();
            else if (_level >= 7) _player.GainScore();
            if (_player.Health <= 0)
            {
                _enemies.Clear();
                State = GameStates.PlayerDestroyed;
            } 

        }
        public void Draw()
        {
            Menu.DrawGameInfo(_player.Health, _player.Score);
            Background.DrawExplosions();
            DrawEnemies();
            _player.Draw();
        }
        private void HandleKeyboardInputs()
        {
            if (SplashKit.KeyDown(KeyCode.LeftKey) && _player.X > 0)   _player.MoveLeft();
            if (SplashKit.KeyDown(KeyCode.RightKey) && _player.X < Global.Width)  _player.MoveRight();
            if (SplashKit.KeyDown(KeyCode.UpKey) && _player.Y > Global.Height / 2)   _player.MoveUp();
            if (SplashKit.KeyDown(KeyCode.DownKey) && _player.Y < Global.Height)  _player.MoveDown();
            if (SplashKit.KeyDown(KeyCode.SpaceKey) && _player.CoolDown == 0) _player.Shoot();
        }
        private void DrawEnemies() => _enemies.ForEach(enemy => enemy.Draw());
        private void UpdateEnemies()
        {
            _mode.AddEnemies((int)_player.Score, _enemies);
            foreach(Enemy enemy in _enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_player.Bullets);
                if (enemy.Y > Global.Height || enemy.IsDestroyed)
                {
                    if (enemy.IsDestroyed) Background.CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType);
                    _enemies.Remove(enemy);
                    _mode.UpdateEnemyAmount(enemy.GetType(), -1);                   
                }
                if (_player.CollideWith(enemy))
                { 
                    enemy.GetDestroyed();
                    _player.LoseHealth(enemy.Damage);
                }
                if (enemy is IHaveGun) _player.CheckEnemyBullets(((IHaveGun)enemy).Bullets);   
            }
        }
    }
}