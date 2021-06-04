using System;
using System.Collections.Generic;
using SplashKitSDK;

//TODO: fix spawn rates for survival mode

namespace SpaceShooter
{
    public abstract class GameMode
    {
        protected Dictionary<Type, int> _limits = new Dictionary<Type, int>()
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
        protected readonly List<Enemy> _enemies = new List<Enemy>();
        private EnemyQuantityList _quantityList = new EnemyQuantityList();
        public IEnumerable<Enemy> Enemies 
        {
            get
            {
                foreach(var enemy in _enemies.ToArray()) yield return enemy;
            }
        }
        public int SpawnRate{get; protected set;}
        public bool GameEnded{get; protected set;} = false;
        public GameMode()
        {
            SpawnRate = 70;
        }
        private bool TimeToSpawn()=> SplashKit.Rnd(0, SpawnRate) == 0;
        public virtual void AddEnemies(int score)
        {
            int[] parameters;
            foreach (var enemyType in _quantityList.Type)
            {
                if (TimeToSpawn() && _quantityList.GetQuantity(enemyType) < _limits[enemyType])
                {
                    if (_enemies.Count == 0) parameters = new int[]{Global.Width, Global.Height};
                    else
                    {
                        var lastEnemy = _enemies[_enemies.Count - 1];
                        parameters = new int[]{lastEnemy.X, lastEnemy.Y};
                    } 
                    _enemies.Add(EnemyFactory.SpawnEnemy(enemyType, parameters));
                    _quantityList.UpdateQuantity(enemyType, 1);
                }
            }
        }
        public virtual void CheckGameEnding(Player player)
        {
            if (player.Health <= 0) GameEnded = true;
        }
        public void RemoveEnemy(Enemy enemy)
        {
            _quantityList.UpdateQuantity(enemy.GetType(), -1);
            _enemies.Remove(enemy);
        }
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
            if (score < 100 ) base.AddEnemies(score);
            else if (!_bossSpawned && _level > 3)
            {
                _bossSpawned = true;
                switch(_level)
                {
                    case 3: _enemies.Add(new Nightmare()); break;
                    case 4: _enemies.Add(new Phantom()); break;
                    default: throw new IndexOutOfRangeException($"The level {_level} is not implemented or the threshold for levels with bosses is incorrect");
                }
            }
        }
        public override void CheckGameEnding(Player player)
        {
            base.CheckGameEnding(player);
            if (_enemies.Count == 0 && ((_level < 3 && player.Score >= 100) || (_level >= 3 && _bossSpawned)))
                GameEnded = true;
        }
        private void SetEnemyLimitsByLevel()
        {
            if (_level < 1) _level = 1;
            if (_level > 4) _level = 4;
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
                    break;
                case 4:
                    _limits[typeof(BlueAlienship)] = 3;
                    _limits[typeof(PurpleAlienship)] = 2;
                    _limits[typeof(RedAlienship)] = 4;
                    _limits[typeof(KamikazeAlien)] = 3;
                    _limits[typeof(Asteroid)] = 5;
                    _limits[typeof(Spacemine)] = 3; 
                    break;
                default: throw new NotImplementedException($"The level {_level} is not implemented");
            }
        }
    }
    public class SurvivalMode : GameMode
    {
        private int _stage;
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
                    SpawnRate = SplashKit.Rnd(0,70);
                    _limits[typeof(BlueAlienship)] = 4;
                    _limits[typeof(PurpleAlienship)] = 3;
                    _limits[typeof(RedAlienship)] = 2;
                    _limits[typeof(KamikazeAlien)] = 2;
                    _limits[typeof(Asteroid)] = 6;
                    _limits[typeof(Spacemine)] = 2;
                    break;
                case 2: 
                    SpawnRate = SplashKit.Rnd(0,65);
                    _limits[typeof(BlueAlienship)] = 5;
                    _limits[typeof(PurpleAlienship)] = 3;
                    _limits[typeof(RedAlienship)] = 2;
                    _limits[typeof(KamikazeAlien)] = 2;
                    _limits[typeof(Asteroid)] = 7;
                    _limits[typeof(Spacemine)] = 3;
                    break;
                default: throw new IndexOutOfRangeException($"The stage index exceeds 2");
            }
        }      
        public override void AddEnemies(int score)
        {
            if ((score > 10 && _stage == 0) || (score > 20 && _stage == 1))
            {
                _stage++;
                SetEnemyLimitsByStage();
            } 
            // Console.WriteLine(_stage);
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
        public BossRunMode() : base(){}
        public override void CheckGameEnding(Player player)
        {
            base.CheckGameEnding(player);
            if (_stage == _stageAmount && _enemies.Count == 0) GameEnded = true;
        }
        public override void AddEnemies(int score)
        {
            if (_enemies.Count == 0)
            {
                switch(_stage)
                {
                    case 0:
                        _enemies.Add(new Nightmare());   
                        _stage++;
                        break;
                    case 1:
                        _enemies.Add(new Phantom());
                        _stage++;
                        break;
                    case 2:
                        _enemies.Add(new Phantom());
                        _enemies.Add(new Nightmare());
                        _stage++;
                        break;
                }
            }
        }  
    }
}