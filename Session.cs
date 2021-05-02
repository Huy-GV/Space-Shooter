using System;
using SplashKitSDK;
using System.Collections.Generic;

namespace Space_Shooter
{
    public class Session 
    {
        public enum Status
        {
            Running,
            Paused,
            Over
        }
        private static List<Explosion> _explosions = new List<Explosion>();
        private int _gameModeIndex;
        private Player _player;
        private GameMode _gameMode;
        public Status CurrentStatus{get; private set;}
        public Session(int spaceshipChoice, int gameModeIndex)
        {
            _player = new Player(spaceshipChoice);
            _gameModeIndex = gameModeIndex;
            SetGameMode();
            CurrentStatus = Status.Running;
        }
        private void SetGameMode()
        {
            switch(_gameModeIndex)
            {
                case 7: _gameMode = new SurvivalMode(); break;
                case 6: _gameMode = new BossRunMode(); break;
                case 5: _gameMode = new MineFieldMode(10); break;
                default: _gameMode = new ByLevelMode(_gameModeIndex); break;
            }
        }
        public void Update()
        {
            UpdateExplosions();
            UpdatePlayer();
            UpdateEnemies();
        }
        private void UpdatePlayer()
        {
            _player.Update();
            if ((_player.Score < 100 && _gameModeIndex < 7) || _gameModeIndex >= 7)
                _player.GainScore();
            if (_gameMode.GameOver) 
                CurrentStatus = Status.Over;
        }
        public void Draw()
        {
            DrawExplosions();
            DrawEnemies();
            DrawPlayerInfo();
            _player.Draw();
        }
        public void Continue() => CurrentStatus = Status.Running;
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
            if (SplashKit.KeyDown(KeyCode.EscapeKey)) CurrentStatus = Status.Paused;
        }
        private void DrawEnemies() => _gameMode.Enemies.ForEach(enemy => enemy.Draw());
        private void DrawExplosions() => _explosions.ForEach(explosion => explosion.Draw());
        private void UpdateEnemies()
        {
            _gameMode.CheckGameEnding(_player);
            _gameMode.AddEnemies((int)_player.Score);
            foreach(Enemy enemy in _gameMode.Enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_player.Bullets);
                CheckEnemyStatus(enemy);
                if (_player.CollideWith(enemy.Image, enemy.X, enemy.Y) && !(enemy is Boss))
                { 
                    _player.LoseHealth(enemy.CollisionDamage);
                    enemy.LoseHealth(100);
                }
                if (enemy is IHaveGun) 
                    _player.CheckEnemyBullets(((IHaveGun)enemy).Bullets);   
            }
        }
        private void CheckEnemyStatus(Enemy enemy)
        {
            if (enemy.Y > Global.Height || enemy.Health <= 0)
            {
                if (enemy.Health <= 0) 
                    CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType); 
                _gameMode.RemoveEnemy(enemy);            
            }
        }
        private void CreateExplosion(int x, int y, Explosion.Type type) => _explosions.Add(new Explosion(x, y, type));
        private void UpdateExplosions()
        {
            foreach (var explosion in _explosions.ToArray())
            {
                explosion.Update();
                if (explosion.AnimationEnded) _explosions.Remove(explosion);
            }
        }
    }
}