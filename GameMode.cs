using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace SpaceShooter
{
    public abstract class GameMode
    {
        protected Dictionary<Type, int> _limits;
        public int SpawnRate{get; protected set;}
        public bool GameEnded{get; protected set;}
        public List<Enemy> Enemies{get; protected init;}
        protected Dictionary<Type, int> _enemyAmountByType;
        public GameMode()
        {
            _limits = new Dictionary<Type, int>()
            {
                {typeof(BlueAlienship), 0},
                {typeof(PurpleAlienship), 0},
                {typeof(RedAlienship), 0},
                {typeof(KamikazeAlien), 0},
                {typeof(Asteroid), 0},
                {typeof(Spacemine), 0},
                {typeof(Nightmare), 0},
                {typeof(Phantom), 0}
            };
            _enemyAmountByType = new Dictionary<Type, int>()
            {
                {typeof(BlueAlienship), 0},
                {typeof(PurpleAlienship), 0},
                {typeof(RedAlienship), 0},
                {typeof(KamikazeAlien), 0},
                {typeof(Asteroid), 0},
                {typeof(Spacemine), 0},
                {typeof(Nightmare), 1},
                {typeof(Phantom), 1}
            };
            Enemies = new List<Enemy>();
            SpawnRate = 70;
            GameEnded = false;
        }
        protected bool TimeToSpawn()=> SplashKit.Rnd(0, SpawnRate) == 0;
        public virtual void AddEnemies(int score)
        {
            foreach (var enemyType in _enemyAmountByType.Keys)
            {
                object[] parameters;
                if (TimeToSpawn() && _enemyAmountByType[enemyType] < _limits[enemyType])
                {
                    if (_enemyAmountByType[enemyType] == 0)
                    {
                        parameters = new object[]{null, null};
                    } else
                    {
                        var lastEnemy = Enemies[Enemies.Count - 1];
                        parameters = new object[]{lastEnemy.X, lastEnemy.Y}; 
                    }
                    Enemies.Add((Enemy)Activator.CreateInstance(enemyType, parameters));
                    UpdateEnemyAmount(enemyType, 1);
                }
            }
        }
        public virtual void CheckGameEnding(Player player)
        {
            if (player.Health <= 0) GameEnded = true;
        }
        public void RemoveEnemy(Enemy enemy)
        {
            UpdateEnemyAmount(enemy.GetType(), -1);
            Enemies.Remove(enemy);
        }
        private void UpdateEnemyAmount(Type type, int increment)=> _enemyAmountByType[type] += increment;
    }
    public class ByLevelMode : GameMode
    {
        private int _level;
        private bool _bossSpawned;
        public ByLevelMode(int level) : base()
        {
            _level = level;
            _bossSpawned = false;
            SetEnemyLimitsByLevel();
        }
        public override void AddEnemies(int score)
        {
            if (score < 80 ) base.AddEnemies(score);
            else if (!_bossSpawned)
            {
                Enemies.Add(SpawnBoss());
                _bossSpawned = true;
            }
        }
        public override void CheckGameEnding(Player player)
        {
            base.CheckGameEnding(player);
            if (Enemies.Count == 0 && ((_level < 3 && player.Score >= 100) || (_level >= 3 && _bossSpawned)))
                GameEnded = true;
        }
        private Boss SpawnBoss()
        {
            switch(_level)
            {
                case 3: return new Nightmare(); 
                case 4: return new Phantom(); 
                default: return new Nightmare();
            }
        }
        private void SetEnemyLimitsByLevel()
        {
            if (_level < 1) _level = 1;
            if (_level > 7) _level = 7;
            switch(_level)
            {
                case 1:
                    _limits[typeof(BlueAlienship)] = 5;
                    _limits[typeof(RedAlienship)] = 2;
                    _limits[typeof(Asteroid)] = 5;
                    _limits[typeof(Spacemine)] = 1;
                    break;
                case 2:
                    _limits[typeof(PurpleAlienship)] = 6;
                    _limits[typeof(Asteroid)] = 5;
                    _limits[typeof(Spacemine)] = 2;
                    break;
                case 3:
                    _limits[typeof(BlueAlienship)] = 3;
                    _limits[typeof(RedAlienship)] = 5;
                    _limits[typeof(Asteroid)] = 4;
                    _limits[typeof(Spacemine)] = 2;
                    _limits[typeof(Nightmare)] = 1;
                    break;
                case 4:
                    _limits[typeof(BlueAlienship)] = 3;
                    _limits[typeof(PurpleAlienship)] = 2;
                    _limits[typeof(RedAlienship)] = 4;
                    _limits[typeof(KamikazeAlien)] = 3;
                    _limits[typeof(Asteroid)] = 5;
                    _limits[typeof(Spacemine)] = 3; 
                    _limits[typeof(Phantom)] = 1;
                    break;
            }
        }
    }
    public class SurvivalMode : GameMode
    {
        private int _stage;
        private readonly int _firstStageThreshold = 10;
        private readonly int _secondStageThreshold = 20;
        public SurvivalMode() : base()
        {
            _stage = 0;
            SetEnemyLimitsByStage();
        }
        private void SetEnemyLimitsByStage()
        {
            switch(_stage)
            {
                case 0:
                    SpawnRate = SplashKit.Rnd(0,80);                
                    _limits[typeof(BlueAlienship)] = 3;
                    _limits[typeof(PurpleAlienship)] = 2;
                    _limits[typeof(RedAlienship)] = 1;
                    _limits[typeof(KamikazeAlien)] = 0;
                    _limits[typeof(Asteroid)] = 5;
                    _limits[typeof(Spacemine)] = 1;
                    break;
                case 1:
                    SpawnRate = SplashKit.Rnd(0,65);
                    _limits[typeof(BlueAlienship)] = 4;
                    _limits[typeof(PurpleAlienship)] = 3;
                    _limits[typeof(RedAlienship)] = 2;
                    _limits[typeof(KamikazeAlien)] = 1;
                    _limits[typeof(Asteroid)] = 6;
                    _limits[typeof(Spacemine)] = 2;
                    break;
                default: 
                    SpawnRate = SplashKit.Rnd(0,70);
                    _limits[typeof(BlueAlienship)] = 5;
                    _limits[typeof(PurpleAlienship)] = 3;
                    _limits[typeof(RedAlienship)] = 2;
                    _limits[typeof(KamikazeAlien)] = 3;
                    _limits[typeof(Asteroid)] = 7;
                    _limits[typeof(Spacemine)] = 3;
                    break;
            }
        }      
        public override void AddEnemies(int score)
        {
            if ((score > 10 && _stage == _firstStageThreshold) || (score > 15 && _stage == _secondStageThreshold)) _stage++;
            base.AddEnemies(score);
        } 
    }
    public class MineFieldMode : GameMode
    {
        public MineFieldMode(int spawnRate) : base()
        {
            _limits[typeof(Asteroid)] = 10;
            _limits[typeof(Spacemine)] = 6;
            SpawnRate = spawnRate;
        }
    }
    public class BossRunMode : GameMode
    {
        private int _stage = 0;
        private readonly int _stageAmount = 3;
        public BossRunMode() : base()
        {
            _limits[typeof(Phantom)] = 1; 
            _limits[typeof(Nightmare)] = 1;
            _enemyAmountByType[typeof(Phantom)] = 0; 
            _enemyAmountByType[typeof(Nightmare)] = 0;
        }
        public override void CheckGameEnding(Player player)
        {
            base.CheckGameEnding(player);
            if (_stage == _stageAmount && Enemies.Count == 0) GameEnded = true;
        }
        public override void AddEnemies(int score)
        {
            if (Enemies.Count == 0)
            {
                switch(_stage)
                {
                    case 0:
                        Enemies.Add(new Nightmare());   
                        _stage++;
                        break;
                    case 1:
                        Enemies.Add(new Phantom());
                        _stage++;
                        break;
                    case 2:
                        Enemies.Add(new Phantom());
                        Enemies.Add(new Nightmare());
                        _stage++;
                        break;
                }
            }
        }  
    }
}