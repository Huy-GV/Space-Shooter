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
        private Dictionary<Type, int> _enemyAmountByClass;
        public GameStates Status{get; private set;}
        public SoloGame(int level , int spaceshipChoice, Dictionary<Type, int> enemyAmountByClass)
        {
            _player = new Player(spaceshipChoice);
            _enemies = new List<Enemy>();
            _enemyAmountByClass = enemyAmountByClass;
            _level = level;
            Difficulty.SetEnemyLimitByLevel(level);
            Status = GameStates.PlayerAlive;
        }
        public void Update()
        {
            Background.PlayMusic(); 
            Background.UpdateExplosions();
            HandleKeyboardInputs();
            UpdatePlayer();
            UpdateEnemies();
            UpdateLevel(_level);
            UpdateDifficulty(_player.Score);
        }
        private void UpdatePlayer()
        {
            _player.Update();
            if (_player.Health <= 0)
            {
                _enemies.Clear();
                Status = GameStates.PlayerDestroyed;
            } 
            
        }
        private void UpdateDifficulty(double score)
        {
            if (score >= 100 && Difficulty.Index == 0) Difficulty.IncreaseStage();
            if (score >= 200 && Difficulty.Index == 1) Difficulty.IncreaseStage();
            if (score >= 400 && Difficulty.Index == 2) Difficulty.IncreaseStage();
        }
        private void UpdateLevel(int level)
        {
            if (level >= 1 && level <= 6 && _player.Score >= 100 && _enemies.Count == 0) AddBoss(level);
            //if the boss is destroyed and there is no enemy left

            //change state to either over or in game, if the player wins game data will update the record
            if (_enemies.Count == 0) Status = SoloGame.GameStates.LevelCompleted;
            else if (level == 7) Difficulty.IncreaseStage(); 
        }
        private void AddBoss(int level)
        {
            switch(level)
            {
                case 7:
                    //_enemies.Add(boss);
                    break;
                case 8:
                    break;
                case 9:
                    break;
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
        private void DrawEnemies() {foreach(Enemy enemy in _enemies.ToArray()) enemy.Draw();}
        private void AddEnemies()
        {
            int enemyTypeAmount;
            foreach (var enemyType in _enemyAmountByClass.Keys)
            {
                enemyTypeAmount = _enemyAmountByClass[enemyType];
                //generate enemies, their amount and spawning location is dependent on the currentamount    and closest enemy created
                if (SplashKit.Rnd(0, 70) == 0 && enemyTypeAmount < Difficulty.Limit[enemyType])
                {
                    if (enemyTypeAmount == 0) _enemies.Add((Enemy)Activator.CreateInstance(enemyType));
                    else
                    {
                        var lastEnemy = _enemies[_enemies.Count - 1];
                        Object[] parameters = {lastEnemy.X, lastEnemy.Y};
                        _enemies.Add((Enemy)Activator.CreateInstance(enemyType, parameters));
                    }
                    UpdateEnemyAmount(enemyType, 1);
                }
            }
        }
        private void UpdateEnemyAmount(Type type, int increment) => _enemyAmountByClass[type] += increment;
        private void UpdateEnemies()
        {
            if (_player.Score < 100) AddEnemies();
            foreach(Enemy enemy in _enemies.ToArray())
            {
                enemy.Update();
                enemy.CheckPlayerBullets(_player.Bullets);
                if (enemy.AdjustedY > Global.Height || enemy.IsDestroyed)
                {
                    if (enemy.IsDestroyed) Background.CreateExplosion(enemy.X, enemy.Y, enemy.ExplosionType);
                    UpdateEnemyAmount(enemy.GetType(), -1);
                    _enemies.Remove(enemy);                     
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