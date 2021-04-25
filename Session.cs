using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class Session 
    {
        public enum States
        {
            LevelCompleted,
            PlayerAlive,
            PlayerDefeated
        }
        private int _level;
        private Player _player;
        private List<Enemy> _enemies;
        private GameMode _gameMode;
        public States Status{get; private set;}
        public Session(int spaceshipChoice, int level)
        {
            _player = new Player(spaceshipChoice);
            _enemies = new List<Enemy>();
            _level = level;
            SetGameMode();
            Status = States.PlayerAlive;
        }
        private void SetGameMode()
        {
            switch(_level)
            {
                case 7: _gameMode = new SurvivalMode(); break;
                case 6: _gameMode = new BossRunMode(); break;
                case 5: _gameMode = new MineFieldMode(10); break;
                default: _gameMode = new ByLevelMode(_level); break;
            }
        }
        public void Update()
        {
            Background.UpdateExplosions();
            UpdatePlayer();
            UpdateEnemies();
        }
        private void UpdatePlayer()
        {
            _player.Update();
            if (_player.Score < 100 && _level < 7)
                _player.GainScore();
            else if (_level >= 7) 
                _player.GainScore();
            if (_gameMode.GameOver)
                Status = (_player.Health <= 0) ? States.PlayerDefeated : States.LevelCompleted;
        }
        public void Draw()
        {
            Background.DrawExplosions();
            DrawEnemies();
            DrawPlayerInfo();
            _player.Draw();
        }
        private void DrawPlayerInfo()
        {
            SplashKit.DrawText($"Health: {(int)_player.Health}", Color.Green, Global.SmallFont, 24, 20, 40);
            SplashKit.DrawText($"Score: {(int)_player.Score}", Color.Yellow, Global.SmallFont, 24, 20, 70);
        }
        public void ProcessInput()
        {
            if (SplashKit.KeyDown(KeyCode.LeftKey) && _player.X > 0)   _player.MoveLeft();
            if (SplashKit.KeyDown(KeyCode.RightKey) && _player.X < Global.Width)  _player.MoveRight();
            if (SplashKit.KeyDown(KeyCode.UpKey) && _player.Y > Global.Height / 2)   _player.MoveUp();
            if (SplashKit.KeyDown(KeyCode.DownKey) && _player.Y < Global.Height)  _player.MoveDown();
            if (SplashKit.KeyDown(KeyCode.SpaceKey) && _player.CoolDown == 0) _player.Shoot();
            if (SplashKit.KeyDown(KeyCode.EscapeKey)) Status = States.PlayerDefeated;
        }
        private void DrawEnemies() => _enemies.ForEach(enemy => enemy.Draw());
        private void UpdateEnemies()
        {
            _gameMode.CheckGameEnding(_player, _enemies);
            _gameMode.AddEnemies((int)_player.Score, _enemies);
            foreach(Enemy enemy in _enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_player.Bullets);
                CheckEnemyStatus(enemy);
                if (_player.CollideWith(enemy))
                { 
                    _player.LoseHealth(enemy.CollisionDamage);
                    if (!(enemy is Boss))
                        enemy.GetDestroyed();
                }
                if (enemy is IHaveGun) 
                    _player.CheckEnemyBullets(((IHaveGun)enemy).Bullets);   
            }
        }
        private void CheckEnemyStatus(Enemy enemy)
        {
            if (enemy.Y > Global.Height || enemy.IsDestroyed)
            {
                if (enemy.IsDestroyed) 
                    Background.CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType);
                _enemies.Remove(enemy);
                _gameMode.UpdateEnemyAmount(enemy.GetType(), -1);               
            }
        }
    }
}